using User.Common.Queue.Exceptions;

namespace User.Common.Queue;

public class QueueConsumer : IQueueConsumer
{
  private readonly QueueClient _queue;
  private readonly string _stream;
  private readonly string _group;
  private readonly string _consumer;

  public QueueConsumer(QueueClient queue, string stream, string group, string consumer)
  {
    _queue = queue;
    _stream = stream;
    _group = group;
    _consumer = consumer;

    bool streamExists = _queue.Connection.KeyExists(_stream);
    if (!streamExists || (streamExists && !_queue.Connection.StreamGroupInfo(_stream).Any(g => g.Name == group)))
    {
      if (!_queue.Connection.StreamCreateConsumerGroup(_stream, _group))
      {
        throw new CreateConsumerGroupException($"Failed to create consumer group {_group} for stream {_stream}.");
      }
    }
  }

  public void Acknowledge(string id)
  {
    if (_queue.Connection.StreamAcknowledge(_stream, _group, id) != 1)
    {
      throw new AcknowledgeMessageException($"Failed to acknowledge message {id} in stream {_stream} for group {_group}");
    }
  }

  public QueueMessage? GetNextMessage()
  {
    var hasPendingMessages = _queue
      .Connection.StreamPending(_stream, _group)
      .Consumers.Any(c => c.Name == _consumer && c.PendingMessageCount > 0);

    var positionFlag = hasPendingMessages ? "0" : ">";
    var messages = _queue.Connection.StreamReadGroup(_stream, _group, _consumer, positionFlag, 1);

    if (messages.Length == 0)
    {
      return null;
    }

    var message = messages.First();

    return new QueueMessage()
    {
      Id = message.Id.ToString(),
      Values = message.Values.ToDictionary(x => x.Name.ToString(), x => x.Value.ToString())
    };
  }
}
