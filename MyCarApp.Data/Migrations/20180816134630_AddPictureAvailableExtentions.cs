using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCarApp.Data.Migrations
{
    public partial class AddPictureAvailableExtentions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExtentionId",
                table: "PicturePaths",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PictureValidExtentions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Extention = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PictureValidExtentions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PicturePaths_ExtentionId",
                table: "PicturePaths",
                column: "ExtentionId");

            migrationBuilder.CreateIndex(
                name: "IX_PictureValidExtentions_Extention",
                table: "PictureValidExtentions",
                column: "Extention",
                unique: true,
                filter: "[Extention] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PicturePaths_PictureValidExtentions_ExtentionId",
                table: "PicturePaths",
                column: "ExtentionId",
                principalTable: "PictureValidExtentions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PicturePaths_PictureValidExtentions_ExtentionId",
                table: "PicturePaths");

            migrationBuilder.DropTable(
                name: "PictureValidExtentions");

            migrationBuilder.DropIndex(
                name: "IX_PicturePaths_ExtentionId",
                table: "PicturePaths");

            migrationBuilder.DropColumn(
                name: "ExtentionId",
                table: "PicturePaths");
        }
    }
}
