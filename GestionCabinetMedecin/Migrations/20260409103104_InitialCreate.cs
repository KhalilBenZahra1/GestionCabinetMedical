using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionCabinetMedecin.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medecins",
                columns: table => new
                {
                    IdMedecin = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroTel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotDePasse = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medecins", x => x.IdMedecin);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    IdPatient = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientBirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sexe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroTel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.IdPatient);
                });

            migrationBuilder.CreateTable(
                name: "Secretaires",
                columns: table => new
                {
                    IdSecretaire = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SecretaireName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecretaireSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotDePasse = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secretaires", x => x.IdSecretaire);
                });

            migrationBuilder.CreateTable(
                name: "TypeTraitements",
                columns: table => new
                {
                    IdType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeTraitements", x => x.IdType);
                });

            migrationBuilder.CreateTable(
                name: "DossiersMedicaux",
                columns: table => new
                {
                    IdDossier = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdPatient = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DossiersMedicaux", x => x.IdDossier);
                    table.ForeignKey(
                        name: "FK_DossiersMedicaux_Patients_IdPatient",
                        column: x => x.IdPatient,
                        principalTable: "Patients",
                        principalColumn: "IdPatient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RendezVous",
                columns: table => new
                {
                    IdRdv = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Heure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Motif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    SecretaireId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RendezVous", x => x.IdRdv);
                    table.ForeignKey(
                        name: "FK_RendezVous_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "IdPatient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RendezVous_Secretaires_SecretaireId",
                        column: x => x.SecretaireId,
                        principalTable: "Secretaires",
                        principalColumn: "IdSecretaire",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    IdNotification = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contenu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateNotification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RendezVousId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.IdNotification);
                    table.ForeignKey(
                        name: "FK_Notifications_RendezVous_RendezVousId",
                        column: x => x.RendezVousId,
                        principalTable: "RendezVous",
                        principalColumn: "IdRdv",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ordonnances",
                columns: table => new
                {
                    IdOrdonnance = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOrdonnance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RendezVousId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordonnances", x => x.IdOrdonnance);
                    table.ForeignKey(
                        name: "FK_Ordonnances_RendezVous_RendezVousId",
                        column: x => x.RendezVousId,
                        principalTable: "RendezVous",
                        principalColumn: "IdRdv",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Traitements",
                columns: table => new
                {
                    IdTraitement = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrdonnaceId = table.Column<int>(type: "int", nullable: false),
                    TypeTraitementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traitements", x => x.IdTraitement);
                    table.ForeignKey(
                        name: "FK_Traitements_Ordonnances_OrdonnaceId",
                        column: x => x.OrdonnaceId,
                        principalTable: "Ordonnances",
                        principalColumn: "IdOrdonnance",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Traitements_TypeTraitements_TypeTraitementId",
                        column: x => x.TypeTraitementId,
                        principalTable: "TypeTraitements",
                        principalColumn: "IdType",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DossiersMedicaux_IdPatient",
                table: "DossiersMedicaux",
                column: "IdPatient",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_RendezVousId",
                table: "Notifications",
                column: "RendezVousId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordonnances_RendezVousId",
                table: "Ordonnances",
                column: "RendezVousId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RendezVous_PatientId",
                table: "RendezVous",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_RendezVous_SecretaireId",
                table: "RendezVous",
                column: "SecretaireId");

            migrationBuilder.CreateIndex(
                name: "IX_Traitements_OrdonnaceId",
                table: "Traitements",
                column: "OrdonnaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Traitements_TypeTraitementId",
                table: "Traitements",
                column: "TypeTraitementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DossiersMedicaux");

            migrationBuilder.DropTable(
                name: "Medecins");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Traitements");

            migrationBuilder.DropTable(
                name: "Ordonnances");

            migrationBuilder.DropTable(
                name: "TypeTraitements");

            migrationBuilder.DropTable(
                name: "RendezVous");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Secretaires");
        }
    }
}
