using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vigor.Core.User.Db.Migrations;

[DbContext(typeof(UserDbContext))]
[Migration("20240721105730_CreateOutboxReplicationSlot")]
partial class CreateOutboxReplicationSlot
{
  protected override void BuildTargetModel(ModelBuilder modelBuilder)
  {
    modelBuilder
        .HasAnnotation("ProductVersion", "8.0.4")
        .HasAnnotation("Relational:MaxIdentifierLength", 63);

    NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);
  }
}
