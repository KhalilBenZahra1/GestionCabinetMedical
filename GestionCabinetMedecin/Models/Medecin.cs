using System.ComponentModel.DataAnnotations;

namespace GestionCabinetMedecin.Models
{
    public class Medecin
    {
        [Key]
        public int IdMedecin { get; set; }

        [Required]
        public string? Nom { get; set; }

        [Required]
        public string? Prenom { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string ? NumeroTel { get; set; }

        [Required]
        public string? MotDePasse { get; set; }

    }
}
