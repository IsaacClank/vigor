namespace User.Api.Queue;

public class ConsumerOptions
{
  public required string Stream { get; set; }
  public required string Group { get; set; }
  public required string Consumer { get; set; }
}
