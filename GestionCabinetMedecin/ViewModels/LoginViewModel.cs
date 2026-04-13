using System.ComponentModel.DataAnnotations;

namespace GestionCabinetMedecin.ViewModels
{
    public class LoginViewModel
    {
        // Email saisi par l'utilisateur
        [Required(ErrorMessage = "L'email est obligatoire.")]
        [EmailAddress(ErrorMessage = "Format d'email invalide.")]
        public string Email { get; set; }

        // Mot de passe saisi par l'utilisateur
        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [DataType(DataType.Password)]
        public string MotDePasse { get; set; }

        // Rôle choisi dans le formulaire : Medecin ou Secretaire
        [Required(ErrorMessage = "Le rôle est obligatoire.")]
        public string Role { get; set; }
    }
}