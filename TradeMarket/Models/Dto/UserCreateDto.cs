using NodaTime;
using System.ComponentModel.DataAnnotations;

namespace TradeMarket.Models.Dto
{
    public class UserCreateDto
    {
        public static Instant currentTime = Instant.FromDateTimeUtc(System.DateTime.UtcNow);

        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        public required string DateOfBirth { get; set; }
        public required AddressCreateDto Address { get; set; }
        /*public required Instant LastUpdated { get; set; } = currentTime;*/
    }
}
