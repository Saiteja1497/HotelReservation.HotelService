namespace BusinessLogicLayer.DTO
{
    public record RoomAddRequestDTO(Guid HotelID, RoomTypeOptions RoomType, decimal RoomPrice, int NoOfRoomsAvailable)
    {
        public RoomAddRequestDTO() : this(default, default, default, default)
        {
        }
    }
}
