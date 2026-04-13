using GestionCabinetMedecin.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// Ajout des services MVC
// -----------------------------
builder.Services.AddControllersWithViews();

// -----------------------------
// Ajout du DbContext avec SQL Server
// -----------------------------
builder.Services.AddDbContext<CabinetDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// -----------------------------
// Configuration de l'authentification par cookie
// -----------------------------
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // Page de login si l'utilisateur n'est pas connecté
        options.LoginPath = "/Account/Login";

        // Page si accès refusé
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

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
// -----------------------------
app.UseAuthentication();

// -----------------------------
// Active l'autorisation
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

app.Run();