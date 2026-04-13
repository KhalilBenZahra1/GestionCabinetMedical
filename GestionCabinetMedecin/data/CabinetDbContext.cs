using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GestionCabinetMedecin.Models;

namespace GestionCabinetMedecin.data
{
    public class CabinetDbContext : IdentityDbContext<ApplicationUser>
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

    }
}