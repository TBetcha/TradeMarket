
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
        /*public Instant LastUpdated { get; set; } = currentTime;*/

        public AddressCreateDto(string Line1, string Line2, string City, string State, string PostalCode)
        {
          this.Line1 = Line1;
          this.Line2 = Line2;
          this.City = City;
          this.State = State;
          this.PostalCode = PostalCode;
        }
    }


}
