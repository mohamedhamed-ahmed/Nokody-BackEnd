using Microsoft.EntityFrameworkCore.Migrations;

namespace Nokody.API.Domain.Model.Migrations
{
    public partial class updating_account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BraceletNumber",
                table: "Account",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportNumber",
                table: "Account",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BraceletNumber",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "PassportNumber",
                table: "Account");
        }
    }
}
