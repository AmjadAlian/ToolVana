using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using toolvana.API.Data;
using toolvana.API.DTOs.Requests.Category;
using toolvana.API.DTOs.Responses.Category;
using toolvana.API.Models;
using toolvana.API.Services.Categories;

namespace toolvana.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController (ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService categoryService = categoryService;

     

        [HttpGet("")]
        public IActionResult GetCategories()
        {
            var categories = categoryService.GetAll().ToList();

            return categories.Count == 0 ? NotFound() : Ok(categories.Adapt<IEnumerable <CategoryResponse>>());
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById([FromRoute] int id)
        {
            var category = categoryService.Get(c => c.Id == id);

            return category == null ? NotFound() : Ok(category.Adapt<CategoryResponse>());
        }
        [HttpPost("")]
        public IActionResult CreateCategory([FromForm] CategoryRequest categoryRequest)
        {
            var category = categoryRequest.Adapt<Category>();
            categoryService.Add(category);
            
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateCategory([FromRoute] int id, [FromForm] CategoryRequest category)
        {
            var categoryDb = categoryService.Edit(id, category.Adapt<Category>());
            if (categoryDb == false)
            {
                return NotFound();
            }
          
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory([FromRoute]int id)
        {
           var category =  categoryService.Remove(id);
            return category == true? NoContent():NotFound();
        }
    }
}
