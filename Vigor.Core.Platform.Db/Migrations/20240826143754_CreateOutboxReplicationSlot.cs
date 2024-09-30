using Microsoft.EntityFrameworkCore.Migrations;

namespace Vigor.Core.Platform.Db.Migrations
{
  public partial class CreateOutboxReplicationSlot : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql(@"SELECT pg_create_logical_replication_slot('outbox', 'wal2json')");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql(@"SELECT pg_drop_replication_slot('outbox')");
    }
  }
}
