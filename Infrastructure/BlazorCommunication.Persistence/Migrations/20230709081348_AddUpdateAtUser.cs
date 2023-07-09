using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorCommunication.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdateAtUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "user",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "user",
                table: "Users");
        }
    }
}
