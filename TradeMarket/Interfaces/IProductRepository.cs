using TradeMarket.Models.Dto;

namespace TradeMarket.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> UpdateProductAsync(Product entity, CancellationToken cancellationToken);
    }
} 
