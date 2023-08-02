using AsyncInn.Data;
using AsyncInn.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AsyncInn.Models.InterFaces.Services
{
    public class RoomService : IRoom
    {

        private readonly AsyncInnDbContext _room;

        private readonly IAmenity _amenity;

        public RoomService(AsyncInnDbContext room, IAmenity amenity)
        {
            _room = room;
            _amenity = amenity;
        }



        public async Task<RoomDTO> Create(AddNewRoomDTO room)
        {
            //_room.Room.Add(room);

            //await _room.SaveChangesAsync();

            //return room;

            Room newRoom = new Room()
            {
                ID = room.ID,
                Name = room.Name,
                Layout = room.Layout
            };
            _room.Entry(newRoom).State = EntityState.Added;

            await _room.SaveChangesAsync();
            room.ID = newRoom.ID;

            var getAmenity = await _amenity.GetAmenityById(room.AmenityId);

            await AddAmenityToRoom(newRoom.ID, getAmenity.Id);

            RoomDTO newRoomWithAmenity = await GetRoomById(room.ID);

            return newRoomWithAmenity;



        }

        public async Task DeleteRoom(int id)
        {
            //Room room = await GetRoomById(id);

            //_room.Entry<Room>(room).State = EntityState.Deleted;

            //await _room.SaveChangesAsync();

            RoomDTO room = await GetRoomById(id);

            _room.Entry<RoomDTO>(room).State = EntityState.Deleted;

            await _room.SaveChangesAsync();
        }

        public async Task<RoomDTO> GetRoomById(int id)
        {
            //Room? room = await _room.Room
            //    .Include(amenities => amenities.RoomAmenities)
            //    .ThenInclude(amenity => amenity.Amenity)
            //    .Include(hotelRooms=> hotelRooms.HotelRooms)
            //    .ThenInclude(hotel=> hotel.Hotel)
            //    .FirstOrDefaultAsync(rId => rId.ID == id);

            //return room;

            RoomDTO? room = await _room.Room
                .Select(r => new RoomDTO
                {
                    ID = r.ID,
                    Name = r.Name,
                    Layout = r.Layout,
                    Amenities = r.RoomAmenities
                    .Select(am => new AmenityDTO
                    {
                        Id = am.Amenity.Id,
                        Name = am.Amenity.Name

                    }).ToList()
                }).FirstOrDefaultAsync(x => x.ID == id);

            return room;
        }

        public async Task<List<RoomDTO>> GetRooms()
        {
            //var rooms = await _room.Room
            //    .Include(r => r.RoomAmenities)
            //        .ThenInclude(ra => ra.Amenity)
            //    .Include(hotelRooms => hotelRooms.HotelRooms)
            //    .ThenInclude(hotel => hotel.Hotel)
            //    .ToListAsync();


            //var result = rooms.Select(r => new Room
            //{
            //    ID = r.ID,
            //    Name = r.Name,
            //    Layout = r.Layout,
            //    RoomAmenities = r.RoomAmenities.Select(ra => new RoomAmenities
            //    {
            //        RoomID = ra.RoomID,
            //        AmenityId = ra.AmenityId,
            //        Amenity = new Amenity
            //        {
            //            Id = ra.Amenity.Id,
            //            Name = ra.Amenity.Name,
            //        },
            //        Room = null

            //    }).ToList(),
            //    HotelRooms = r.HotelRooms.Select(ra=> new HotelRoom
            //    {
            //        HotelID =ra.HotelID,
            //        RoomID = ra.RoomID,
            //        RoomNumber = ra.RoomNumber, 
            //        Rate = ra.Rate,
            //        IsPetFriendly = ra.IsPetFriendly,
            //        Hotel = new Hotel
            //        {
            //            ID = ra.Hotel.ID,
            //            Name = ra.Hotel.Name,
            //            StreetAddress = ra.Hotel.StreetAddress,
            //            City = ra.Hotel.City,
            //            State = ra.Hotel.State,
            //            Country = ra.Hotel.Country,
            //            Phone = ra.Hotel.Phone
            //        }

            //    }).ToList()

            //}).ToList();

            //return result;

           return await _room.Room
                .Select(r => new RoomDTO
                {
                    ID = r.ID,
                    Name = r.Name,
                    Layout = r.Layout,
                    Amenities = r.RoomAmenities
                .Select(am => new AmenityDTO
                {
                    Id = am.Amenity.Id,
                    Name = am.Amenity.Name

                }).ToList()
                }).ToListAsync();

        }



        public async Task<RoomDTO> UpdateRoom(int id, AddNewRoomDTO room)
        {
            //var roomValue = await _room.Room.FindAsync(id);

            //if (roomValue != null)
            //{
            //    roomValue.Name = room.Name;
            //    roomValue.Layout = room.Layout;

            //    await _room.SaveChangesAsync();
            //}

            //return roomValue;

            Room updatingRoom = new Room()
            {
                ID = id,
                Name = room.Name,
                Layout = room.Layout,
            };
            _room.Entry(updatingRoom).State = EntityState.Modified;

            await _room.SaveChangesAsync();
            room.ID = updatingRoom.ID;


            await AddAmenityToRoom(id, room.AmenityId);

            RoomDTO newRoomWithAmenity = await GetRoomById(id);

            return newRoomWithAmenity;

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
