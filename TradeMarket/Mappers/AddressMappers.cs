using TradeMarket.Models.Dto;
using NodaTime;

namespace TradeMarket.Mappers
{
    public static class AddressMappers
    {
        public static Instant currentTime = Instant.FromDateTimeUtc(System.DateTime.UtcNow);
        public static Address ToAddressFromAddressCreateDto(this AddressCreateDto addressCreate, Guid userId)
        {
            return new Address
            {
                Line1 = addressCreate.Line1,
                Line2 = addressCreate.Line2,
                City = addressCreate.City,
                State = addressCreate.State,
                PostalCode = addressCreate.PostalCode,
                Type = addressCreate.Type,
                LastUpdated = currentTime,
                UserId = userId
            };
        }

        public static AddressDto ToAddressDto(this Address addressModel)
        {
            return new AddressDto
            {
                AddressId = addressModel.AddressId,
                Line1 = addressModel.Line1,
                Line2 = addressModel.Line2,
                City = addressModel.City,
                State = addressModel.State,
                PostalCode = addressModel.PostalCode,
                Type = addressModel.Type,
                LastUpdated = addressModel.LastUpdated
            };

        }
    }
}
