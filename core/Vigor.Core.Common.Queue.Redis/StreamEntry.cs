using System.Globalization;

namespace Vigor.Core.Common.Queue.Redis;

public class StreamEntry
{
  public required string Id { get; set; }
  public required Dictionary<string, string> Values { get; set; }

  public T MapTo<T>() where T : new()
  {
    T instance = new();
    Type type = typeof(T);

    foreach (var (key, value) in Values)
    {
      var property = type
        .GetProperties()
        .FirstOrDefault(p => p.Name.Contains(key, StringComparison.InvariantCultureIgnoreCase));

      if (property == null || !property.CanWrite)
      {
        continue;
      }

      var parsedValue = Convert.ChangeType(value, property.PropertyType, CultureInfo.InvariantCulture);
      property.SetValue(instance, parsedValue);
    }

    return instance;
  }
}
