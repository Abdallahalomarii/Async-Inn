
namespace AsyncInn.Models
{
    public class Room
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Layout { get; set; }
        

        public virtual List<RoomAmenities> RoomAmenities { get; set; }
        public virtual List<HotelRoom> HotelRooms { get; set; }
    }
}
