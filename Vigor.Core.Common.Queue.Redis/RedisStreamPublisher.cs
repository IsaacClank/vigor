using StackExchange.Redis;

using Vigor.Core.Common.Queue.Redis.Options;

namespace Vigor.Core.Common.Queue.Redis;

public class RedisStreamPublisher(RedisClient redisClient, RedisStreamPublisherOptions options)
{
  private readonly RedisClient _redisClient = redisClient;
  private readonly RedisStreamPublisherOptions _options = options;

  public void Publish(Dictionary<string, string> message)
  {
    IEnumerable<NameValueEntry> content = [];

    foreach (var (key, value) in message)
    {
      content = content.Append(new(key, value.ToString()));
    }

    _redisClient.Connection.StreamAdd(_options.Stream, content.ToArray());
  }
}
