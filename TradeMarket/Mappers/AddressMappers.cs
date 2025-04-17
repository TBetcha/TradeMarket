using TradeMarket.Models.Dto;
using NodaTime;

namespace TradeMarket.Mappers
{
    public static class AddressMappers
    {
        public static Instant currentTime = Instant.FromDateTimeUtc(System.DateTime.UtcNow);
        public static Address ToAddressFromAddressCreateDto(this AddressCreateDto addressCreate)
        {
            return new Address
            {
                Line1 = addressCreate.Line1,
                Line2 = addressCreate.Line2,
                City = addressCreate.City,
                State = addressCreate.State,
                PostalCode = addressCreate.PostalCode,
                Type = addressCreate.Type,
                LastUpdated = Address.currentTime
            };
        }
    }
}
