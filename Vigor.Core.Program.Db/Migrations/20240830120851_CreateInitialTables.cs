using Microsoft.EntityFrameworkCore.Migrations;

using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Vigor.Core.Program.Db.Migrations;

public partial class CreateInitialTables : Migration
{
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.CreateTable(
      name: "facility",
      columns: table => new
      {
        id = table.Column<int>(type: "integer", nullable: false)
              .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
        name = table.Column<string>(type: "text", nullable: false),
        address = table.Column<string>(type: "jsonb", nullable: true),
        owner_id = table.Column<int>(type: "integer", nullable: false),
        created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
        updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
      },
      constraints: table =>
      {
        table.PrimaryKey("pk_facility", x => x.id);
      });

    migrationBuilder.CreateTable(
      name: "program",
      columns: table => new
      {
        id = table.Column<int>(type: "integer", nullable: false)
              .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
        name = table.Column<string>(type: "text", nullable: false),
        monthly_fee = table.Column<decimal>(type: "numeric", nullable: false),
        owner_id = table.Column<int>(type: "integer", nullable: false),
        created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
        updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
      },
      constraints: table =>
      {
        table.PrimaryKey("pk_program", x => x.id);
      });

    migrationBuilder.CreateTable(
      name: "facility_program",
      columns: table => new
      {
        facilities_id = table.Column<int>(type: "integer", nullable: false),
        programs_id = table.Column<int>(type: "integer", nullable: false)
      },
      constraints: table =>
      {
        table.PrimaryKey("pk_facility_program", x => new { x.facilities_id, x.programs_id });
        table.ForeignKey(
          name: "fk_facility_program_facility_facilities_id",
          column: x => x.facilities_id,
          principalTable: "facility",
          principalColumn: "id",
          onDelete: ReferentialAction.Cascade);
        table.ForeignKey(
          name: "fk_facility_program_program_programs_id",
          column: x => x.programs_id,
          principalTable: "program",
          principalColumn: "id",
          onDelete: ReferentialAction.Cascade);
      });

    migrationBuilder.CreateTable(
      name: "membership",
      columns: table => new
      {
        id = table.Column<int>(type: "integer", nullable: false)
              .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
        program_id = table.Column<int>(type: "integer", nullable: false),
        created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
        updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
      },
      constraints: table =>
      {
        table.PrimaryKey("pk_membership", x => x.id);
        table.ForeignKey(
          name: "fk_membership_program_program_id",
          column: x => x.program_id,
          principalTable: "program",
          principalColumn: "id",
          onDelete: ReferentialAction.Cascade);
      });

    migrationBuilder.CreateIndex(
        name: "ix_facility_program_programs_id",
        table: "facility_program",
        column: "programs_id");

    migrationBuilder.CreateIndex(
        name: "ix_membership_program_id",
        table: "membership",
        column: "program_id");
  }

  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropTable(name: "facility_program");
    migrationBuilder.DropTable(name: "membership");
    migrationBuilder.DropTable(name: "facility");
    migrationBuilder.DropTable(name: "program");
  }
}
