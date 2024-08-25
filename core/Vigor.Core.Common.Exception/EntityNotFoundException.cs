namespace Vigor.Core.Common.Exception;

public class EntityNotFoundException<T>() : ApplicationException($"{typeof(T).Name} does not exist.")
{
}
