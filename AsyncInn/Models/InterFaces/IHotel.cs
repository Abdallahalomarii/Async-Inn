using AsyncInn.Models.DTO;

namespace AsyncInn.Models.InterFaces
{
    public interface IHotel
    {

        Task<Hotel> Create(HotelDTO hotel);

        Task<List<HotelDTO>> GetHotels();

        Task<HotelDTO> GetHotelById(int id);

        Task<HotelDTO> UpdateHotel(int id, HotelDTO hotel);

        Task DeleteHotel(int id);
    }
}