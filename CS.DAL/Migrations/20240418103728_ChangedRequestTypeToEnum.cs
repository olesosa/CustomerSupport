using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS.DAL.Migrations
{
    public partial class ChangedRequestTypeToEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestTypeTemp",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.Sql("UPDATE Tickets SET RequestTypeTemp = RequestType");
                
            migrationBuilder.Sql("UPDATE Tickets SET RequestType = 0 ");
            
            migrationBuilder.AlterColumn<int>(
                name: "RequestType",
                table: "Tickets",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.Sql("UPDATE Tickets " +
                                 "SET RequestType = CASE " +
                                 "WHEN RequestTypeTemp = 'Payment Issue' THEN '0' " +
                                 "WHEN RequestTypeTemp = 'Website Issue' THEN '1' " +
                                 "WHEN RequestTypeTemp = 'Security Issue' THEN '2' " +
                                 "ELSE '3' " +
                                 "END");

            migrationBuilder.DropColumn(
                name: "RequestTypeTemp",
                table: "Tickets");
        }
        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequestType",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
