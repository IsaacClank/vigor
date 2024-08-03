using System.Text.Json;
using User.Common.Queue;

namespace User.Api.Queue;

public class Consumer(ILogger<Consumer> logger, IQueueConsumer queue) : BackgroundService
{
  private readonly ILogger<Consumer> _logger = logger;
  private readonly IQueueConsumer _queue = queue;

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
    var message = _queue.GetNextMessage();

    if (message == null || string.IsNullOrWhiteSpace(message.Id))
    {
      return;
    }

    if (_logger.IsEnabled(LogLevel.Trace))
      _logger.LogTrace("{Id}: {Content}", message.Id, JsonSerializer.Serialize(message.MapTo<UserRegisteredPayload>()));

    // Handling logic here.
    _queue.Acknowledge(message.Id);
    _logger.LogInformation("Acknowledged {Id}", message.Id);
  }
}

public class QueuePayload
{
  public string? Type { get; set; }
}

public class UserRegisteredPayload : QueuePayload
{
  public string? Email { get; set; }
}
