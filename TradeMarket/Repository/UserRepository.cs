using System.Linq;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using TradeMarket.Data;
using TradeMarket.IRepository;
using TradeMarket.Models.Dto;

namespace TradeMarket.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger _logger;

        public UserRepository(ApplicationDbContext db, ILogger<UserRepository> logger) : base(db)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<User> UpdateUserAsync(User entity)
        {
            var modTime = Instant.FromDateTimeUtc(System.DateTime.UtcNow);
            _db.Users.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            var joinUserAndAddress = await _db.Users.Include(user => user.Address).ToListAsync();
            return joinUserAndAddress;
        }

        public override async Task<User?> GetByIdAsync(Guid id)
        {
            var user = await _db
                .Users.Include(user => user.Address)
                .FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                _logger.LogError($"User with id {id} not found");
                return null;
            }
            return user;
        }

    }
}
