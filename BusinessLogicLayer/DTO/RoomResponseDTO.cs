namespace BusinessLogicLayer.DTO
{
    //public record RoomResponseDTO(Guid HotelID, Guid RoomID, RoomTypeOptions RoomType, decimal RoomPrice, bool IsAvailable)
    //{
    //    public RoomResponseDTO() : this(default, default, default, default, default)
    //    {
    //    }
    //}
    public class RoomResponseDTO
    {
        public Guid HotelID { get; init; }
        public Guid RoomID { get; init; }
        public RoomTypeOptions RoomType { get; init; }
        public decimal RoomPrice { get; init; }
        public bool IsAvailable { get; init; }
    }
}
