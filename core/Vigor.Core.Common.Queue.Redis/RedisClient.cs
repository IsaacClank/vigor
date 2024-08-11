using StackExchange.Redis;

namespace Vigor.Core.Common.Queue.Redis;

public class RedisClient(string connectionString)
{
  private readonly ConnectionMultiplexer _connection = ConnectionMultiplexer.Connect(connectionString);
  public IDatabase Connection { get => _connection.GetDatabase(); }
}
