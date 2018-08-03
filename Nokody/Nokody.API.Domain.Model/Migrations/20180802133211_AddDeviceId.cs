using Microsoft.EntityFrameworkCore.Migrations;

namespace Nokody.API.Domain.Model.Migrations
{
    public partial class AddDeviceId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceId",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "User");
        }
    }
}
