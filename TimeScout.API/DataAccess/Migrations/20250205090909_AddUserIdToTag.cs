using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeScout.API.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created",
                table: "tags",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "tags",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "modified",
                table: "tags",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "tags",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created",
                table: "tags");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "tags");

            migrationBuilder.DropColumn(
                name: "modified",
                table: "tags");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "tags");
        }
    }
}
