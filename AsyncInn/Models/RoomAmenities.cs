namespace AsyncInn.Models
{
    public class RoomAmenities
    {
        public int RoomID { get; set; }

        public int AmenityId { get; set; }

        public Room Room { get; set; }

        public Amenity Amenity { get; set;}
        
    }
}
