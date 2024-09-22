using Microsoft.Extensions.Logging;

namespace Vigor.Common.Extensions.Logging;

public static partial class LoggingExtensions
{
  [LoggerMessage(Level = LogLevel.Information, Message = "Upserted {Entity} {Id}")]
  public static partial void UpsertedEntity(this ILogger logger, string entity, Guid id);

  [LoggerMessage(Level = LogLevel.Information, Message = "Deleted {Entity} {Id}")]
  public static partial void DeletedEntity(this ILogger logger, string entity, Guid id);
}
