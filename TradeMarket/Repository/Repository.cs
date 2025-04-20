using TradeMarket.IRepository;
using TradeMarket.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TradeMarket.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _db;
        internal DbSet<T> dbSet;

        public Repository(DbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<T?> GetByIdAsync(T entity)
        {
            return await dbSet.FindAsync(entity);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>>? predicate = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;
            if (!tracked) query = query.AsNoTracking();
            if (predicate != null) query = query.Where(predicate);
            return await query.FirstOrDefaultAsync();
        }
    }
}
