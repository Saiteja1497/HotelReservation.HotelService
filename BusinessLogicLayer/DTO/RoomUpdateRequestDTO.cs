namespace BusinessLogicLayer.DTO
{
    public record RoomUpdateRequestDTO(Guid HotelID, Guid RoomID,RoomTypeOptions RoomType, decimal RoomPrice, int NoOfRoomsAvailable)
    {
        public RoomUpdateRequestDTO() : this(default,default, default, default, default)
        {
        }
    }
}
