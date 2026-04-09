using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionCabinetMedecin.Models
{
    public class DossierMedical
    {
        [Key]
        public int IdDossier { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateCreation { get; set; } = DateTime.Now;
        public string ? Description { get; set; }
        public string ? Observation { get; set; }

        [ForeignKey("Patient")]
        public int IdPatient { get; set; }
        public virtual Patient? Patient { get; set; }
    }
}
