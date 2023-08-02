using AsyncInn.Models.DTO;

namespace AsyncInn.Models.InterFaces
{
    public interface IHotel
    {
        Task<Hotel> Create(Hotel hotel);

        Task<List<HotelDTO>> GetHotels();

        Task<HotelDTO> GetHotelById(int id);

        Task<Hotel> UpdateHotel(int id, Hotel hotel);

        Task DeleteHotel(int id);
    }
}