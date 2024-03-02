using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS.DAL.Migrations
{
    public partial class TicketAndMessageModelsImprovemrnt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessageDetails_DetailsId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "MessageDetails");

            migrationBuilder.DropIndex(
                name: "IX_Messages_DetailsId",
                table: "Messages");

            migrationBuilder.AddColumn<DateTime>(
                name: "WhenSend",
                table: "Messages",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WhenSend",
                table: "Messages");

            migrationBuilder.CreateTable(
                name: "MessageDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WhenCreated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageDetails", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_DetailsId",
                table: "Messages",
                column: "DetailsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessageDetails_DetailsId",
                table: "Messages",
                column: "DetailsId",
                principalTable: "MessageDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
