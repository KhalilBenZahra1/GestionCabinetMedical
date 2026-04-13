using GestionCabinetMedecin.data;
using GestionCabinetMedecin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<CabinetDbContext>()
    .AddDefaultTokenProviders();

// -----------------------------
// Ajout des services MVC
// -----------------------------
builder.Services.AddControllersWithViews();

// -----------------------------
// Ajout du DbContext avec SQL Server
// -----------------------------
builder.Services.AddDbContext<CabinetDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// -----------------------------
// Gestion des erreurs en production
// -----------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// -----------------------------
// Active l'authentification
// 🔐 Authentification (QUI est l'utilisateur)
// -----------------------------
app.UseAuthentication();

// -----------------------------
// Active l'autorisation
// 🔒 Autorisation (est-ce qu'il a le droit ?)
// -----------------------------
app.UseAuthorization();

// -----------------------------
// Fichiers statiques
// -----------------------------
app.MapStaticAssets();

// -----------------------------
// Route par défaut
// -----------------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// -----------------------------
// Création automatique des rôles et utilisateurs par défaut
// au démarrage de l'application
// -----------------------------
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // Récupération des services Identity
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    // Appel de la méthode d'initialisation
    await DbInitializer.SeedUsersAndRolesAsync(userManager, roleManager);
}

app.Run();