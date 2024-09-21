using Vigor.Common.Queue.Redis.Exceptions;
using Vigor.Common.Queue.Redis.Options;

namespace Vigor.Common.Queue.Redis;

public class RedisStreamConsumer
{
  private readonly RedisClient _redisClient;
  private readonly RedisStreamConsumerOptions _options;

  public RedisStreamConsumer(RedisClient redisClient, RedisStreamConsumerOptions options)
  {
    _redisClient = redisClient;
    _options = options;

    if (!StreamGroupExists())
    {
      _redisClient.Connection.StreamCreateConsumerGroup(_options.Stream, _options.Group);
    }
  }

  private bool StreamGroupExists()
  {
    var streamExists = _redisClient.Connection.KeyExists(_options.Stream);
    if (!streamExists)
    {
      return false;
    }

    return _redisClient
      .Connection
      .StreamGroupInfo(_options.Stream)
      .Any(g => g.Name == _options.Group);
  }

  public void Acknowledge(string id)
  {
    if (_redisClient.Connection.StreamAcknowledge(_options.Stream, _options.Group, id) != 1)
    {
      throw new AcknowledgementException($"Failed to acknowledge message {id} in stream {_options.Stream} for group {_options.Group}");
    }
  }

  public StreamEntry? NextMessage()
  {
    var hasPendingMessages = _redisClient
      .Connection.StreamPending(_options.Stream, _options.Group)
      .Consumers.Any(c => c.Name == _options.Consumer && c.PendingMessageCount > 0);

    var positionFlag = hasPendingMessages ? "0" : ">";
    var messages = _redisClient.Connection.StreamReadGroup(_options.Stream, _options.Group, _options.Consumer, positionFlag, 1);

    if (messages.Length == 0)
    {
      return null;
    }

    var message = messages.First();

    return new StreamEntry()
    {
      Id = message.Id.ToString(),
      Values = message.Values.ToDictionary(x => x.Name.ToString(), x => x.Value.ToString())
    };
  }
}
