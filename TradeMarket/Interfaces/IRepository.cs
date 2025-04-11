using System.Linq.Expressions;

namespace TradeMarket.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);

    }

}
