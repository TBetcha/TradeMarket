using TradeMarket.Models.Dto;
using TradeMarket.Data;
using TradeMarket.IRepository;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NodaTime;

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

        public async Task<User> UpdateAsync(User entity)
        {
            var modTime = Instant.FromDateTimeUtc(System.DateTime.UtcNow);
            _db.Users.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public override async Task<IEnumerable<User>> GetAllAsync()
        {
          
          var joinUserAndAddress = await _db.Users
              .Include(user => user.Address)
              .ToListAsync();

          return joinUserAndAddress;
        }

    }
} 
