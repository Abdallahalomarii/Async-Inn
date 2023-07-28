using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsyncInn.Models
{
    public class HotelRoom
    {
        public int HotelID { get; set; }

        public int RoomID { get; set; }

        public int RoomNumber { get; set; }

        public decimal Rate { get; set; }

        public bool IsPetFriendly { get; set; }

        public Room Room { get; set; }

        public Hotel Hotel { get; set; }
    }
}
