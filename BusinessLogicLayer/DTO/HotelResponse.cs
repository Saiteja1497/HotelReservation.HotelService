using DataAccessLayer.Entities;

namespace BusinessLogicLayer.DTO
{
    //public record HotelResponse(Guid HotelID, string HotelName, string HotelLocation, string HotelDescription, List<RoomResponseDTO>? Rooms)
    //{
    //    public HotelResponse() : this(default, default, default, default, default)
    //    {
    //    }
    //}
    public class HotelResponse
    {
        public Guid HotelID { get; init; }
        public string HotelName { get; init; } = string.Empty;
        public string HotelLocation { get; init; } = string.Empty;
        public string HotelDescription { get; init; } = string.Empty;
        public List<RoomResponseDTO>? Rooms { get; init; } = new();
    }
}
