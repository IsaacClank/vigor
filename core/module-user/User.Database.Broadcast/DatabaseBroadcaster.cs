using System.Text.Json;
using User.Common.Queue;

namespace User.Database.Broadcast
{
  public class DatabaseBroadcaster(ILogger<DatabaseBroadcaster> logger, IServiceProvider serviceProvider, QueueClient redisClient) : BackgroundService
  {
    private readonly ILogger<DatabaseBroadcaster> _logger = logger;
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly QueueClient _redisClient = redisClient;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        BroadcastDbOutboxEvent();
        await Task.Delay(0, stoppingToken);
      }
    }

    protected void BroadcastDbOutboxEvent()
    {
      using var scope = _serviceProvider.CreateScope();
      var db = scope.ServiceProvider.GetRequiredService<UserDbContext>();

      if (!db.HasNewOutboxEntry())
      {
        return;
      }
      _logger.LogInformation("Detected outbox event");

      try
      {
        var outboxEvent = db.GetLatestOutboxEntry();
        _logger.LogTrace("Raw outbox payload: {}", JsonSerializer.Serialize(outboxEvent));

        if (!outboxEvent.Changes.Any())
        {
          return;
        }

        var messages = MapToMessages(outboxEvent);
        _logger.LogInformation("Generated {Count} messages from outbox event", messages.Count());

        foreach (var message in messages)
        {
          _redisClient.Publish("stream__core", message);
          _logger.LogInformation("Published {EventType} event", message.Type);
          _logger.LogTrace("Raw message payload: {}", JsonSerializer.Serialize(message));
        }

        _logger.LogInformation("Finished broadcasting latest outbox message");
      }
      catch (System.Exception)
      {
        throw;
      }
    }

    private static IEnumerable<QueueMessageData> MapToMessages(OutboxEntryData outboxData)
    {
      return (IEnumerable<QueueMessageData>)outboxData.Changes
        .Select(static change =>
        {
          var payloadDict = change.ColumnNames.Zip(change.ColumnValues).ToDictionary(entry => entry.First, entry => entry.Second);
          var payloadObj = JsonSerializer.Deserialize<object>(JsonSerializer.Serialize(payloadDict));

          return payloadObj == null
            ? null
            : new QueueMessageData() { Type = $"{change.Table}_{change.Kind}", Payload = payloadObj };
        })
        .Where(static m => m != null);
    }
  }
}
