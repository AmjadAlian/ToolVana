using System.Linq.Expressions;
using toolvana.API.Models;

namespace toolvana.API.Services.Brands
{
    public interface IBrandService
    {
         IEnumerable<Brand> GetAll();
        Brand? GetBrand(Expression<Func<Brand,bool>> expression);
        Brand Add(IFormFile file,Brand brand);
        bool Edit(int id, IFormFile? file ,Brand brand);
        bool Remove(int id);
    }
}
