using StackExchange.Redis;

namespace User.Common.Queue;

public class QueuePublisher(QueueClient queue, string stream) : IQueuePublisher
{
  private readonly QueueClient _queue = queue;
  private readonly string _stream = stream;

  public void Publish(Dictionary<string, string> message)
  {
    NameValueEntry[] content = [];

    foreach (var (key, value) in message)
    {
      content = [.. content, new(key, value.ToString())];
    }

    _queue.Connection.StreamAdd(_stream, content);
  }
}
