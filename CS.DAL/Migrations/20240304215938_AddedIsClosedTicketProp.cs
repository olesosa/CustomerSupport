using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS.DAL.Migrations
{
    public partial class AddedIsClosedTicketProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "Tickets");
        }
    }
}
