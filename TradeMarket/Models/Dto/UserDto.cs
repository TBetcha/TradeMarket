using NodaTime;
using System.ComponentModel.DataAnnotations;

namespace TradeMarket.Models.Dto
{
    public class UserDto
    {
        [Required]
        public required Guid UserId { get; set; }
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string DateOfBirth { get; set; }
        [Required]
        public required AddressDto Address { get; set; }
        [Required]
        public required Instant LastUpdated { get; set; }
    }
}
