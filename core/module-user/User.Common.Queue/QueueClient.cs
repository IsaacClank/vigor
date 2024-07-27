using StackExchange.Redis;
using System.Text.Json;

namespace User.Common.Queue
{
  public class QueueClient(string connectionString)
  {
    private readonly ConnectionMultiplexer _connection = ConnectionMultiplexer.Connect(connectionString);

    public IDatabase Connection { get => _connection.GetDatabase(); }

    public void Publish(string stream, QueueMessageData data)
    {
      Connection.StreamAdd(stream, "message", JsonSerializer.Serialize(data));
    }
  }
}
