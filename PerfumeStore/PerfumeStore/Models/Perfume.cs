namespace PerfumeStore.Models;

public class Perfume
{
    public int PerfumeId { get; set; }
    public string Name { get; set; } = null!;
    public int? BrandId { get; set; }
    public int? CategoryId { get; set; }
    public decimal Price { get; set; }
    public int? Volume { get; set; }
    public int? StockQuantity { get; set; }

    // Навігаційні властивості (для зв'язку в коді)
    public virtual Brand? Brand { get; set; }
    public virtual Category? Category { get; set; }
}