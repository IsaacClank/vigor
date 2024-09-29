using Vigor.Common.Exception;
using Vigor.Common.Extensions.System;

namespace Vigor.Common.Util;

/// <summary>
/// Simple object mapping using <see cref="System.Reflection"/>.
/// </summary>
public static partial class Util
{
  /// <summary>
  /// Populate a destination object using properties from the source object.
  /// </summary>
  /// <typeparam name="TSrc">The source type</typeparam>
  /// <typeparam name="TDest">The destination type</typeparam>
  /// <param name="dest">An instance of <typeparamref name="TDest"/></param>
  /// <param name="src">An instance of <typeparamref name="TSrc"/></param>
  /// <param name="includedProperties"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public static void Map<TDest, TSrc>(
    TDest dest,
    TSrc src,
    IEnumerable<string>? includedProperties = default)
  {
    ArgumentNullException.ThrowIfNull(dest);
    ArgumentNullException.ThrowIfNull(src);
    src
      .GetType()
      .GetProperties()
      .Where(prop => (includedProperties?.Contains(prop.Name) ?? true) && prop.GetValue(src) is not null)
      .ToList()
      .ForEach(srcProp => dest.GetType().TrySetProperty(
        dest,
        srcProp.Name,
        srcProp.GetValue(src) ?? throw new UnexpectedException()));
  }

  /// <summary>
  /// Map the source object to a given type. The destination object is instantiated.
  /// </summary>
  /// <typeparam name="TDest">The destination type</typeparam>
  /// <param name="src"></param>
  /// <param name="includedProperties"></param>
  /// <returns>The mapped object of type <typeparamref name="TDest"/></returns>
  /// <exception cref="MissingMethodException"></exception>
  public static TDest Map<TDest>(
    object src,
    IEnumerable<string>? includedProperties = default)
  {
    var dest = Activator.CreateInstance<TDest>() ?? throw new UnexpectedException();
    Map(dest, src, includedProperties);
    return dest;
  }

  /// <summary>
  /// Map the source object to a given type. The destination object is instantiated.
  /// </summary>
  /// <typeparam name="TDest">The destination type</typeparam>
  /// <param name="src"></param>
  /// <param name="includedProperties"></param>
  /// <returns>The mapped object of type <typeparamref name="TDest"/></returns>
  /// <exception cref="MissingMethodException"></exception>
  public static IEnumerable<TDest> MapRange<TDest>(
    IEnumerable<object> src,
    IEnumerable<string>? includedProperties = default)
  {
    return src.Select(s =>
    {
      var dest = Activator.CreateInstance<TDest>() ?? throw new UnexpectedException();
      Map(dest, s, includedProperties);
      return dest;
    });
  }
}
