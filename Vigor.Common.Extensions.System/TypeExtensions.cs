namespace Vigor.Common.Extensions.System;

public static class TypeExtensions
{
  public static bool TrySetProperty(this Type type, object obj, string property, object value)
  {
    var targetProp = type.GetProperties().FirstOrDefault(p => p.Name == property);
    if (targetProp is null)
    {
      return false;
    }

    targetProp.SetValue(obj, value);
    return true;
  }
}
