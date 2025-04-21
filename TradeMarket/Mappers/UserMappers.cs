using TradeMarket.Models.Dto;
using NodaTime;

namespace TradeMarket.Mappers
{
    public static class UserMappers
    {
        public static Instant currentTime = Instant.FromDateTimeUtc(System.DateTime.UtcNow);
        public static User ToUserFromUserCreateDto(this UserCreateDto userCreate)
        {
            var UserId = Guid.NewGuid();
            return new User
            {
                UserId = UserId,
                FirstName = userCreate.FirstName,
                LastName = userCreate.LastName,
                Password = Utilities.Utils.HashUserPassword(userCreate.Password),
                Email = userCreate.Email,
                DateOfBirth = userCreate.DateOfBirth,
                Address = AddressMappers.ToAddressFromAddressCreateDto(userCreate.Address, UserId),
                LastUpdated = User.currentTime
            };
        }

    }
}
