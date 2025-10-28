using AutoMapper;
using BusinessLogicLayer.DTO;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Mappers
{
    public class RoomUpdateRequestToRoomMappingProfile:Profile
    {
        public RoomUpdateRequestToRoomMappingProfile()
        {
            CreateMap<RoomUpdateRequestDTO, Room>()
                .ForMember(dest => dest.HotelID, opt => opt.MapFrom(src => src.HotelID))
                .ForMember(dest => dest.RoomID, opt => opt.MapFrom(src => src.RoomID))
                .ForMember(dest => dest.RoomPrice, opt => opt.MapFrom(src => src.RoomPrice))
                .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.RoomType))
                .ForMember(dest => dest.NoOfRoomsAvailable, opt => opt.MapFrom(src => src.NoOfRoomsAvailable));
        }
    }
}
