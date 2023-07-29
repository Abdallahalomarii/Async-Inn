using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AsyncInn.Models
{
    public class HotelRoom
    {
        public int HotelID { get; set; }

        public int RoomID { get; set; }

        public int RoomNumber { get; set; }

        public decimal Rate { get; set; }

        public bool IsPetFriendly { get; set; }

        [ForeignKey("RoomID")]
        public Room? Room { get; set; }

        [ForeignKey("HotelID")]
        public Hotel? Hotel { get; set; }
    }
}
