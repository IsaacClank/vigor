using Vigor.Common.Queue.Redis;

namespace Vigor.Core.Program.Api.Queue;

public partial class QueueConsumer(
  ILogger<QueueConsumer> logger,
  RedisStreamConsumer consumer) : BackgroundService
{
  private readonly ILogger<QueueConsumer> _logger = logger;
  private readonly RedisStreamConsumer _queueConsumer = consumer;

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
    {
      ProcessNextMessage();
      await Task.Delay(0, stoppingToken);
    }
  }

  protected void ProcessNextMessage()
  {
    var message = _queueConsumer.NextMessage();

    if (message == null || string.IsNullOrWhiteSpace(message.Id))
    {
      return;
    }

    _queueConsumer.Acknowledge(message.Id);
    LogMessageAcknowledgement(message.Id);
  }

  #region Loggers
  [LoggerMessage(Level = LogLevel.Information, Message = "Acknowledged {Id}.")]
  private partial void LogMessageAcknowledgement(string Id);
  #endregion
}
