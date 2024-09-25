using System.Diagnostics.CodeAnalysis;

namespace Vigor.Common.Exception;

public class UnexpectedResultException : ApplicationException
{
  public static void ThrowIfNull([NotNull] object? result)
  {
    if (result is null)
    {
      throw new UnexpectedResultException("Unexpected null result");
    }
  }

  public UnexpectedResultException(string? message) : base(message)
  {
  }

  public UnexpectedResultException(string? message, System.Exception? innerException) : base(message, innerException)
  {
  }
}
