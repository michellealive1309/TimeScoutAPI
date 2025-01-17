using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeScout.API.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ResettingEventGroupAndEventRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_events_event_groups_event_group_id",
                table: "events");

            migrationBuilder.AddForeignKey(
                name: "fk_events_groups_event_group_id",
                table: "events",
                column: "event_group_id",
                principalTable: "event_groups",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_events_groups_event_group_id",
                table: "events");

            migrationBuilder.AddForeignKey(
                name: "fk_events_event_groups_event_group_id",
                table: "events",
                column: "event_group_id",
                principalTable: "event_groups",
                principalColumn: "id");
        }
    }
}
