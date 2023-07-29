
namespace AsyncInn.Models
{
    public class Room
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Layout { get; set; }
        

        public  List<RoomAmenities> RoomAmenities { get; set; }

        public  List<HotelRoom> HotelRooms { get; set; }
    }
}
