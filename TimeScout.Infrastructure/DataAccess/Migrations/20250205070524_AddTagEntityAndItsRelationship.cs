using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TimeScout.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTagEntityAndItsRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_events_groups_event_group_id",
                table: "events");

            migrationBuilder.AddColumn<int>(
                name: "tag_id",
                table: "events",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    color = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tags", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_events_tag_id",
                table: "events",
                column: "tag_id");

            migrationBuilder.AddForeignKey(
                name: "fk_events_event_groups_event_group_id",
                table: "events",
                column: "event_group_id",
                principalTable: "event_groups",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_events_tags_tag_id",
                table: "events",
                column: "tag_id",
                principalTable: "tags",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_events_event_groups_event_group_id",
                table: "events");

            migrationBuilder.DropForeignKey(
                name: "fk_events_tags_tag_id",
                table: "events");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropIndex(
                name: "ix_events_tag_id",
                table: "events");

            migrationBuilder.DropColumn(
                name: "tag_id",
                table: "events");

            migrationBuilder.AddForeignKey(
                name: "fk_events_groups_event_group_id",
                table: "events",
                column: "event_group_id",
                principalTable: "event_groups",
                principalColumn: "id");
        }
    }
}
