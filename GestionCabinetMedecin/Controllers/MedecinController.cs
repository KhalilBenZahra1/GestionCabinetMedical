using GestionCabinetMedecin.Models;
using GestionCabinetMedecin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GestionCabinetMedecin.Controllers
{
    // -----------------------------
    // Seul le médecin peut accéder à ce controller
    // -----------------------------
    [Authorize(Roles = "Medecin")]
    public class MedecinController : Controller
    {
        // -----------------------------
        // Service de gestion des utilisateurs Identity
        // -----------------------------
        private readonly UserManager<ApplicationUser> _userManager;

        public MedecinController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // =========================================================
        // 1) GESTION DU PROPRE COMPTE DU MEDECIN
        // =========================================================

        // -----------------------------
        // GET : afficher le formulaire MonCompte
        // -----------------------------
        [HttpGet]
        public async Task<IActionResult> MonCompte()
        {
            // Récupérer l'utilisateur actuellement connecté
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            // Remplir le ViewModel avec les données actuelles
            var model = new EditMedecinProfileViewModel
            {
                Nom = user.Nom,
                Prenom = user.Prenom,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber
            };

            return View(model);
        }

        // -----------------------------
        // POST : enregistrer les modifications du compte médecin
        // -----------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MonCompte(EditMedecinProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Récupérer l'utilisateur connecté
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            // Mettre à jour les propriétés
            user.Nom = model.Nom;
            user.Prenom = model.Prenom;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            // Sauvegarder les modifications
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                ViewBag.Success = "Compte médecin modifié avec succès.";
                return View(model);
            }

            // Afficher les erreurs Identity s'il y en a
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        // =========================================================
        // 2) LISTE DES SECRETAIRES
        // =========================================================

        // -----------------------------
        // GET : afficher tous les comptes secrétaire
        // -----------------------------
        [HttpGet]
        public async Task<IActionResult> ListeSecretaires()
        {
            // Récupérer tous les utilisateurs
            var users = _userManager.Users.ToList();

            // Liste qui contiendra uniquement les secrétaires
            var secretaires = new List<ApplicationUser>();

            foreach (var user in users)
            {
                // Vérifie si l'utilisateur a le rôle Secretaire
                if (await _userManager.IsInRoleAsync(user, "Secretaire"))
                {
                    secretaires.Add(user);
                }
            }

            return View(secretaires);
        }

        // =========================================================
        // 3) AJOUTER UNE SECRETAIRE
        // =========================================================

        // -----------------------------
        // GET : afficher le formulaire d'ajout
        // -----------------------------
        [HttpGet]
        public IActionResult AjouterSecretaire()
        {
            return View();
        }

        // -----------------------------
        // POST : enregistrer une nouvelle secrétaire
        // -----------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjouterSecretaire(CreateSecretaireViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Vérifie si un utilisateur avec cet email existe déjà
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Un utilisateur avec cet email existe déjà.");
                return View(model);
            }

            // Création du nouvel utilisateur secrétaire
            var secretaire = new ApplicationUser
            {
                Nom = model.Nom,
                Prenom = model.Prenom,
                Email = model.Email,
                UserName = model.Email,
                PhoneNumber = model.PhoneNumber,
                RoleName = "Secretaire",
                EmailConfirmed = true
            };

            // Création avec hash automatique du mot de passe
            var result = await _userManager.CreateAsync(secretaire, model.Password);

            if (result.Succeeded)
            {
                // Ajouter le rôle Secretaire
                await _userManager.AddToRoleAsync(secretaire, "Secretaire");

                return RedirectToAction(nameof(ListeSecretaires));
            }

            // Affichage des erreurs
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        // =========================================================
        // 4) MODIFIER UNE SECRETAIRE
        // =========================================================

        // -----------------------------
        // GET : afficher le formulaire de modification
        // -----------------------------
        [HttpGet]
        public async Task<IActionResult> ModifierSecretaire(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secretaire = await _userManager.FindByIdAsync(id);

            if (secretaire == null || !await _userManager.IsInRoleAsync(secretaire, "Secretaire"))
            {
                return NotFound();
            }

            var model = new EditSecretaireViewModel
            {
                Id = secretaire.Id,
                Nom = secretaire.Nom,
                Prenom = secretaire.Prenom,
                Email = secretaire.Email!,
                PhoneNumber = secretaire.PhoneNumber
            };

            return View(model);
        }

        // -----------------------------
        // POST : enregistrer la modification
        // -----------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifierSecretaire(EditSecretaireViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var secretaire = await _userManager.FindByIdAsync(model.Id);

            if (secretaire == null || !await _userManager.IsInRoleAsync(secretaire, "Secretaire"))
            {
                return NotFound();
            }

            secretaire.Nom = model.Nom;
            secretaire.Prenom = model.Prenom;
            secretaire.Email = model.Email;
            secretaire.UserName = model.Email;
            secretaire.PhoneNumber = model.PhoneNumber;

            var result = await _userManager.UpdateAsync(secretaire);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ListeSecretaires));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        // =========================================================
        // 5) SUPPRIMER UNE SECRETAIRE
        // =========================================================

        // -----------------------------
        // GET : page de confirmation suppression
        // -----------------------------
        [HttpGet]
        public async Task<IActionResult> SupprimerSecretaire(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secretaire = await _userManager.FindByIdAsync(id);

            if (secretaire == null || !await _userManager.IsInRoleAsync(secretaire, "Secretaire"))
            {
                return NotFound();
            }

            return View(secretaire);
        }

        // -----------------------------
        // POST : suppression confirmée
        // -----------------------------
        [HttpPost, ActionName("SupprimerSecretaire")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SupprimerSecretaireConfirmed(string id)
        {
            var secretaire = await _userManager.FindByIdAsync(id);

            if (secretaire == null || !await _userManager.IsInRoleAsync(secretaire, "Secretaire"))
            {
                return NotFound();
            }

            await _userManager.DeleteAsync(secretaire);

            return RedirectToAction(nameof(ListeSecretaires));
        }
    }
}