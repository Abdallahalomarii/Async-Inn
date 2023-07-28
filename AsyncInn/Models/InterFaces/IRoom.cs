namespace AsyncInn.Models.InterFaces
{
    public interface IRoom
    {
        Task<Room> Create(Room room);

        Task<List<Room>> GetRooms();

        Task<Room> GetRoomById(int id);

        Task<Room> UpdateRoom(int id, Room room);

        Task DeleteRoom(int id);


        Task<RoomAmenities> AddAmenityToRoom(int RoomID, int AmenityId);

        Task RemoveAmenityFromRoom(int roomId, int amenityId);


    }
}