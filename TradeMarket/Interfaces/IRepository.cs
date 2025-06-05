using System.Linq.Expressions;

namespace TradeMarket.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity, CancellationToken cancellationToken);
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<T> FindAsync(Expression<Func<T, bool>>? predicate = null, bool tracked = true);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        Task DeleteAsync(T entity, CancellationToken cancellationToken);
    }
}
