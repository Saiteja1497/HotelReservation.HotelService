namespace BusinessLogicLayer.DTO
{
    public record RoomUpdateRequestDTO(Guid HotelID, Guid RoomID,RoomTypeOptions RoomType, decimal RoomPrice, bool IsAvailable)
    {
        public RoomUpdateRequestDTO() : this(default,default, default, default, default)
        {
        }
    }
}
