using Microsoft.AspNetCore.Identity;

namespace GestionCabinetMedecin.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Rôle logique personnalisé affiché dans l'application
        public string? RoleName { get; set; }
    }
}