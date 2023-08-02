using AsyncInn.Models.DTO;

namespace AsyncInn.Models.InterFaces
{
    public interface IHotelRoom
    {
        Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoomDTO,  int hotelId);

        Task<List<HotelRoomDTO>> GetHotelRooms(int hotelId);

        Task<HotelRoomDTO> GetHotelRoomsDetails(int hotelId, int roomNumber);

        Task<HotelRoomDTO> UpdateHotelRooms(int hotelId, int roomNumber, HotelRoomDTO hotelRoom);

        Task DeleteHotelRooms(int hotelId, int roomNumber);

        Task<List<HotelRoom>> GetHotelRoomsByName(string name);
    }
}
