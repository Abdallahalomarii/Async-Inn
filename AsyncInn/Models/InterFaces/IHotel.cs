namespace AsyncInn.Models.InterFaces
{
    public interface IHotel
    {
        Task<Hotel> Create(Hotel hotel);

        Task<List<Hotel>> GetHotels();

        Task<Hotel> GetHotelById(int id);

        Task<Hotel> UpdateHotel(int id, Hotel hotel);

        Task DeleteHotel(int id);
    }
}