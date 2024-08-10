using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using User.Common.Queue;

namespace User.Database.Broadcast
{
  public partial class DatabaseBroadcaster(
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

      var outboxEntry = db.NextOutboxEvent();
      if (outboxEntry == null || !outboxEntry.Changes.Any())
      {
        return;
      }

      LogDetectOutboxEvent(outboxEntry.Changes.Count());
      LogRawOutboxEventContent(JsonSerializer.Serialize(outboxEntry.Changes));

      var messages = (IEnumerable<Dictionary<string, string>>)outboxEntry
          .Changes
          .Where(c => c.ColumnNames.Length > 0)
          .Select(c => c.ToDictionary());

      LogNumberOfGeneratedMessages(messages.Count());

      foreach (var message in messages)
      {
        _queuePublisher.Publish(message);
        LogRawMessage(message);
        LogPublishedMessageType(message["Type"]);
      }
    }

    #region Loggers
    [LoggerMessage(Level = LogLevel.Information, Message = "Detected outbox event with {Count} changes.")]
    private partial void LogDetectOutboxEvent(int count);

    [LoggerMessage(Level = LogLevel.Trace, Message = "Raw outbox event: {Content}")]
    private partial void LogRawOutboxEventContent(string content);

    [LoggerMessage(Level = LogLevel.Information, Message = "Generated {Count} messages from outbox event.")]
    private partial void LogNumberOfGeneratedMessages(int count);

    [LoggerMessage(Level = LogLevel.Information, Message = "Published {MessageType}.")]
    private partial void LogPublishedMessageType(string messageType);

    [LoggerMessage(Level = LogLevel.Trace, Message = "Raw queue message: {Content}")]
    private partial void LogRawMessage(Dictionary<string, string> content);
    #endregion
  }
}
