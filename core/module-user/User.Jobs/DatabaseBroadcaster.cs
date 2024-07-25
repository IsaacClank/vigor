using User.Database;

namespace User.Jobs
{
  public class DatabaseBroadcaster(IServiceProvider serviceProvider, ILogger<DatabaseBroadcaster> logger) : BackgroundService
  {
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<DatabaseBroadcaster> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        using (var scope = _serviceProvider.CreateScope())
        {
          var db = scope.ServiceProvider.GetRequiredService<UserDbContext>();
          var change = db.GetNextLogicalSlotChange();

          if (change is not null)
          {
            _logger.LogInformation("Detected database change: {}", change);
            await Task.Delay(5000, stoppingToken);
            _logger.LogInformation("Finished processing latest change");
            db.AcknowledgeLatestLogicalSlotChange();
          }
          else
          {
            _logger.LogInformation("No changes");
            await Task.Delay(5000, stoppingToken);
          }
        }
      }
    }
  }
}
