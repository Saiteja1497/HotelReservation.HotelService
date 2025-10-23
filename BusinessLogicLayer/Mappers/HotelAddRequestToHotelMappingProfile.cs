using AutoMapper;
using BusinessLogicLayer.DTO;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Mappers
{
    public class HotelAddRequestToHotelMappingProfile:Profile
    {
        public HotelAddRequestToHotelMappingProfile()
        {
            CreateMap<HotelAddRequest, Hotel>()
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.HotelName))
                .ForMember(dest => dest.HotelLocation, opt => opt.MapFrom(src => src.HotelLocation))
                .ForMember(dest => dest.HotelDescription, opt => opt.MapFrom(src => src.HotelDescription))
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.Rooms));
        }
    }
}
