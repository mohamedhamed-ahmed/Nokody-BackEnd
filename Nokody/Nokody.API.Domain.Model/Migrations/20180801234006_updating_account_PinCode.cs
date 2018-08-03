using Microsoft.EntityFrameworkCore.Migrations;

namespace Nokody.API.Domain.Model.Migrations
{
    public partial class updating_account_PinCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PinCode",
                table: "Account",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PinCode",
                table: "Account");
        }
    }
}
