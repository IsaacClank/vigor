namespace Vigor.Core.Platform.Api.Queue;

public class QueueConsumerOptions
{
  public required string Stream { get; set; }
  public required string Group { get; set; }
  public required string Consumer { get; set; }
}
