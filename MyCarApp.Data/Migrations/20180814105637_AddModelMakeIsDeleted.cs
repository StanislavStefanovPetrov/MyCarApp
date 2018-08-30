using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCarApp.Data.Migrations
{
    public partial class AddModelMakeIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "VehicleModels",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "VehicleMakes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "VehicleMakes");
        }
    }
}
