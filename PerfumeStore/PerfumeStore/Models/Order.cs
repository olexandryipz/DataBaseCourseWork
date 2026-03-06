namespace PerfumeStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int PerfumeId { get; set; }
        public virtual Perfume Perfume { get; set; }

        public string CustomerName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}