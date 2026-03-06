using Microsoft.EntityFrameworkCore;
using PerfumeStore.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Підключення до нашої робочої бази даних
builder.Services.AddDbContext<PerfumeStoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Налаштування системи реєстрації (Identity) ТА РОЛЕЙ
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>() // <--- ОСЬ ЦЕЙ РЯДОК ДОДАЄ ПІДТРИМКУ РОЛЕЙ (АДМІНА)
    .AddEntityFrameworkStores<PerfumeStoreContext>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// ОБОВ'ЯЗКОВО: Перевірка, хто зайшов (Authentication), має бути перед правами доступу (Authorization)
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// ОБОВ'ЯЗКОВО: Дозволяємо програмі відкривати сторінки Login та Register
app.MapRazorPages();

app.Run();