using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeScout.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserRelationWithEventGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "event_group_user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "event_group_user",
                columns: table => new
                {
                    event_groups_id = table.Column<int>(type: "integer", nullable: false),
                    members_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_event_group_user", x => new { x.event_groups_id, x.members_id });
                    table.ForeignKey(
                        name: "fk_event_group_user_groups_event_groups_id",
                        column: x => x.event_groups_id,
                        principalTable: "event_groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_event_group_user_users_members_id",
                        column: x => x.members_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_event_group_user_members_id",
                table: "event_group_user",
                column: "members_id");
        }
    }
}
