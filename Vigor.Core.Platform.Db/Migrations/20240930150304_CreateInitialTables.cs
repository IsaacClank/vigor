using Microsoft.EntityFrameworkCore.Migrations;

namespace Vigor.Core.Platform.Db.Migrations
{
  /// <inheritdoc />
  public partial class CreateInitialTables : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "facility",
          columns: table => new
          {
            id = table.Column<Guid>(type: "uuid", nullable: false),
            name = table.Column<string>(type: "text", nullable: false),
            address = table.Column<string>(type: "jsonb", nullable: true),
            owner_id = table.Column<Guid>(type: "uuid", nullable: false),
            created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("pk_facility", x => x.id);
          });

      migrationBuilder.CreateTable(
          name: "training_plan",
          columns: table => new
          {
            id = table.Column<Guid>(type: "uuid", nullable: false),
            name = table.Column<string>(type: "text", nullable: false),
            monthly_fee = table.Column<decimal>(type: "numeric", nullable: false),
            owner_id = table.Column<Guid>(type: "uuid", nullable: false),
            created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("pk_training_plan", x => x.id);
          });

      migrationBuilder.CreateTable(
          name: "facility_training_plan",
          columns: table => new
          {
            facilities_id = table.Column<Guid>(type: "uuid", nullable: false),
            training_plans_id = table.Column<Guid>(type: "uuid", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("pk_facility_training_plan", x => new { x.facilities_id, x.training_plans_id });
            table.ForeignKey(
                      name: "fk_facility_training_plan_facility_facilities_id",
                      column: x => x.facilities_id,
                      principalTable: "facility",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "fk_facility_training_plan_training_plan_training_plans_id",
                      column: x => x.training_plans_id,
                      principalTable: "training_plan",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "membership",
          columns: table => new
          {
            id = table.Column<Guid>(type: "uuid", nullable: false),
            training_plan_id = table.Column<Guid>(type: "uuid", nullable: false),
            owner_id = table.Column<Guid>(type: "uuid", nullable: false),
            created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("pk_membership", x => x.id);
            table.ForeignKey(
                      name: "fk_membership_training_plan_training_plan_id",
                      column: x => x.training_plan_id,
                      principalTable: "training_plan",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "ix_facility_training_plan_training_plans_id",
          table: "facility_training_plan",
          column: "training_plans_id");

      migrationBuilder.CreateIndex(
          name: "ix_membership_training_plan_id",
          table: "membership",
          column: "training_plan_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "facility_training_plan");

      migrationBuilder.DropTable(
          name: "membership");

      migrationBuilder.DropTable(
          name: "facility");

      migrationBuilder.DropTable(
          name: "training_plan");
    }
  }
}
