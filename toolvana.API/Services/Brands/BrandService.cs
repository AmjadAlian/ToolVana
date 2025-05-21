using System.Linq.Expressions;
using toolvana.API.Data;
using toolvana.API.Models;

namespace toolvana.API.Services.Brands
{
    public class BrandService : IBrandService
    {
        private readonly ApplicationDbContext context;

        public BrandService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Brand Add(Brand brand)
        {
           context.Brands.Add(brand);
            context.SaveChanges();
            return brand;
        }

        public Brand Add(IFormFile file, Brand brand)
        {
            throw new NotImplementedException();
        }

        public bool Edit(int id, Brand brand)
        {
            throw new NotImplementedException();
        }

        public bool Edit(int id, IFormFile file, Brand brand)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Brand> GetAll()
        {
            return context.Brands.ToList();
        }

        public Brand? GetBrand(Expression<Func<Brand, bool>> expression)
        {
           var brand = context.Brands.FirstOrDefault(expression);
            return brand;
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
