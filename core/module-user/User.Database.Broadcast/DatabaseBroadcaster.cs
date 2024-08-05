using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using User.Common.Queue;

namespace User.Database.Broadcast
{
  public class DatabaseBroadcaster(
      ILogger<DatabaseBroadcaster> logger,
      IDbContextFactory<UserDbContext> dbContextFactory,
      IQueuePublisher queuePublisher) : BackgroundService
  {
    private readonly ILogger<DatabaseBroadcaster> _logger = logger;
    private readonly IDbContextFactory<UserDbContext> _dbContextFactory = dbContextFactory;
    private readonly IQueuePublisher _queuePublisher = queuePublisher;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        BroadcastDbchanges();
        await Task.Delay(0, stoppingToken);
      }
    }

    protected void BroadcastDbchanges()
    {
      using var db = _dbContextFactory.CreateDbContext();
      if (!db.AnyOutboxEvent())
      {
        return;
      }
      else
      {
        _logger.LogInformation("Detected outbox event");
      }

      var outboxEntry = db.NextOutboxEvent();
      if (_logger.IsEnabled(LogLevel.Trace))
      {
        _logger.LogTrace("Raw outbox entry content: {}", JsonSerializer.Serialize(outboxEntry));
      }
      if (!outboxEntry.Changes.Any())
      {
        return;
      }

      var messages = ConvertToQueueMessages(outboxEntry);
      _logger.LogInformation("Generated {Count} messages from outbox event", messages.Count());

      foreach (var message in messages)
      {
        _queuePublisher.Publish(message);
        _logger.LogInformation("Published {EventType} event", message["Type"]);
        if (_logger.IsEnabled(LogLevel.Trace))
        {
          _logger.LogTrace("Raw queue message: {}", JsonSerializer.Serialize(message));
        }
      }
    }

    private static IEnumerable<Dictionary<string, string>> ConvertToQueueMessages(OutboxEntryContent outboxData) => outboxData
      .Changes
      .Where(c => c.ColumnNames.Length > 0)
      .Select(change => ConvertToQueueMessage(change));

    private static Dictionary<string, string> ConvertToQueueMessage(OutboxEntryChange change) => change
      .ColumnNames.Prepend("Type")
      .Zip(change.ColumnValues.Prepend($"{change.Table}_{change.Kind}"))
      .Where(e => e.Second != null)
      .ToDictionary(e => e.First.ToString(), e => e.Second.ToString()) as Dictionary<string, string>;
  }
}
