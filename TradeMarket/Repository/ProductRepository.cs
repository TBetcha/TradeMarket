using System.Linq;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using TradeMarket.Data;
using TradeMarket.IRepository;
using TradeMarket.Models.Dto;


namespace TradeMarket.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger _logger;

        public ProductRepository(ApplicationDbContext db, ILogger<ProductRepository> logger) : base(db)
        {
            _db = db;
            _logger = logger;
        }
    }

}
