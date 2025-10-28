using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Room
    {
        [Key]
        public Guid RoomID { get; set; }
        public Guid HotelID { get; set; }
        public string RoomType { get; set; } = string.Empty;
        public decimal RoomPrice { get; set; }
        public int NoOfRoomsAvailable { get; set; }
    }
}
