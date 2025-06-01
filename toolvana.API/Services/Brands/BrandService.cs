using Microsoft.EntityFrameworkCore;
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

        public Brand Add(IFormFile file,Brand brand)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),"images",fileName);
            using (var stream = System.IO.File.Create(filePath))
            {
                file.CopyTo(stream);
            }
            brand.LogoUrl = fileName;
            context.Brands.Add(brand);
            context.SaveChanges();
            // Save the file to the server
            return brand;
        }

        public bool Edit(int id, IFormFile? file, Brand brand)
        {
            var brandDb = context.Brands.AsNoTracking().FirstOrDefault(b => b.Id == id);

            if (brandDb == null) return false;

                if (file != null)
                {
                    // Delete the old file if it exists
                    var oldFileName = brandDb.LogoUrl;
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "images", oldFileName);

                    if (System.IO.File.Exists(oldFilePath))
                    {
                       
                        System.IO.File.Delete(oldFilePath);
                    }
                    //create new image 
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                    }

                    brand.Id = id;
                    brand.LogoUrl = fileName;
                }
                else
                {
                  brand.LogoUrl = brandDb.LogoUrl;
                    brand.Id = id;
                }
                context.Brands.Update(brand);
                context.SaveChanges();
                return true;
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
