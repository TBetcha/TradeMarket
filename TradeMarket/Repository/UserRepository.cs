using TradeMarket.Models;
using TradeMarket.Data;
using TradeMarket.IRepository;

namespace TradeMarket.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) : base(db)
        {
            db = _db;
        }

        public async Task<User> UpdateAsync(User entity)
        {
            var modTime = DateTime.Now;
            _db.Users.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }

}
