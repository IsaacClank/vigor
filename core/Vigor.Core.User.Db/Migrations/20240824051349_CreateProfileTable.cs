using Microsoft.EntityFrameworkCore.Migrations;

using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Vigor.Core.User.Db.Migrations;

public partial class CreateProfileTable : Migration
{
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.CreateTable(
        name: "profile",
        columns: table => new
        {
          id = table.Column<int>(type: "integer", nullable: false)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
          type = table.Column<int>(type: "integer", nullable: false),
          created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
          updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
        },
        constraints: table => table.PrimaryKey("pk_profile", x => x.id));
  }

  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropTable(name: "profile");
  }
}
