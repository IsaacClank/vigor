using StackExchange.Redis;
using System.Text.Json;

namespace User.Common.Queue
{
  public class QueueClient(string? connectionString)
  {
    private readonly ConnectionMultiplexer _connection = string.IsNullOrWhiteSpace(connectionString)
      ? throw new ArgumentNullException("Queue connection string cannot be null")
      : ConnectionMultiplexer.Connect(connectionString);

    public IDatabase Connection { get => _connection.GetDatabase(); }

    public void Publish(string stream, QueueMessageData data)
    {
      Connection.StreamAdd(stream, "message", JsonSerializer.Serialize(data));
    }

    public void Consume(string stream, string group, string consumer)
    {
      Connection.StreamReadGroup(stream, group, consumer, ">", 1);
    }
  }
}
