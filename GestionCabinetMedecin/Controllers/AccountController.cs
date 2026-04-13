using System.Security.Claims;
using GestionCabinetMedecin.data;
using GestionCabinetMedecin.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionCabinetMedecin.Controllers
{
    public class AccountController : Controller
    {
        // Injection du contexte de base de données
        private readonly CabinetDbContext _context;

        public AccountController(CabinetDbContext context)
        {
            _context = context;
        }

        // -----------------------------
        // Action GET : afficher la page Login
        // -----------------------------
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // -----------------------------
        // Action POST : traiter le formulaire Login
        // -----------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // Vérifie si les champs du formulaire sont valides
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Variable qui indique si l'utilisateur a été trouvé
            bool utilisateurValide = false;

            // Nom complet à stocker dans les claims
            string nomComplet = "";

            // -----------------------------
            // Vérification si le rôle choisi est Medecin
            // -----------------------------
            if (model.Role == "Medecin")
            {
                var medecin = await _context.Medecins
                    .FirstOrDefaultAsync(m =>
                        m.Email == model.Email &&
                        m.MotDePasse == model.MotDePasse);

                if (medecin != null)
                {
                    utilisateurValide = true;
                    nomComplet = medecin.Nom + " " + medecin.Prenom;
                }
            }

            // -----------------------------
            // Vérification si le rôle choisi est Secretaire
            // -----------------------------
            else if (model.Role == "Secretaire")
            {
                var secretaire = await _context.Secretaires
                    .FirstOrDefaultAsync(s =>
                        s.Email == model.Email &&
                        s.MotDePasse == model.MotDePasse);

                if (secretaire != null)
                {
                    utilisateurValide = true;
                    nomComplet = secretaire.SecretaireName + " " + secretaire.SecretaireSurname;
                }
            }

            // -----------------------------
            // Si aucun utilisateur correspondant n'a été trouvé
            // -----------------------------
            if (!utilisateurValide)
            {
                ModelState.AddModelError("", "Email, mot de passe ou rôle incorrect.");
                return View(model);
            }

            // -----------------------------
            // Création des informations d'authentification (claims)
            // -----------------------------
            var claims = new List<Claim>
            {
                // Nom affiché de l'utilisateur connecté
                new Claim(ClaimTypes.Name, nomComplet),

                // Email de l'utilisateur
                new Claim(ClaimTypes.Email, model.Email),

                // Rôle : Medecin ou Secretaire
                new Claim(ClaimTypes.Role, model.Role)
            };

            // Création de l'identité utilisateur basée sur les claims
            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            // Création du principal utilisateur
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Connexion réelle de l'utilisateur via cookie
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal
            );

            // Redirection après connexion réussie
            return RedirectToAction("Index", "Home");
        }

        // -----------------------------
        // Action Logout : déconnexion
        // -----------------------------
        public async Task<IActionResult> Logout()
        {
            // Supprime le cookie d'authentification
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirige vers la page Login
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