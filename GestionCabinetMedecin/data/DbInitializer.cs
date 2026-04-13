using GestionCabinetMedecin.Models;
using Microsoft.AspNetCore.Identity;

namespace GestionCabinetMedecin.data
{
    public static class DbInitializer
    {
        // -----------------------------
        // Méthode appelée au démarrage de l'application
        // Elle crée les rôles et les utilisateurs par défaut
        // -----------------------------
        public static async Task SeedUsersAndRolesAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            // -----------------------------
            // 1) Créer le rôle Medecin s'il n'existe pas
            // -----------------------------
            if (!await roleManager.RoleExistsAsync("Medecin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Medecin"));
            }

            // -----------------------------
            // 2) Créer le rôle Secretaire s'il n'existe pas
            // -----------------------------
            if (!await roleManager.RoleExistsAsync("Secretaire"))
            {
                await roleManager.CreateAsync(new IdentityRole("Secretaire"));
            }

            // -----------------------------
            // 3) Vérifier si le compte médecin existe déjà
            // -----------------------------
            var medecinEmail = "medecin@cabinet.com";
            var medecinUser = await userManager.FindByEmailAsync(medecinEmail);

            if (medecinUser == null)
            {
                // Création de l'utilisateur médecin
                medecinUser = new ApplicationUser
                {
                    UserName = medecinEmail,
                    Email = medecinEmail,
                    RoleName = "Medecin",
                    EmailConfirmed = true
                };

                // Le mot de passe sera hashé automatiquement par Identity
                var result = await userManager.CreateAsync(medecinUser, "Medecin123!");

                // Si l'utilisateur est créé avec succès, on lui ajoute le rôle
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(medecinUser, "Medecin");
                }
            }

            // -----------------------------
            // 4) Vérifier si le compte secrétaire existe déjà
            // -----------------------------
            var secretaireEmail = "secretaire@cabinet.com";
            var secretaireUser = await userManager.FindByEmailAsync(secretaireEmail);

            if (secretaireUser == null)
            {
                // Création de l'utilisateur secrétaire
                secretaireUser = new ApplicationUser
                {
                    UserName = secretaireEmail,
                    Email = secretaireEmail,
                    RoleName = "Secretaire",
                    EmailConfirmed = true
                };

                // Le mot de passe sera hashé automatiquement par Identity
                var result = await userManager.CreateAsync(secretaireUser, "Secretaire123!");

                // Si l'utilisateur est créé avec succès, on lui ajoute le rôle
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(secretaireUser, "Secretaire");
                }
            }
        }
    }
}