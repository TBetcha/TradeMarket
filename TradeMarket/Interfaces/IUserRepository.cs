using TradeMarket.Models;

namespace TradeMarket.IRepository
{
  public interface IUserRepository: IRepository<User>
  {
    Task<User> UpdateAsync(User entity);
  }

}
