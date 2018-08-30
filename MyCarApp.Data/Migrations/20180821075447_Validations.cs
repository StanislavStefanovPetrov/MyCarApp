using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCarApp.Data.Migrations
{
    public partial class Validations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VehicleTypes_Type",
                table: "VehicleTypes");

            migrationBuilder.DropIndex(
                name: "IX_VehicleMakes_Name",
                table: "VehicleMakes");

            migrationBuilder.DropIndex(
                name: "IX_VehicleConditions_Condition",
                table: "VehicleConditions");

            migrationBuilder.DropIndex(
                name: "IX_PictureValidExtensions_Extension",
                table: "PictureValidExtensions");

            migrationBuilder.DropIndex(
                name: "IX_PicturePaths_Path",
                table: "PicturePaths");

            migrationBuilder.DropIndex(
                name: "IX_FuelTypes_Fuel",
                table: "FuelTypes");

            migrationBuilder.DropIndex(
                name: "IX_Colours_Name",
                table: "Colours");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "VehicleTypes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "VehicleModels",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "VehicleMakes",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Condition",
                table: "VehicleConditions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Transmissions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Extension",
                table: "PictureValidExtensions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "PicturePaths",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Fuel",
                table: "FuelTypes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Colours",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Advertisements",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTypes_Type",
                table: "VehicleTypes",
                column: "Type",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMakes_Name",
                table: "VehicleMakes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleConditions_Condition",
                table: "VehicleConditions",
                column: "Condition",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PictureValidExtensions_Extension",
                table: "PictureValidExtensions",
                column: "Extension",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PicturePaths_Path",
                table: "PicturePaths",
                column: "Path",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FuelTypes_Fuel",
                table: "FuelTypes",
                column: "Fuel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Colours_Name",
                table: "Colours",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VehicleTypes_Type",
                table: "VehicleTypes");

            migrationBuilder.DropIndex(
                name: "IX_VehicleMakes_Name",
                table: "VehicleMakes");

            migrationBuilder.DropIndex(
                name: "IX_VehicleConditions_Condition",
                table: "VehicleConditions");

            migrationBuilder.DropIndex(
                name: "IX_PictureValidExtensions_Extension",
                table: "PictureValidExtensions");

            migrationBuilder.DropIndex(
                name: "IX_PicturePaths_Path",
                table: "PicturePaths");

            migrationBuilder.DropIndex(
                name: "IX_FuelTypes_Fuel",
                table: "FuelTypes");

            migrationBuilder.DropIndex(
                name: "IX_Colours_Name",
                table: "Colours");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "VehicleTypes",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "VehicleModels",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "VehicleMakes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Condition",
                table: "VehicleConditions",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Transmissions",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Extension",
                table: "PictureValidExtensions",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "PicturePaths",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Fuel",
                table: "FuelTypes",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Colours",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Advertisements",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTypes_Type",
                table: "VehicleTypes",
                column: "Type",
                unique: true,
                filter: "[Type] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMakes_Name",
                table: "VehicleMakes",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleConditions_Condition",
                table: "VehicleConditions",
                column: "Condition",
                unique: true,
                filter: "[Condition] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PictureValidExtensions_Extension",
                table: "PictureValidExtensions",
                column: "Extension",
                unique: true,
                filter: "[Extension] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PicturePaths_Path",
                table: "PicturePaths",
                column: "Path",
                unique: true,
                filter: "[Path] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FuelTypes_Fuel",
                table: "FuelTypes",
                column: "Fuel",
                unique: true,
                filter: "[Fuel] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Colours_Name",
                table: "Colours",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }
    }
}
