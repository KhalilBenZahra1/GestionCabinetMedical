using System.ComponentModel.DataAnnotations;

namespace GestionCabinetMedecin.Models
{
    public class Secretaire
    {
        [Key] public int IdSecretaire { get; set; }

        [Required] public string ? SecretaireName { get; set; }
        [Required] public string? SecretaireSurname { get; set; }
        [EmailAddress] public string? Email { get; set; }
        [Phone][Required] public string? Phone { get; set; }
        [Required] public string? MotDePasse { get; set; }
        public virtual ICollection<RendezVous>? RendezVous { get; set; } = new List<RendezVous>();
    }
}
