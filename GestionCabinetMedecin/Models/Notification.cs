using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionCabinetMedecin.Models
{
    public class Notification
    {
        [Key]
        public int IdNotification { get; set; }

        [Required]
        public string? Contenu { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateNotification { get; set; }
        public string? Statut { get; set; }

        [ForeignKey("RendezVous")]
        public int RendezVousId { get; set; }
        public virtual RendezVous? RendezVous { get; set; }


    }
}
