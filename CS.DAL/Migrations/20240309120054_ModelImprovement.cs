using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS.DAL.Migrations
{
    public partial class ModelImprovement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WhenCreated",
                table: "TicketDetails",
                newName: "AssignmentTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "TicketDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "TicketDetails");

            migrationBuilder.RenameColumn(
                name: "AssignmentTime",
                table: "TicketDetails",
                newName: "WhenCreated");
        }
    }
}
