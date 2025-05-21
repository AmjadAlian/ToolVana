using System.Linq.Expressions;
using toolvana.API.Models;

namespace toolvana.API.Services.Categories
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        Category? Get(Expression<Func<Category,bool>> expression);
        Category Add(Category category);
        bool Edit(int id,Category category);
        bool Remove(int id);

    }
}
