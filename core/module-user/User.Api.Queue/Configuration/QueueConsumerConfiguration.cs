namespace User.Api.Queue.Configuration;

public class QueueConsumerConfiguration
{
  public required string Stream { get; set; }
  public required string Group { get; set; }
  public required string Consumer { get; set; }
}
