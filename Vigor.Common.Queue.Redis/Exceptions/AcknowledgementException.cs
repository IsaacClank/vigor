namespace Vigor.Common.Queue.Redis.Exceptions;

public class AcknowledgementException : Exception
{
  public AcknowledgementException(string? message) : base(message)
  {
  }

  public AcknowledgementException(string? message, Exception? innerException) : base(message, innerException)
  {
  }
}
