namespace PerfumeStore.Models;

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = null!;
    // Зв'язок: одна категорія має багато парфумів
    public virtual ICollection<Perfume> Perfumes { get; set; } = new List<Perfume>();
}