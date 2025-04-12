using System.Linq.Expressions;

namespace TradeMarket.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task<T?> GetByIdAsync(T entity);
        Task<T> FindAsync(Expression<Func<T, bool>>? predicate = null);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);

    }

}
