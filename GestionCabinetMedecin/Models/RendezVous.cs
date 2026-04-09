using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionCabinetMedecin.Models
{
    public class RendezVous
    {
        [Key]
        public int IdRdv { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public string? Heure { get; set; }
        public string? Statut { get; set; }
        public string? Motif { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public virtual Patient? Patient { get; set; }

        [ForeignKey("Secretaire")]
        public int SecretaireId { get; set; }
        public virtual Secretaire? Secretaire { get; set; }

        public virtual ICollection <Notification> Notifications { get; set; } = new List<Notification>();
        public virtual Ordonnance? Ordonnace { get; set; }
    }
}
