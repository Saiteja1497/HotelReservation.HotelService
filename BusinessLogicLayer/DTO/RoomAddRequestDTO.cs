namespace BusinessLogicLayer.DTO
{
    public record RoomAddRequestDTO(Guid HotelID, RoomTypeOptions RoomType, decimal RoomPrice, bool IsAvailable)
    {
        public RoomAddRequestDTO() : this(default, default, default, default)
        {
        }
    }
}
