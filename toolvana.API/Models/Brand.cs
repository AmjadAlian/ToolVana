namespace toolvana.API.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
      
        public ICollection<Product>? Products { get; set; }
        public double AverageRating => Products?.Where(p => p.Rating > 0).Average(p => p.Rating) ?? 0;


    }
}
