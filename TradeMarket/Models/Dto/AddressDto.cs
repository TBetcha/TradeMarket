using NodaTime;
using System.ComponentModel.DataAnnotations;

namespace TradeMarket.Models.Dto
{
    public class AddressDto
    {
        public required Guid AddressId { get; set; }
        [Required]
        public required string Line1 { get; set; }
        public string Line2 { get; set; } = string.Empty;
        [Required]
        public required string City { get; set; }
        [Required]
        public required string State { get; set; }
        [Required]
        public required string PostalCode { get; set; }

        [Required]
        public required AddressType Type { get; set; }
        public required Instant LastUpdated { get; set; }
    }
}
