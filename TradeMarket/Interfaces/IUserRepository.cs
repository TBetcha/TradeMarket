using TradeMarket.Models.Dto;

namespace TradeMarket.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> UpdateUserAsync(User entity);
    }
} 
