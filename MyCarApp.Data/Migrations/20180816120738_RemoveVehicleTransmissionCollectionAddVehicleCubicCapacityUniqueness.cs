using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCarApp.Data.Migrations
{
    public partial class RemoveVehicleTransmissionCollectionAddVehicleCubicCapacityUniqueness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleTransmissions");

            migrationBuilder.AddColumn<int>(
                name: "TransmissionId",
                table: "Vehicles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_TransmissionId",
                table: "Vehicles",
                column: "TransmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Engines_CubicCapacity",
                table: "Engines",
                column: "CubicCapacity",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Transmissions_TransmissionId",
                table: "Vehicles",
                column: "TransmissionId",
                principalTable: "Transmissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Transmissions_TransmissionId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_TransmissionId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Engines_CubicCapacity",
                table: "Engines");

            migrationBuilder.DropColumn(
                name: "TransmissionId",
                table: "Vehicles");

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
    }
}
