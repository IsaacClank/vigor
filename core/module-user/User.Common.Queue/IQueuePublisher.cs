namespace User.Common.Queue;

public interface IQueuePublisher
{
  public void Publish(Dictionary<string, object> message);
}
