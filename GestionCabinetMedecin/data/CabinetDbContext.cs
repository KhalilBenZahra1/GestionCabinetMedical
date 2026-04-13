using Microsoft.EntityFrameworkCore;
using GestionCabinetMedecin.Models;

namespace GestionCabinetMedecin.data
{
    public class CabinetDbContext : DbContext
    {
        public CabinetDbContext(DbContextOptions<CabinetDbContext> options) : base(options)
        {
        }

        // -----------------------------
        // Tables de la base de données
        // -----------------------------
        public DbSet<Patient> Patients { get; set; }
        public DbSet<DossierMedical> DossiersMedicaux { get; set; }
        public DbSet<RendezVous> RendezVous { get; set; }
        public DbSet<Secretaire> Secretaires { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Ordonnance> Ordonnances { get; set; }
        public DbSet<Traitement> Traitements { get; set; }
        public DbSet<TypeTraitement> TypeTraitements { get; set; }
        public DbSet<Medecin> Medecins { get; set; }

        // -----------------------------
        // Méthode pour insérer des données par défaut (Seed Data)
        // -----------------------------
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -----------------------------
            // Médecin par défaut
            // -----------------------------
            modelBuilder.Entity<Medecin>().HasData(
                new Medecin
                {
                    IdMedecin = 1,
                    Nom = "Khalil",
                    Prenom = "Doctor",
                    Email = "kb6004710@gmail.com",
                    NumeroTel = "1234",
                    MotDePasse = "1234"
                }
            );

            // -----------------------------
            // Secrétaire par défaut
            // -----------------------------
            modelBuilder.Entity<Secretaire>().HasData(
                new Secretaire
                {
                    IdSecretaire = 1,
                    SecretaireName = "Sara",
                    SecretaireSurname = "Ali",
                    Email = "benzahra.khalil@yahoo.com",
                    Phone = "5678",
                    MotDePasse = "1234"
                }
            );
        }
    }
}