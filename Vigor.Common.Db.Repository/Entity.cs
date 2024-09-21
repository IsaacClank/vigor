using System.ComponentModel.DataAnnotations;

namespace Vigor.Common.Db.Repository;

public abstract class Entity
{
  public Guid Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }

  [Timestamp]
  public uint Version { get; set; }
}
