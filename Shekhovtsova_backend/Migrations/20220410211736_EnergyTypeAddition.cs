using Microsoft.EntityFrameworkCore.Migrations;

namespace Shekhovtsova_backend.Migrations
{
    public partial class EnergyTypeAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Energies",
                columns: new[] { "EnergyID", "EcologyDamage", "Type" },
                values: new object[] { 1, 6, 0 });

            migrationBuilder.InsertData(
                table: "Energies",
                columns: new[] { "EnergyID", "EcologyDamage", "Type" },
                values: new object[] { 2, 3, 1 });

            migrationBuilder.InsertData(
                table: "Energies",
                columns: new[] { "EnergyID", "EcologyDamage", "Type" },
                values: new object[] { 3, 10, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Energies",
                keyColumn: "EnergyID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Energies",
                keyColumn: "EnergyID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Energies",
                keyColumn: "EnergyID",
                keyValue: 3);
        }
    }
}
