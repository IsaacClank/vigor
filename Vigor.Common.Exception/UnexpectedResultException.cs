using System.Diagnostics.CodeAnalysis;

namespace Vigor.Common.Exception;

public class UnexpectedException : ApplicationException
{
  public static void ThrowIfNull([NotNull] object? result)
  {
    if (result is null)
    {
      throw new UnexpectedException("Unexpected null");
    }
  }

  public UnexpectedException() : base("Something unexpected happened")
  {
  }

  public UnexpectedException(string? message) : base(message)
  {
  }

  public UnexpectedException(string? message, System.Exception? innerException)
    : base(message, innerException)
  {
  }
}
