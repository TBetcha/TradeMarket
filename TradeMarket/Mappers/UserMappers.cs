using TradeMarket.Models.Dto;
using NodaTime;

namespace TradeMarket.Mappers
{
    public static class UserMappers
    {
        public static Instant currentTime = Instant.FromDateTimeUtc(System.DateTime.UtcNow);
        public static User ToUserFromUserCreateDto(this UserCreateDto userCreate)
        {
            return new User
            {
                FirstName = userCreate.FirstName,
                LastName = userCreate.LastName,
                Password = userCreate.Password,
                Email = userCreate.Email,
                DateOfBirth = userCreate.DateOfBirth,
                Address = AddressMappers.ToAddressFromAddressCreateDto(userCreate.Address),
                LastUpdated = User.currentTime
            };
        }

    }
}
