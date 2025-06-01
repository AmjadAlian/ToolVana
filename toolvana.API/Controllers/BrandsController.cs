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
        [HttpGet("")]
        public IActionResult GetAll()
        {
            var brands = brandService.GetAll().ToList();
            return Ok(brands.Adapt<IEnumerable<BrandResponse>>());
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
        public IActionResult Create([FromForm] BrandRequest brandRequest)
        {

            if (brandRequest != null)
            {
                if (brandRequest.LogoUrl != null && brandRequest.LogoUrl.Length > 0  ) 
                { 
                
                    var brand = brandRequest.Adapt<Brand>();
                    brandService.Add(brandRequest.LogoUrl, brand);
                    var brandResponse = brand.Adapt<BrandResponse>();
                    return CreatedAtAction(nameof(GetById),new{ id = brand.Id}, brandResponse);

                }
               
            }

            return BadRequest();
        }

        [HttpPatch("{id}")]
        public IActionResult Edit([FromRoute] int id, [FromForm] EditBrandRequest brandRequest)
        {
            if (brandRequest != null && id > 0)
            {
                var brand = brandService.Edit(id, brandRequest.LogoUrl, brandRequest.Adapt<Brand>());

                if (brand == false)
                {
                    return NotFound();
                }
            }


            return NoContent();
        }
    }
}
