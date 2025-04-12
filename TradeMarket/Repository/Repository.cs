using TradeMarket.IRepository;
using TradeMarket.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TradeMarket.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        internal DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task<T?> GetByIdAsync(T entity)
        {
            return await dbSet.FindAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
          return await dbSet.ToListAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
          return await Not Implemented;
        }

        public async Task<T> DeleteAsync(T entity)
       {
          return await Not I,mplemented;
      }

        public async Task<T> FindAsync(Expression<Func<T, bool>>? predicate = null, bool tracked = true)
        {
          IQueryable<T> query = dbSet;
          if(!tracked) query = query.AsNoTracking();
          if (predicate != null) query = query.Where(predicate);
           return await query.FirstOrDefaultAsync();
          }
    }
}
