using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TradeMarket.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        public required string DateOfBirth { get; set; }
        [Required]
        public required Address Address { get; set; }
    }
}
