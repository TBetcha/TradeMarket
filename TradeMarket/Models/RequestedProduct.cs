using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TradeMarket.Models
{
    public class RequestedProduct
    {
        public Guid Id { get; set; }
        [Required]
        public required RequestedProduct AskingFor { get; set; }
        [Required]
        public required string Title { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        [Required]
        public Condition Condition { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public required User Seller { get; set; }

    }
}
