using System.Linq.Expressions;
using toolvana.API.Models;
using toolvana.API.Services.GenericService;

namespace toolvana.API.Services.Categories
{
    public interface ICategoryService : IService<Category>
    {
       
        Task <bool> EditAsync(int id,Category category , CancellationToken cancellationToken = default);
      
        Task <bool> UpdateStatusAsync(int id, CancellationToken cancellationToken = default);

    }
}
