using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using toolvana.API.Data;
using toolvana.API.Models;
using toolvana.API.Services.GenericService;

namespace toolvana.API.Services.Categories
{
    public class CategoryService : Service< Category>, ICategoryService
    {
        

        public CategoryService(ApplicationDbContext context) : base(context)
        {
           
        }
        public async Task<bool> EditAsync(int id, Category category , CancellationToken cancellationToken = default)
        {
            var CategoryInDb = _context.Categories.Find( id);
            if (CategoryInDb == null)
            {
                return false;
            }
           
            CategoryInDb.Name = category.Name;
            CategoryInDb.Description = category.Description;
           await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<bool> UpdateStatusAsync(int id , CancellationToken cancellationToken)
        {
            var categoryInDb = _context.Categories.Find(id);
            if (categoryInDb == null) return false;
            categoryInDb.Status = !categoryInDb.Status;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
