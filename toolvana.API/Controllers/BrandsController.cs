using Mapster;
using Microsoft.AspNetCore.Mvc;
using toolvana.API.Data;
using toolvana.API.DTOs.Requests.Brand;
using toolvana.API.DTOs.Responses.Brand;
using toolvana.API.Models;
using toolvana.API.Services.Brands;

namespace toolvana.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService brandService;

        public BrandsController(IBrandService brandService)
        {
            this.brandService = brandService;
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var brand = brandService.GetBrand(b =>b.Id == id);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(brand.Adapt<BrandResponse>());
        }
        [HttpPost("")]
        public IActionResult CreateBrand([FromForm] BrandRequest brandRequest)
        {

            if (brandRequest != null)
            {
                if (brandRequest.LogoUrl != null && brandRequest.LogoUrl.Length > 0  ) 
                { 
                
                    var brand = brandRequest.Adapt<Brand>();
                    brandService.Add(brandRequest.LogoUrl, brand);

                    return CreatedAtAction(nameof(GetById),new{ id = brand.Id},brand);

                }
               
            }

            return BadRequest();
        }
    }
}
