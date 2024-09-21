using System.Diagnostics.CodeAnalysis;

namespace Vigor.Common.Exception;

public class EntityNotFoundException : ApplicationException
{
  public EntityNotFoundException()
  {
  }

  public EntityNotFoundException(string message) : base(message)
  {
  }

  public EntityNotFoundException(string? message, System.Exception? innerException) : base(message, innerException)
  {
  }

  public static void ThrowIfNull<T>([NotNull] T? value) where T : class
  {
    if (value is null)
    {
      throw new EntityNotFoundException($"{typeof(T).Name} does not exist.");
    }
  }
}
