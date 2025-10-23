using DataAccessLayer.Entities;

namespace BusinessLogicLayer.DTO
{
    public record HotelResponse(Guid HotelID, string HotelName, string HotelLocation, string HotelDescription, List<Room>? Rooms)
    {
        public HotelResponse() : this(default, default, default, default, default)
        {
        }
    }
}
