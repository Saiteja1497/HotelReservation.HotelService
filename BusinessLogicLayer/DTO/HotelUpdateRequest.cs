using DataAccessLayer.Entities;

namespace BusinessLogicLayer.DTO
{
    public record HotelUpdateRequest(Guid HotelID,string HotelName, string HotelLocation, string HotelDescription, List<Room>? Rooms)
    {
        public HotelUpdateRequest() : this(default,default, default, default, default)
        {
        }
    }
}
