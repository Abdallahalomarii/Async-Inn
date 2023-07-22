﻿using AsyncInn.Data;
using AsyncInn.Models.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn.Models.Services
{
    public class RoomServices : IRoom
    {
        private readonly AsyncInnDbContext _room;

        public RoomServices(AsyncInnDbContext room)
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
            var rooms = await _room.Room.ToListAsync();

            return rooms;
        }

        public async Task<Room> UpdateRoom(int id, Room room)
        {
            room = await GetRoomById(id);

            _room.Entry(room).State = EntityState.Modified;

            await _room.SaveChangesAsync();

            return room;
        }
    }
}
