using AsyncInn.Models.DTO;

namespace AsyncInn.Models.InterFaces
{
    public interface IRoom
    {
        Task<RoomDTO> Create(AddNewRoomDTO room);

        Task<List<RoomDTO>> GetRooms();

        Task<RoomDTO> GetRoomById(int id);

        Task<RoomDTO> UpdateRoom(int id, AddNewRoomDTO room);

        Task DeleteRoom(int id);

        Task<RoomAmenities> AddAmenityToRoom(int RoomID, int AmenityId);

        Task RemoveAmenityFromRoom(int roomId, int amenityId);
    }
}