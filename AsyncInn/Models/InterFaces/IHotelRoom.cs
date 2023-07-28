namespace AsyncInn.Models.InterFaces
{
    public interface IHotelRoom
    {
        Task<HotelRoom> Create(HotelRoom hotelRoom);

        Task<List<HotelRoom>> GetHotelRooms();

        Task<HotelRoom> GetHotelRoomsById(int id);

        Task<HotelRoom> UpdateHotelRooms(int id, HotelRoom hotelRoom);

        Task DeleteHotelRooms(int id);
    }
}
