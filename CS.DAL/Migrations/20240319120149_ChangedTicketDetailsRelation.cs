using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS.DAL.Migrations
{
    public partial class ChangedTicketDetailsRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketDetails_DetailsId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_DetailsId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "DetailsId",
                table: "Tickets");

            migrationBuilder.AddColumn<Guid>(
                name: "TicketId",
                table: "TicketDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TicketDetails_TicketId",
                table: "TicketDetails",
                column: "TicketId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketDetails_Tickets_TicketId",
                table: "TicketDetails",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketDetails_Tickets_TicketId",
                table: "TicketDetails");

            migrationBuilder.DropIndex(
                name: "IX_TicketDetails_TicketId",
                table: "TicketDetails");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "TicketDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "DetailsId",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_DetailsId",
                table: "Tickets",
                column: "DetailsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketDetails_DetailsId",
                table: "Tickets",
                column: "DetailsId",
                principalTable: "TicketDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
