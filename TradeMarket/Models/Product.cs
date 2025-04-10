using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TradeMarket.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public RequestedProduct? AskingFor { get; set; }
        [Required]
        public required string Title { get; set; }
        public string? Description { get; set; }
        [Required]
        public required Status Status { get; set; }
        [Required]
        public required string Type { get; set; }
        [Required]
        public required Condition Condition { get; set; }
        [Required]
        public required Category Category { get; set; }
        [Required]
        public required User Seller { get; set; }

    }
}
