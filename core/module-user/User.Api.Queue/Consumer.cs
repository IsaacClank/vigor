using User.Common.Queue;

namespace User.Api.Queue;

public class Consumer(ILogger<Consumer> logger, QueueClient queue) : BackgroundService
{
  private readonly ILogger<Consumer> _logger = logger;
  private readonly QueueClient _queue = queue;

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
    {
      if (_logger.IsEnabled(LogLevel.Information))
      {
        _logger.LogInformation("Worker running at: {Time}", DateTimeOffset.Now);
      }
      await Task.Delay(1000, stoppingToken);
    }
  }
}
