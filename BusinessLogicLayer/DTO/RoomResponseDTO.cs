namespace BusinessLogicLayer.DTO
{
    public record RoomResponseDTO(Guid HotelID, Guid RoomID, RoomTypeOptions RoomType, decimal RoomPrice, bool IsAvailable)
    {
        public RoomResponseDTO() : this(default, default, default, default, default)
        {
        }
    }
}
