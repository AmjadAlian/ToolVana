using System.Linq.Expressions;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using toolvana.API.Data;
using toolvana.API.Models;

namespace toolvana.API.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext context;

        public CategoryService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Category Add(Category category)
        {

            context.Categories.Add(category);
            context.SaveChanges();
            return category;
        
        }
        

        public bool Edit(int id, Category category)
        {
            var CategoryInDb = context.Categories.AsNoTracking().FirstOrDefault(c=> c.Id == id);
            if (CategoryInDb == null)
            {
                return false;
            }
            category.Id = id;   
            context.Categories.Update(category);
            context.SaveChanges();
            return true;
        }

        public Category? Get(Expression<Func<Category, bool>> expression)
        {
            return context.Categories.FirstOrDefault(expression);

        }

        public IEnumerable<Category> GetAll()
        {
           var categories = context.Categories.ToList();
            return categories;
        }

        public bool Remove(int id)
        {
            var category = context.Categories.Find(id);
            if (category == null)
            {
                return false;
            }
            context.Categories.Remove(category);
            context.SaveChanges();
            return true;
        }
    }
}
