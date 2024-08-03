namespace User.Common.Queue.Exceptions;

public class CreateConsumerGroupException(
    string? message = default,
    Exception? innerException = default) : Exception(message, innerException)
{
}
