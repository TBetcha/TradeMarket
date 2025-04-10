using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TradeMarket.Models
{

    public class Address
    {
        public int Id { get; set; }
        [Required]
        public required string Line1 { get; set; }
        public string? Line2 { get; set; }
        [Required]
        public required string City { get; set; }
        [Required]
        public required string State { get; set; }
        [Required]
        public required string PostalCode { get; set; }
        [Required]
        public required AddressType Type { get; set; }
    }


}
