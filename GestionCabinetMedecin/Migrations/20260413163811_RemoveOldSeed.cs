using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionCabinetMedecin.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOldSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Medecins",
                keyColumn: "IdMedecin",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Secretaires",
                keyColumn: "IdSecretaire",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Medecins",
                columns: new[] { "IdMedecin", "Email", "MotDePasse", "Nom", "NumeroTel", "Prenom" },
                values: new object[] { 1, "kb6004710@gmail.com", "1234", "Khalil", "1234", "Doctor" });

            migrationBuilder.InsertData(
                table: "Secretaires",
                columns: new[] { "IdSecretaire", "Email", "MotDePasse", "Phone", "SecretaireName", "SecretaireSurname" },
                values: new object[] { 1, "benzahra.khalil@yahoo.com", "1234", "5678", "Sara", "Ali" });
        }
    }
}
