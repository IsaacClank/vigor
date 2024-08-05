using StackExchange.Redis;

namespace User.Common.Queue;

public class QueueClient(string? connectionString)
{
  private readonly ConnectionMultiplexer _connection = string.IsNullOrWhiteSpace(connectionString)
    ? throw new ArgumentNullException("Queue connection string cannot be null")
    : ConnectionMultiplexer.Connect(connectionString);

  public IDatabase Connection { get => _connection.GetDatabase(); }
}
