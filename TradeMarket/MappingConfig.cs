using AutoMapper;
using TradeMarket.Models.Dto;
namespace TradeMarket.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<User, UserCreateDto>().ReverseMap();
            CreateMap<Address, AddressCreateDto>().ReverseMap();
            CreateMap<AddressCreateDto, Address>();
        }
    }
}
