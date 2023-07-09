using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorCommunication.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRemovaAtUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RemovedAt",
                schema: "user",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemovedAt",
                schema: "user",
                table: "Users");
        }
    }
}
