using System.ComponentModel.DataAnnotations;

namespace GestionCabinetMedecin.ViewModels
{
    public class LoginViewModel
    {
        // Email saisi dans le formulaire
        [Required(ErrorMessage = "L'email est obligatoire.")]
        [EmailAddress(ErrorMessage = "Format d'email invalide.")]
        public string Email { get; set; }

        // Mot de passe saisi dans le formulaire
        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Permet de mémoriser la connexion si on veut plus tard
        public bool RememberMe { get; set; }
    }
}