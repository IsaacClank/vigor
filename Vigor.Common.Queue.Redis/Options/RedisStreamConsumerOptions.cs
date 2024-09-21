namespace Vigor.Common.Queue.Redis.Options;

public class RedisStreamConsumerOptions
{
  public string Stream { get; set; } = string.Empty;
  public string Group { get; set; } = string.Empty;
  public string Consumer { get; set; } = string.Empty;
}
