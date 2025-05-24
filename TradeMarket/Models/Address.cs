using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

namespace TradeMarket.Models.Dto
{
    public class Address
    {
        public static Instant currentTime = Instant.FromDateTimeUtc(System.DateTime.UtcNow);

        [Key]
        /*[DatabaseGenerated(DatabaseGeneratedOption.Identity)]*/
        public Guid AddressId { get; set; }

        [Required]
        public required string Line1 { get; set; }
        public string? Line2 { get; set; }

        [Required]
        public required string City { get; set; }

        [Required]
        [MaxLength(2)]
        [MinLength(2)]
        public required string State { get; set; }

        [Required]
        [MaxLength(10)]
        [MinLength(5)]
        public required string PostalCode { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public required Guid UserId { get; set; }

        [Required]
        public required AddressType Type { get; set; }
        public Instant LastUpdated { get; set; }
        public Instant CreatedAt { get; set; } = currentTime;
    }
}
