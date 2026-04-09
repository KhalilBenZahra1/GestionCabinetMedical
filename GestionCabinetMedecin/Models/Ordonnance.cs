using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionCabinetMedecin.Models
{
    public class Ordonnance
    {
        [Key]
        public int IdOrdonnance { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOrdonnance { get; set; }

        [ForeignKey("RendezVous")]
        public int RendezVousId { get; set; }
        public virtual RendezVous? RendezVous { get; set; }

        public virtual ICollection<Traitement> Traitements { get; set; } = new List<Traitement>();
    }
}
