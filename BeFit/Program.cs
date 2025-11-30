using BeFit.Data;
using BeFit.Models; 
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>() 
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();




using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        string adminEmail = "admin@befit.pl";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            var newAdmin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            var result = await userManager.CreateAsync(newAdmin, "BeFit123!");
            if (result.Succeeded) await userManager.AddToRoleAsync(newAdmin, "Admin");
        }

        
        if (!context.TypyCwiczen.Any())
        {
            var cwiczeniaSilowe = new List<TypCwiczenia>
            {
                new TypCwiczenia { Nazwa = "Wyciskanie sztangi leżąc", Opis = "Klatka piersiowa" },
                new TypCwiczenia { Nazwa = "Przysiad ze sztangą", Opis = "Nogi" },
                new TypCwiczenia { Nazwa = "Martwy ciąg", Opis = "Plecy/Nogi" },
                new TypCwiczenia { Nazwa = "Wyciskanie żołnierskie (OHP)", Opis = "Barki" },
                new TypCwiczenia { Nazwa = "Wiosłowanie sztangą", Opis = "Plecy" },
                new TypCwiczenia { Nazwa = "Podciąganie na drążku", Opis = "Plecy" },
                new TypCwiczenia { Nazwa = "Pompki na poręczach (Dipy)", Opis = "Triceps/Klatka" },
                new TypCwiczenia { Nazwa = "Uginanie ramion ze sztangą", Opis = "Biceps" },
                new TypCwiczenia { Nazwa = "Wyciskanie francuskie", Opis = "Triceps" },
                new TypCwiczenia { Nazwa = "Wznosy hantli bokiem", Opis = "Barki" },
                new TypCwiczenia { Nazwa = "Wykroki z hantlami", Opis = "Nogi" },
                new TypCwiczenia { Nazwa = "Leg Press (Suwnica)", Opis = "Nogi" },
                new TypCwiczenia { Nazwa = "Ściąganie drążka wyciągu górnego", Opis = "Plecy" },
                new TypCwiczenia { Nazwa = "Rozpiętki na ławce", Opis = "Klatka" },
                new TypCwiczenia { Nazwa = "Face Pull", Opis = "Barki (tył)" },
                new TypCwiczenia { Nazwa = "Plank (Deska)", Opis = "Brzuch" },
                new TypCwiczenia { Nazwa = "Allachy (Brzuszki na wyciągu)", Opis = "Brzuch" },
                new TypCwiczenia { Nazwa = "Łydki na maszynie", Opis = "Łydki" },
                new TypCwiczenia { Nazwa = "Hip Thrust", Opis = "Pośladki" },
                new TypCwiczenia { Nazwa = "Szrugsy (Kaptury)", Opis = "Plecy" }
            };

            var cwiczeniaCardio = new List<TypCwiczenia>
            {
                new TypCwiczenia { Nazwa = "Bieganie (Bieżnia)", Opis = "Cardio" },
                new TypCwiczenia { Nazwa = "Bieganie (Teren)", Opis = "Cardio" },
                new TypCwiczenia { Nazwa = "Rower stacjonarny", Opis = "Cardio" },
                new TypCwiczenia { Nazwa = "Orbitrek", Opis = "Cardio" },
                new TypCwiczenia { Nazwa = "Wioślarz (Ergometr)", Opis = "Cardio" },
                new TypCwiczenia { Nazwa = "Skakanka", Opis = "Cardio" },
                new TypCwiczenia { Nazwa = "Schody (Stair Master)", Opis = "Cardio" },
                new TypCwiczenia { Nazwa = "Pływanie", Opis = "Cardio" },
                new TypCwiczenia { Nazwa = "Spacer farmera", Opis = "Kondycja/Siła" },
                new TypCwiczenia { Nazwa = "Burpees", Opis = "Interwały" }
            };

            context.TypyCwiczen.AddRange(cwiczeniaSilowe);
            context.TypyCwiczen.AddRange(cwiczeniaCardio);
            await context.SaveChangesAsync();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Błąd podczas seedowania danych.");
    }
}


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();