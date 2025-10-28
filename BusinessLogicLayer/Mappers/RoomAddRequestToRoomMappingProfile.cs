using AutoMapper;
using BusinessLogicLayer.DTO;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Mappers
{
    public class RoomAddRequestToRoomMappingProfile:Profile
    {
        public RoomAddRequestToRoomMappingProfile()
        {
            CreateMap<RoomAddRequestDTO, Room>()
                .ForMember(dest => dest.HotelID, opt => opt.MapFrom(src => src.HotelID))
                .ForMember(dest => dest.RoomPrice, opt => opt.MapFrom(src => src.RoomPrice))
                .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.RoomType))
                .ForMember(dest => dest.NoOfRoomsAvailable, opt => opt.MapFrom(src => src.NoOfRoomsAvailable));
        }
    }
}
