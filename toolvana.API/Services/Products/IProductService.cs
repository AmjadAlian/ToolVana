using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using toolvana.API.Models;

namespace toolvana.API.Services.Products
{
    public interface IProductService
    {
        IEnumerable<Product>GetAll( string? query,  int page, int limit);
        Product? GetProduct(Expression <Func<Product,bool>> expression);
        Product Add(IFormFile file,Product product);
        bool Edit(int id, IFormFile? file, Product product);
        bool Delete(int id);
    }
}
