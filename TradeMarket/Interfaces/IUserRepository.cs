using TradeMarket.Models.Dto;
using System.Linq.Expressions;

namespace TradeMarket.IRepository
{
    public interface IUserRepository
    {
        Task<User?> UpdateAsync(User entity);
        Task<User?> CreateAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
        /*Task<User> FindAsync(Expression<Func<User, bool>>? predicate = null, bool tracked = true);*/
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> DeleteAsync(Guid id);
    }

}
