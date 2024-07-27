namespace User.Common.Queue;

public class QueueMessageData
{
  public required string Type { get; set; }
  public required object Payload { get; set; }
}
