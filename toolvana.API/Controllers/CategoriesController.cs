using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using toolvana.API.Data;
using toolvana.API.DTOs.Requests.Category;
using toolvana.API.DTOs.Responses.Category;
using toolvana.API.Models;
using toolvana.API.Services.Categories;

namespace toolvana.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController (ICategoryService categoryService ) : ControllerBase
    {
        private readonly ICategoryService categoryService = categoryService;

     

        [HttpGet("")]
        public async Task<IActionResult> GetCategories(string query)
        {
            var categories =await categoryService.GetAsync();

            return categories == null ? NotFound() : Ok(categories.Adapt<IEnumerable <CategoryResponse>>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int id)
        {
            var category = await categoryService.GetOneAsync(c => c.Id == id);

            return category == null ? NotFound() : Ok(category.Adapt<CategoryResponse>());
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest categoryRequest, CancellationToken cancellationToken = default)
        {
            var category = categoryRequest.Adapt<Category>();
            await categoryService.AddAsync(category,cancellationToken);
            
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }
        [HttpPut("{id}")]
        public async Task <IActionResult> UpdateCategory([FromRoute] int id, [FromForm] CategoryRequest category , CancellationToken cancellationToken = default)
        {
            var categoryDb = await categoryService.EditAsync(id, category.Adapt<Category>() , cancellationToken);
            if (categoryDb == false)
            {
                return NotFound();
            }
          
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute]int id)
        {
           var category =await categoryService.RemoveAsync(id);
            return category == true? NoContent():NotFound();
        }
    }
}
