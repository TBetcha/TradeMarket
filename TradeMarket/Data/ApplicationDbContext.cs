using Microsoft.EntityFrameworkCore;
using TradeMarket.Models.Dto;

namespace TradeMarket.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
        : base(dbContextOptions) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Address { get; set; }
}
