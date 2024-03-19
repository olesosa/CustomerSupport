using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS.DAL.Migrations
{
    public partial class ChangedIsAssignedProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignmentTime",
                table: "TicketDetails");

            migrationBuilder.AddColumn<DateTime>(
                name: "AssignmentTime",
                table: "Tickets",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignmentTime",
                table: "Tickets");

            migrationBuilder.AddColumn<DateTime>(
                name: "AssignmentTime",
                table: "TicketDetails",
                type: "datetime2",
                nullable: true);
        }
    }
}
