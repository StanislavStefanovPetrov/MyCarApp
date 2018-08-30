using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCarApp.Data.Migrations
{
    public partial class VehicleTransmissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EngineTransmissions");

            migrationBuilder.CreateTable(
                name: "VehicleTransmissions",
                columns: table => new
                {
                    VehicleId = table.Column<int>(nullable: false),
                    TransmissionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTransmissions", x => new { x.VehicleId, x.TransmissionId });
                    table.ForeignKey(
                        name: "FK_VehicleTransmissions_Transmissions_TransmissionId",
                        column: x => x.TransmissionId,
                        principalTable: "Transmissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleTransmissions_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTransmissions_TransmissionId",
                table: "VehicleTransmissions",
                column: "TransmissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleTransmissions");

            migrationBuilder.CreateTable(
                name: "EngineTransmissions",
                columns: table => new
                {
                    EngineId = table.Column<int>(nullable: false),
                    TransmissionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineTransmissions", x => new { x.EngineId, x.TransmissionId });
                    table.ForeignKey(
                        name: "FK_EngineTransmissions_Engines_EngineId",
                        column: x => x.EngineId,
                        principalTable: "Engines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EngineTransmissions_Transmissions_TransmissionId",
                        column: x => x.TransmissionId,
                        principalTable: "Transmissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EngineTransmissions_TransmissionId",
                table: "EngineTransmissions",
                column: "TransmissionId");
        }
    }
}
