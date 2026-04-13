using System.ComponentModel.DataAnnotations;

namespace GestionCabinetMedecin.ViewModels
{
    public class EditSecretaireViewModel
    {
        // -----------------------------
        // Id de l'utilisateur Identity
        // -----------------------------
        public string? Id { get; set; }

        // -----------------------------
        // Nom
        // -----------------------------
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        public string? Nom { get; set; }

        // -----------------------------
        // Prénom
        // -----------------------------
        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        public string? Prenom { get; set; }

        // -----------------------------
        // Email
        // -----------------------------
        [Required(ErrorMessage = "L'email est obligatoire.")]
        [EmailAddress(ErrorMessage = "Email invalide.")]
        public string? Email { get; set; }

        // -----------------------------
        // Téléphone
        // -----------------------------
        [Phone(ErrorMessage = "Numéro invalide.")]
        public string? PhoneNumber { get; set; }
    }
}