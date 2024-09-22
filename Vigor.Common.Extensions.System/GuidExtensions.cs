namespace Vigor.Common.Extensions.System;

public static class GuidExtensions
{
  public static bool IsEmptyOrDefault(this Guid? guid) => guid == default || guid == Guid.Empty;
}
