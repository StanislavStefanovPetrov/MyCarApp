using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCarApp.Data.Migrations
{
    public partial class ChangePictureExtensionName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PicturePaths_PictureValidExtentions_ExtentionId",
                table: "PicturePaths");

            migrationBuilder.DropTable(
                name: "PictureValidExtentions");

            migrationBuilder.RenameColumn(
                name: "ExtentionId",
                table: "PicturePaths",
                newName: "ExtensionId");

            migrationBuilder.RenameIndex(
                name: "IX_PicturePaths_ExtentionId",
                table: "PicturePaths",
                newName: "IX_PicturePaths_ExtensionId");

            migrationBuilder.CreateTable(
                name: "PictureValidExtensions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Extension = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PictureValidExtensions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PictureValidExtensions_Extension",
                table: "PictureValidExtensions",
                column: "Extension",
                unique: true,
                filter: "[Extension] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PicturePaths_PictureValidExtensions_ExtensionId",
                table: "PicturePaths",
                column: "ExtensionId",
                principalTable: "PictureValidExtensions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PicturePaths_PictureValidExtensions_ExtensionId",
                table: "PicturePaths");

            migrationBuilder.DropTable(
                name: "PictureValidExtensions");

            migrationBuilder.RenameColumn(
                name: "ExtensionId",
                table: "PicturePaths",
                newName: "ExtentionId");

            migrationBuilder.RenameIndex(
                name: "IX_PicturePaths_ExtensionId",
                table: "PicturePaths",
                newName: "IX_PicturePaths_ExtentionId");

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
    }
}
