using System.ComponentModel.DataAnnotations;
using NodaTime;

namespace TradeMarket.Models.Dto
{
    public class UserUpdateDto
    {
        [Required]
        public required Guid UserId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? DateOfBirth { get; set; }
        /*public AddressDto? Address { get; set; }*/
    }
}
