using System.Reflection;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using toolvana.API.Data;
using toolvana.API.DTOs.Requests.Product;
using toolvana.API.DTOs.Responses.Product;
using toolvana.API.Models;
using toolvana.API.Services.Products;

namespace toolvana.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
       
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            
            this.productService = productService;
        }

        [HttpGet("")]
        public IActionResult GetProducts([FromQuery] string?query , [FromQuery] int page, [FromQuery] int limit)
        {
            var products = productService.GetAll(query , page , limit).ToList();
            if (products == null || products.Count == 0)
            {
                return NotFound();
            }
            return Ok(products.Adapt<IEnumerable<ProductResponse>>());
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var product = productService.GetProduct(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product.Adapt<ProductResponse>());
        }

        [HttpPost("")]
        public IActionResult CreateProduct([FromForm] ProductRequest productRequest)
        {

            if (productRequest != null)
            {
                var product = productRequest.Adapt<Product>();
               product = productService.Add(productRequest.MainImageUrl, product);
                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product.Adapt<ProductResponse>());
            }


            return BadRequest();
        }
        [HttpPatch("{id}")]
        public IActionResult UpdateProduct([FromRoute] int id, [FromForm] EditProductRequest productRequest)
        {
           
            var product = productService.Edit(id , productRequest.MainImageUrl, productRequest.Adapt<Product>());

               if (product == false)
                {
                    return BadRequest();
                }
                return NoContent();

            }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var product = productService.Delete(id);
            if(product == true)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }              

        }


    }
}
