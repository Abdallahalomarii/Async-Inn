﻿
namespace AsyncInn.Models
{
    public class Amenity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<RoomAmenities> Rooms { get; set; }
    }   
}
