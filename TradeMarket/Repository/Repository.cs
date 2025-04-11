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

        public async Task GetByIdAsync(T entity)
        {
            await dbSet.FindAsync(entity);
        }

        public async Task GetAllAsync()
        {
          await dbSet.ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
          return await Not Implemented;
        }

        public async Task DeleteAsync(T entity)
       {
          return await Not Implemented;
      }

        public async Task FindAsync(Expression<Func<T, bool>>)
        {
          }
    }
}
