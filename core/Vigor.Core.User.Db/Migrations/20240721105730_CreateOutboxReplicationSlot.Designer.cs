// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vigor.Core.User.Db.Migrations
{
  [DbContext(typeof(UserDbContext))]
  [Migration("20240721105730_CreateOutboxReplicationSlot")]
  partial class CreateOutboxReplicationSlot
  {
    /// <inheritdoc />
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
      modelBuilder
          .HasAnnotation("ProductVersion", "8.0.4")
          .HasAnnotation("Relational:MaxIdentifierLength", 63);

      NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);
#pragma warning restore 612, 618
    }
  }
}
