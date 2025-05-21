using toolvana.API.Models;

namespace toolvana.API.DTOs.Requests.Product
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile MainImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public double Rating { get; set; }
        public int Stock { get; set; }
        public bool Status { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }

    }
}
