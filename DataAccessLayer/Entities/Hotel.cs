using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class Hotel
    {
        [Key]
        public Guid HotelID { get; set; }
        public string HotelName { get; set; } = string.Empty;
        public string HotelLocation { get; set; } = string.Empty;
        public string HotelDescription { get; set; } = string.Empty;
        public List<Room> Rooms { get; set; } = new List<Room>();
    }
}
