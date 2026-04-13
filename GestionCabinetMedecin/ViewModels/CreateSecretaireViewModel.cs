using System.ComponentModel.DataAnnotations;

namespace GestionCabinetMedecin.ViewModels
{
    public class CreateSecretaireViewModel
    {
        // -----------------------------
        // Nom de la secrétaire
        // -----------------------------
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        public string? Nom { get; set; }

        // -----------------------------
        // Prénom de la secrétaire
        // -----------------------------
        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        public string? Prenom { get; set; }

        // -----------------------------
        // Email de connexion
        // -----------------------------
        [Required(ErrorMessage = "L'email est obligatoire.")]
        [EmailAddress(ErrorMessage = "Email invalide.")]
        public string? Email { get; set; }

        // -----------------------------
        // Téléphone
        // -----------------------------
        [Phone(ErrorMessage = "Numéro invalide.")]
        public string? PhoneNumber { get; set; }

        // -----------------------------
        // Mot de passe initial
        // -----------------------------
        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        // -----------------------------
        // Confirmation du mot de passe
        // -----------------------------
        [Required(ErrorMessage = "La confirmation est obligatoire.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas.")]
        public string? ConfirmPassword { get; set; }
    }
}