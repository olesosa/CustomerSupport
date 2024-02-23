using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS.DAL.Migrations
{
    public partial class AddedIsAssignedProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAssigned",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAssigned",
                table: "Tickets");
        }
    }
}
