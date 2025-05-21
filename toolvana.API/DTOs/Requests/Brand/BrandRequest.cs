namespace toolvana.API.DTOs.Requests.Brand
{
    public class BrandRequest
    {
       
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile LogoUrl { get; set; }
        public bool Status { get; set; }
    }
}
