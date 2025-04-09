namespace TradeMarket.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public  required string LastName { get; set; }
        public required string DateOfBirth { get; set; }
        public required Address Address { get; set; }
    }
}
