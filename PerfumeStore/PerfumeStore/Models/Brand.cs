namespace PerfumeStore.Models;

public class Brand
{
    public int BrandId { get; set; }
    public string Name { get; set; } = null!;
    public virtual ICollection<Perfume> Perfumes { get; set; } = new List<Perfume>();
}