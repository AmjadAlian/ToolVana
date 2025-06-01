using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;
using toolvana.API.Data;

namespace toolvana.API.Services.GenericService
{
    public class Service<T> : IService<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Service(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _context.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAsync(Expression <Func<T,bool>>? filter = null, Expression<Func<T, object>> []? includes = null, bool isTracked = true)
        {
            IQueryable <T> query = _dbSet;
            if(filter != null)
            {
                query = query.Where(filter);
            }
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            if(!isTracked)
            {
                query = query.AsNoTracking();
            }
            return await query.ToListAsync();
        }

        public async Task<T?> GetOneAsync( Expression<Func<T, bool>> expression , Expression<Func<T, object>>[]? includes = null , bool isTracked = true)
        {

            var entity = await GetAsync(expression, includes , isTracked);
            
            return entity.FirstOrDefault();

        }
        public async Task<bool> RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
             var entity = _dbSet.Find(id);
            if (entity == null)
            {
                return false;
            }
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
