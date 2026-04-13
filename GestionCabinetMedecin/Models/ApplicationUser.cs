using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GestionCabinetMedecin.Models
{
    public class ApplicationUser : IdentityUser
    {
        // -----------------------------
        // Nom de l'utilisateur
        // -----------------------------
        [Required]
        public string? Nom { get; set; }

        // -----------------------------
        // Prénom de l'utilisateur
        // -----------------------------
        [Required]
        public string? Prenom { get; set; }

        // -----------------------------
        // Rôle logique personnalisé
        // (même si Identity gère déjà les rôles)
        // -----------------------------
        public string? RoleName { get; set; }
    }
}