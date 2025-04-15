
using NodaTime;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeMarket.Models.Dto
{

    public class AddressCreateDto
    {
        public static Instant currentTime = Instant.FromDateTimeUtc(System.DateTime.UtcNow);


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
        public required AddressType Type { get; set; }
        public Instant LastUpdated { get; set; } = currentTime;

    }


}
