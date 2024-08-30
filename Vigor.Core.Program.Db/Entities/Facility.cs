using System.ComponentModel.DataAnnotations.Schema;

using Vigor.Core.Common.Db.Repository;

namespace Vigor.Core.Program.Db.Entities;

public class Facility : Entity
{
  public required string Name { get; set; }

  [Column(TypeName = "jsonb")]
  public string? Address { get; set; }

  #region Relations
  public IEnumerable<Program> Programs { get; set; } = [];
  #endregion

  #region External fields
  public int OwnerId { get; set; }
  #endregion
}
