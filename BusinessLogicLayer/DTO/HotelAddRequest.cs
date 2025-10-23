using DataAccessLayer.Entities;

namespace BusinessLogicLayer.DTO
{
    public record HotelAddRequest(string HotelName, string HotelLocation, string HotelDescription, List<Room>? Rooms)
    {
       public HotelAddRequest():this(default, default, default, default)
       {
        }
    }
}
