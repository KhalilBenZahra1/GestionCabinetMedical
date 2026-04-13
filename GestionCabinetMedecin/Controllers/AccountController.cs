using GestionCabinetMedecin.Models;
using GestionCabinetMedecin.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GestionCabinetMedecin.Controllers
{
    public class AccountController : Controller
    {
        // Service Identity pour connecter / déconnecter un utilisateur
        private readonly SignInManager<ApplicationUser> _signInManager;

        // Service Identity pour accéder aux utilisateurs
        private readonly UserManager<ApplicationUser> _userManager;

        // Constructeur : injection des services Identity
        public AccountController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // -----------------------------
        // GET : afficher la page Login
        // -----------------------------
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // -----------------------------
        // POST : traiter le formulaire de connexion
        // -----------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // Vérifie si les données du formulaire sont valides
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Recherche l'utilisateur par email
            var user = await _userManager.FindByEmailAsync(model.Email);

            // Si l'utilisateur n'existe pas
            if (user == null)
            {
                ModelState.AddModelError("", "Email ou mot de passe incorrect.");
                return View(model);
            }

            // Tentative de connexion sécurisée avec Identity
            var result = await _signInManager.PasswordSignInAsync(
                user.UserName!,          // nom d'utilisateur
                model.Password,          // mot de passe saisi
                model.RememberMe,        // mémoriser ou non la connexion
                false                    // verrouillage désactivé pour l'instant
            );

            // Si la connexion réussit
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            // Si la connexion échoue
            ModelState.AddModelError("", "Email ou mot de passe incorrect.");
            return View(model);
        }

        // -----------------------------
        // Déconnexion
        // -----------------------------
        public async Task<IActionResult> Logout()
        {
            // Supprime le cookie d'authentification
            await _signInManager.SignOutAsync();

            // Retour vers la page Login
            return RedirectToAction("Login", "Account");
        }

        // -----------------------------
        // Page accès refusé
        // -----------------------------
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}