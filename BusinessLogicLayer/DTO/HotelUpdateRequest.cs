using DataAccessLayer.Entities;

namespace BusinessLogicLayer.DTO
{
    public record HotelUpdateRequest(Guid HotelID,string HotelName, string HotelLocation, string HotelDescription, List<RoomUpdateRequestDTO>? Rooms)
    {
        public HotelUpdateRequest() : this(default,default, default, default, default)
        {
        }
    }
}
