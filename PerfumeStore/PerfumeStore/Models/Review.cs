namespace PerfumeStore.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int PerfumeId { get; set; }
        public virtual Perfume Perfume { get; set; }

        public string AuthorName { get; set; }
        public int Rating { get; set; } // Оцінка від 1 до 5
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}