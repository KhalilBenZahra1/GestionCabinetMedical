using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionCabinetMedecin.Models
{
    public class Traitement
    {
        [Key]
        public int IdTraitement { get; set; }

        [Required]
        public string? Nom { get; set; }
        public string? Description { get; set; }

        [ForeignKey("Ordonnance")]
        public int OrdonnaceId { get; set; }
        public virtual Ordonnance? Ordonnance { get; set; }

        [ForeignKey("TypeTraitement")]
        public int TypeTraitementId { get; set; }
        public virtual TypeTraitement? TypeTraitement { get; set; }
    }
}
