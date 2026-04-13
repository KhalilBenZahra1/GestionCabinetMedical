using System.ComponentModel.DataAnnotations;

namespace GestionCabinetMedecin.ViewModels
{
    public class ChangePasswordViewModel
    {
        // -----------------------------
        // Ancien mot de passe saisi par l'utilisateur
        // -----------------------------
        [Required(ErrorMessage = "L'ancien mot de passe est obligatoire.")]
        [DataType(DataType.Password)]
        public string? OldPassword { get; set; }

        // -----------------------------
        // Nouveau mot de passe
        // -----------------------------
        [Required(ErrorMessage = "Le nouveau mot de passe est obligatoire.")]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        // -----------------------------
        // Confirmation du nouveau mot de passe
        // -----------------------------
        [Required(ErrorMessage = "La confirmation du mot de passe est obligatoire.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "La confirmation ne correspond pas au nouveau mot de passe.")]
        public string? ConfirmNewPassword { get; set; }
    }
}