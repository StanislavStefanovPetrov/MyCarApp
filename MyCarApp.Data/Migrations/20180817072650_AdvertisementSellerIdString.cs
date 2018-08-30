using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCarApp.Data.Migrations
{
    public partial class AdvertisementSellerIdString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_AspNetUsers_SellerId1",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_SellerId1",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "SellerId1",
                table: "Advertisements");

            migrationBuilder.AlterColumn<string>(
                name: "SellerId",
                table: "Advertisements",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_SellerId",
                table: "Advertisements",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_AspNetUsers_SellerId",
                table: "Advertisements",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_AspNetUsers_SellerId",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_SellerId",
                table: "Advertisements");

            migrationBuilder.AlterColumn<int>(
                name: "SellerId",
                table: "Advertisements",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerId1",
                table: "Advertisements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_SellerId1",
                table: "Advertisements",
                column: "SellerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_AspNetUsers_SellerId1",
                table: "Advertisements",
                column: "SellerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
