using System.Text.Json;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using Vigor.Common.Queue.Redis;

namespace Vigor.Core.Platform.Db.Broadcast;

public partial class DbBroadcaster(
    ILogger<DbBroadcaster> logger,
    IOptions<DbBroadcasterOptions> options,
    IDbContextFactory<PlatformDbContext> dbContextFactory,
    RedisStreamPublisher queuePublisher) : BackgroundService
{
  private readonly ILogger<DbBroadcaster> _logger = logger;
  private readonly IOptions<DbBroadcasterOptions> _options = options;
  private readonly IDbContextFactory<PlatformDbContext> _dbContextFactory = dbContextFactory;
  private readonly RedisStreamPublisher _queuePublisher = queuePublisher;

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

    var outboxEvent = db.NextOutboxEvent();
    if (outboxEvent == null || !outboxEvent.AnyChange())
    {
      return;
    }

    LogDetectOutboxEvent(outboxEvent.Changes.Count());
    LogRawOutboxEventContent(JsonSerializer.Serialize(outboxEvent.Changes));

    var messages = outboxEvent.ParseChanges();
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
