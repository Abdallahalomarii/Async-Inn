namespace AsyncInn.Models.DTO
{
    public class HotelRoomDTO
    {
        public int HotelID { get; set; }

        public int RoomNumber { get; set; }

        public decimal Rate { get; set; }

        public bool IsPetFriendly { get; set; }

        public int RoomID { get; set; }

        
        public RoomDTO? Room { get; set; }

    }
}
