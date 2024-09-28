namespace Vigor.Common.Extensions.System;

public static class TypeExtensions
{
  public static void SetProperty(this Type type, object obj, string property, object value)
  {
    type.GetProperties().First(p => p.Name == property).SetValue(obj, value);
  }
}
