using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

namespace TradeMarket.Models.Dto
{
    public class User
    {
        public static Instant currentTime = Instant.FromDateTimeUtc(System.DateTime.UtcNow);

        [Key]
        /*[DatabaseGenerated(DatabaseGeneratedOption.Identity)]*/
        public Guid UserId { get; set; }

        [Required]
        [MinLength(3)]
        public required string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public required string LastName { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string DateOfBirth { get; set; }

        [Required]
        [ForeignKey("AddressId")]
        public required Address Address { get; set; }

        [Required]
        public Instant LastUpdated { get; set; }
        public Instant CreatedAt { get; set; } = currentTime;
    }
}
