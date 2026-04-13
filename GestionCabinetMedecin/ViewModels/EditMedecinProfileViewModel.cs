using System.ComponentModel.DataAnnotations;

namespace GestionCabinetMedecin.ViewModels
{
    public class EditMedecinProfileViewModel
    {
        // -----------------------------
        // Nom du médecin
        // -----------------------------
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        public string? Nom { get; set; }
            
        // -----------------------------
        // Prénom du médecin
        // -----------------------------
        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        public string? Prenom { get; set; }

        // -----------------------------
        // Email du médecin
        // -----------------------------
        [Required(ErrorMessage = "L'email est obligatoire.")]
        [EmailAddress(ErrorMessage = "Email invalide.")]
        public string? Email { get; set; }

        // -----------------------------
        // Numéro de téléphone
        // -----------------------------
        [Phone(ErrorMessage = "Numéro invalide.")]
        public string? PhoneNumber { get; set; }
    }
}