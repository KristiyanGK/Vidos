using Microsoft.EntityFrameworkCore.Migrations;

namespace Vidos.Data.Migrations
{
    public partial class BoughtCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimesBought",
                table: "AirConditioners",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimesBought",
                table: "AirConditioners");
        }
    }
}
