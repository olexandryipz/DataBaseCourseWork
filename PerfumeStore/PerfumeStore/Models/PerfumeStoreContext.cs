using Microsoft.EntityFrameworkCore;

namespace PerfumeStore.Models;

public class PerfumeStoreContext : DbContext
{
    public PerfumeStoreContext(DbContextOptions<PerfumeStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Perfume> Perfumes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Вказуємо EF Core, як наші класи пов'язані з таблицями в SQL
        modelBuilder.Entity<Perfume>(entity =>
        {
            entity.HasOne(d => d.Brand).WithMany(p => p.Perfumes)
                .HasForeignKey(d => d.BrandId);

            entity.HasOne(d => d.Category).WithMany(p => p.Perfumes)
                .HasForeignKey(d => d.CategoryId);
        });
    }
}