using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeScout.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SetEventGroupIdNullableOnEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "event_group_id",
                table: "events",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "event_group_id",
                table: "events",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
