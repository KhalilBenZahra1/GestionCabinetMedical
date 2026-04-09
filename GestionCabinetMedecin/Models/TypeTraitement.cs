using System.ComponentModel.DataAnnotations;

namespace GestionCabinetMedecin.Models
{
    public class TypeTraitement
    {
        [Key]
        public int IdType { get; set; }

        [Required]
        public string? Nom { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Traitement>? Traitements { get; set; } = new List<Traitement>();
    }
}
