using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using toolvana.API.Data;
using toolvana.API.Models;

namespace toolvana.API.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext context;

        public ProductService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Product Add(IFormFile file,Product product)
        {
            
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),"images",fileName);

            using (var stream = System.IO.File.Create(filePath))
            {
             file.CopyTo(stream);
            }
          product.MainImageUrl = fileName;
            // Save the product to the database
            context.Products.Add(product);
            context.SaveChanges();
            return product;
        }

        public bool Delete(int id)
        {
            var product = context.Products.Find(id);
            if (product != null)
            {
                if(product.MainImageUrl != null)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(),"images",product.MainImageUrl);
                    System.IO.File.Delete(imagePath);
                }
 
            context.Products.Remove(product);
            context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Edit(int id,IFormFile? file, Product product)
        {
            var productDb = context.Products.AsNoTracking().FirstOrDefault(p=>p.Id == id);
            
            if (productDb != null) {
             if(file != null)
                {
                    var oldImage = Path.Combine(Directory.GetCurrentDirectory(),"images", productDb.MainImageUrl);

                        System.IO.File.Delete(oldImage);
                    
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(),"images",fileName);
                    using ( var stream = System.IO.File.Create(filePath)) 
                    {
                        file.CopyTo(stream);

                    }
                    product.MainImageUrl = fileName;
                    product.Id = id;
                    context.Products.Update(product);
                    context.SaveChanges();
                    return true;

                }
                else
                {
                    product.MainImageUrl = productDb.MainImageUrl;
                    product.Id = id;
                    context.Products.Update(product);
                    context.SaveChanges();
                    return true;
                }


            }
            return false;
        }

        public IEnumerable<Product> GetAll( string? query,  int page, int limit)
        {
            IQueryable <Product> products = context.Products;
            if (!string.IsNullOrEmpty(query))
            {
                return products
                    .Where(p => p.Name.Contains(query) || p.Description.Contains(query))
                    .Skip((page - 1) * limit)
                    .Take(limit)
                    .ToList();
            }

            if(limit <=0 || page <= 0)
            {
                page = 1;
                limit = 10;
            }


            return products.Skip((page - 1) * limit).Take(limit);
           
        }

        public Product? GetProduct(Expression<Func<Product, bool>> expression)
        {   
                return context.Products.FirstOrDefault(expression);  
        }
    }
}
