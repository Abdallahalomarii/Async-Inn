using AsyncInn.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            Room? room = await _room.Room
                .Include(amenities => amenities.RoomAmenities)
                .ThenInclude(amenity => amenity.Amenity)
                .Include(hotelRooms=> hotelRooms.HotelRooms)
                .ThenInclude(hotel=> hotel.Hotel)
                .FirstOrDefaultAsync(rId => rId.ID == id);

            return room;
        }

        public async Task<List<Room>> GetRooms()
        {
            var rooms = await _room.Room
                .Include(r => r.RoomAmenities)
                    .ThenInclude(ra => ra.Amenity)
                .Include(hotelRooms => hotelRooms.HotelRooms)
                .ThenInclude(hotel => hotel.Hotel)
                .ToListAsync();


            var result = rooms.Select(r => new Room
            {
                ID = r.ID,
                Name = r.Name,
                Layout = r.Layout,
                RoomAmenities = r.RoomAmenities.Select(ra => new RoomAmenities
                {
                    RoomID = ra.RoomID,
                    AmenityId = ra.AmenityId,
                    Amenity = new Amenity
                    {
                        Id = ra.Amenity.Id,
                        Name = ra.Amenity.Name,
                    },
                    Room = null

                }).ToList(),
                HotelRooms = r.HotelRooms.Select(ra=> new HotelRoom
                {
                    HotelID =ra.HotelID,
                    RoomID = ra.RoomID,
                    RoomNumber = ra.RoomNumber, 
                    Rate = ra.Rate,
                    IsPetFriendly = ra.IsPetFriendly,
                    Hotel = new Hotel
                    {
                        ID = ra.Hotel.ID,
                        Name = ra.Hotel.Name,
                        StreetAddress = ra.Hotel.StreetAddress,
                        City = ra.Hotel.City,
                        State = ra.Hotel.State,
                        Country = ra.Hotel.Country,
                        Phone = ra.Hotel.Phone
                    }

                }).ToList()

            }).ToList();

            return result;
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

        public async Task<RoomAmenities> AddAmenityToRoom(int RoomID, int AmenityId)
        {
            var addAmenityToRoom = new RoomAmenities { RoomID = RoomID, AmenityId = AmenityId };
            _room.RoomAmenities.Add(addAmenityToRoom);
            await _room.SaveChangesAsync();
            return addAmenityToRoom;
        }

        public async Task RemoveAmenityFromRoom(int roomId, int amenityId)
        {
            var removedRoomsAmenity = await _room.RoomAmenities.FirstOrDefaultAsync(roomAmenities => roomAmenities.RoomID == roomId && roomAmenities.AmenityId == amenityId);

            _room.Entry<RoomAmenities>(removedRoomsAmenity).State = EntityState.Deleted;

            await _room.SaveChangesAsync();

        }
    }
}
