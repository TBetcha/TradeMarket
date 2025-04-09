namespace TradeMarket.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public RequestedProduct AskingFor { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public string Type { get; set; }
        public Condition Condition { get; set; }
        public Category Category { get; set; }
        public User Seller { get; set; }

    }
}
