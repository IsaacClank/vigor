namespace Vigor.Common.Db.Repository;

public abstract class Entity
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}
