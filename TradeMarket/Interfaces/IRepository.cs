using System.Linq.Expressions;

namespace TradeMarket.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task<T?> GetByIdAsync(Guid id);
        Task<T> FindAsync(Expression<Func<T, bool>>? predicate = null, bool tracked = true);
        Task<IEnumerable<T>> GetAllAsync();
        Task DeleteAsync(T entity);
 
    }

}
