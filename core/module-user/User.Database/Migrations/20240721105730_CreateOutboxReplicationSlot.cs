using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Database.Migrations
{
    /// <inheritdoc />
    public partial class CreateOutboxReplicationSlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"SELECT * FROM pg_create_logical_replication_slot('outbox', 'wal2json')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"SELECT pg_drop_replication_slot('outbox')");
        }
    }
}
