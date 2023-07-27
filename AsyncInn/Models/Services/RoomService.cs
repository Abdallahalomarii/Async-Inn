using AsyncInn.Data;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn.Models.InterFaces.Services
{
    public class RoomService : IRoom
    {

        private readonly AsyncInnDbContext _room;

        public RoomService(AsyncInnDbContext room)
        {
            _room = room;
        }

        public async Task<Room> Create(Room room)
        {
            _room.Room.Add(room);

            await _room.SaveChangesAsync();

            return room;
        }

        public async Task DeleteRoom(int id)
        {
            Room room = await GetRoomById(id);

            _room.Entry<Room>(room).State = EntityState.Deleted;

            await _room.SaveChangesAsync();
        }

        public async Task<Room> GetRoomById(int id)
        {
            Room? room = await _room.Room.FindAsync(id);

            return room;
        }

        public async Task<List<Room>> GetRooms()
        {
            var rooms = await _room.Room.Include(roomAmenities => roomAmenities.RoomAmenities).ThenInclude(amenity => amenity.Amenity).ToListAsync();

            return rooms;
        }

        public async Task<Room> UpdateRoom(int id, Room room)
        {
            var roomValue = await _room.Room.FindAsync(id);

            if (roomValue != null)
            {
                roomValue.Name = room.Name;
                roomValue.Layout = room.Layout;

                await _room.SaveChangesAsync();
            }

            return roomValue;
        }
    }
}
