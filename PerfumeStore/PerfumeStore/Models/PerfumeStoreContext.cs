using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PerfumeStore.Models;

// 1. ЗМІНА: Тепер ми успадковуємося від IdentityDbContext (це дає нам таблиці користувачів)
public class PerfumeStoreContext : IdentityDbContext<IdentityUser>
{
    public PerfumeStoreContext(DbContextOptions<PerfumeStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Perfume> Perfumes { get; set; }

    // ДОДАНО ДЛЯ КУРСОВОЇ: 4-та та 5-та сутності (Відгуки та Замовлення)
    public virtual DbSet<Review> Reviews { get; set; }
    public virtual DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 2. ЗМІНА: ОБОВ'ЯЗКОВО викликаємо базовий метод для створення таблиць Identity!
        base.OnModelCreating(modelBuilder);

        // Налаштування зв'язків для парфумів залишаються без змін
        modelBuilder.Entity<Perfume>(entity =>
        {
            entity.HasOne(d => d.Brand).WithMany(p => p.Perfumes)
                .HasForeignKey(d => d.BrandId);

            entity.HasOne(d => d.Category).WithMany(p => p.Perfumes)
                .HasForeignKey(d => d.CategoryId);
        });
    }
}