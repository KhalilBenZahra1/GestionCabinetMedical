using GestionCabinetMedecin.Models;
using GestionCabinetMedecin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GestionCabinetMedecin.Controllers
{
    // -----------------------------
    // Seule une secrétaire connectée peut accéder à ce controller
    // -----------------------------
    [Authorize(Roles = "Secretaire")]
    public class SecretaireController : Controller
    {
        // -----------------------------
        // Service Identity pour gérer l'utilisateur connecté
        // -----------------------------
        private readonly UserManager<ApplicationUser> _userManager;

        public SecretaireController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // -----------------------------
        // GET : afficher la page changement mot de passe
        // -----------------------------
        [HttpGet]
        public IActionResult ChangerMotDePasse()
        {
            return View();
        }

        // -----------------------------
        // POST : changer le mot de passe de la secrétaire connectée
        // -----------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangerMotDePasse(ChangePasswordViewModel model)
        {
            // Vérifie la validité des champs
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Récupère la secrétaire connectée
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            // Change le mot de passe en vérifiant l'ancien
            var result = await _userManager.ChangePasswordAsync(
                user,
                model.OldPassword,
                model.NewPassword
            );

            // Si succès
            if (result.Succeeded)
            {
                ViewBag.Success = "Mot de passe modifié avec succès.";
                return View();
            }

            // Sinon afficher les erreurs
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
    }
}