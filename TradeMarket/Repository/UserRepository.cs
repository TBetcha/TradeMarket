using TradeMarket.Models.Dto;
using TradeMarket.Data;
using TradeMarket.IRepository;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using TradeMarket.Mappers;

namespace TradeMarket.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger _logger;

        public UserRepository(ApplicationDbContext db, ILogger<UserRepository> logger)
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

        public async Task<IEnumerable<User>> GetAllAsync()
        {

            var joinUserAndAddress = await _db.Users
                .Include(user => user.Address)
                .ToListAsync();

            return joinUserAndAddress;
        }

        /*public async Task<User> GetByIdAsync(Guid id)*/
        /*{*/
        /*    var user = await _db.Users*/
        /*        .Include(user => user.Address)*/
        /*        .FirstOrDefaultAsync(u => u.UserId == id);*/
        /*    if (user == null)*/
        /*    {*/
        /*        _logger.LogError($"User with id {id} not found");*/
        /*        return null;*/
        /*    }*/
        /*    return user;*/
        /*}*/

        public async Task<User?> GetByIdAsync(Guid id)
        {
          return await _db.Users.FindAsync(id);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<User> CreateAsync(User userData)
        {
            await _db.AddAsync((userData));
            await _db.SaveChangesAsync();
            return userData;
        }

        public async Task<User?> DeleteAsync(Guid id)
        {
            var userToRemove = _db.Users.FirstOrDefault(x => x.UserId == id);
            if (userToRemove == null)
                return null;

            _db.Users.Remove(userToRemove);
            await _db.SaveChangesAsync();
            return userToRemove;
        }

        /*public async task<user> findasync(expression<func<user, bool>>? predicate = null, bool tracked = true)*/
        /*{*/
        /*    iqueryable<user> query = _db.users;*/
        /*    if (!tracked) query = query.asnotracking();*/
        /*    if (predicate != null) query = query.where(predicate);*/
        /*    return await query.firstordefaultasync();*/
        /*}*/

    }
}
