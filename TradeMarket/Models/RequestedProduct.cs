
namespace TradeMarket.Models
{
    public class RequestedProduct
    {
        public required RequestedProduct AskingFor { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public Condition Condition { get; set; }
        public Category Category { get; set; }
        public required User Seller { get; set; }

    }
}
