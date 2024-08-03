namespace User.Common.Queue.Exceptions;

public class AcknowledgeMessageException(string? message = default, Exception? innerException = null) : Exception(message, innerException)
{
}
