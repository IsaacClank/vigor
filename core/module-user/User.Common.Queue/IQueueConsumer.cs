namespace User.Common.Queue;

public interface IQueueConsumer
{
  public QueueMessage? GetNextMessage();
  public void Acknowledge(string id);
}
