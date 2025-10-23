using AutoMapper;
using BusinessLogicLayer.DTO;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Mappers
{
    public class HotelUpdateRequestToHotelMappingProfile:Profile
    {
        public HotelUpdateRequestToHotelMappingProfile()
        {
            CreateMap<HotelUpdateRequest, Hotel>()
                .ForMember(dest => dest.HotelID, opt => opt.MapFrom(src => src.HotelID))
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.HotelName))
                .ForMember(dest => dest.HotelLocation, opt => opt.MapFrom(src => src.HotelLocation))
                .ForMember(dest => dest.HotelDescription, opt => opt.MapFrom(src => src.HotelDescription))
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.Rooms));
        }
    }
}
