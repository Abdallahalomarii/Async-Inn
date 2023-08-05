using AsyncInn.Data;
using AsyncInn.Models.DTO;
using AsyncInn.Models.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn.Models.Services
{
    public class HotelRoomService : IHotelRoom
    {

        private readonly AsyncInnDbContext _context;

        private readonly IRoom _room;

        public HotelRoomService(AsyncInnDbContext context, IRoom room)
        {
            _context = context;
            _room = room;
        }
        
        /// <summary>
        /// Creating a new hotel Room DTO for a specific hotel 
        /// </summary>
        /// <param name="hotelRoom"></param>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        public async Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoom, int hotelId)
        {
            //var room = await _context.Room.FindAsync(hotelRoom.RoomID);
            //var hotel = await _context.Hotel.FindAsync(hotelRoom.HotelID);

            //hotelRoom.HotelID = hotelId;

            //hotelRoom.Room = room;
            //hotelRoom.Hotel = hotel;

            //_context.HotelRooms.Add(hotelRoom);

            //await _context.SaveChangesAsync();

            //return hotelRoom;
       
       
                HotelRoom newHotelRoom = new HotelRoom()
                {
                    HotelID = hotelId,
                    RoomNumber = hotelRoom.RoomNumber,
                    Rate = hotelRoom.Rate,
                    IsPetFriendly = hotelRoom.IsPetFriendly,
                    RoomID = hotelRoom.RoomID
                };

                var room = await _room.GetRoomById(newHotelRoom.RoomID);
                if (room != null)
                {
                    hotelRoom.Room = room;

                    _context.Entry(newHotelRoom).State = EntityState.Added;

                    await _context.SaveChangesAsync();


                    return hotelRoom;
                }

                else
                    return null;
       


        }

       
        /// <summary>
        /// delete the hotel room by hotel id and the room number
        /// </summary>
        /// <param name="hotelId">number of the hotel</param>
        /// <param name="roomNumber">number of the room</param>
        /// <returns></returns>
        public async Task DeleteHotelRooms(int hotelId, int roomNumber)
        {
            var delete = await _context.HotelRooms
                .Where(r => r.HotelID == hotelId && r.RoomNumber == roomNumber)
                .FirstOrDefaultAsync();
            if (delete != null)
            {
                _context.Entry<HotelRoom>(delete).State = EntityState.Deleted;

                await _context.SaveChangesAsync();
            }
        }
        /// <summary>
        /// return all hotel rooms for only specific hotel depend on the hotel id
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        public async Task<List<HotelRoomDTO>> GetHotelRooms(int hotelId)
        {
            //var hotelRooms = await _context.HotelRooms
            //    .Include(hotel => hotel.Hotel)
            //    .Include(room => room.Room)
            //    .ThenInclude(amenities => amenities.RoomAmenities)
            //    .ThenInclude(roomAmenities => roomAmenities.Amenity)
            //    .Where(x => x.HotelID == hotelId)
            //    .ToListAsync();

            //var result = hotelRooms.Select(hr => new HotelRoom
            //{
            //    HotelID = hr.HotelID,
            //    RoomID = hr.RoomID,
            //    RoomNumber = hr.RoomNumber,
            //    Rate = hr.Rate,
            //    IsPetFriendly = hr.IsPetFriendly,
            //    Room = new Room
            //    {
            //        ID = hr.Room.ID,
            //        Name = hr.Room.Name,
            //        Layout = hr.Room.Layout,
            //        RoomAmenities = hr.Room.RoomAmenities.Select(ra => new RoomAmenities
            //        {
            //            RoomID = ra.RoomID,
            //            AmenityId = ra.AmenityId,
            //            Amenity = new Amenity
            //            {
            //                Id = ra.Amenity.Id,
            //                Name = ra.Amenity.Name,
            //            }
            //        }).ToList()
            //    },
            //    Hotel = new Hotel
            //    {
            //        ID = hr.Hotel.ID,
            //        Name = hr.Hotel.Name,
            //        StreetAddress = hr.Hotel.StreetAddress,
            //        City = hr.Hotel.City,
            //        State = hr.Hotel.State,
            //        Country = hr.Hotel.Country,
            //        Phone = hr.Hotel.Phone
            //    }

            //}).ToList();

            //return result;

            return await _context.HotelRooms
                .Select( hr=> new HotelRoomDTO()
                {
                    HotelID = hr.HotelID,
                    RoomNumber = hr.RoomNumber,
                    Rate = hr.Rate,
                    IsPetFriendly = hr.IsPetFriendly,
                    RoomID = hr.RoomID,
                    Room = new RoomDTO
                    {
                        ID = hr.Room.ID,
                        Name = hr.Room.Name,
                        Layout = hr.Room.Layout,
                        Amenities = hr.Room.RoomAmenities.Select(am=> new AmenityDTO()
                        {
                            Id= am.Amenity.Id,
                            Name = am.Amenity.Name
                        }).ToList()
                    }
                }).Where(x=> x.HotelID == hotelId)
                .ToListAsync();

        }
        /// <summary>
        /// return a hotel room by the name of the hotel 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<List<HotelRoom>> GetHotelRoomsByName(string name)
        {
            var hotels = await _context.HotelRooms.Include(x => x.Hotel)
                .Where(hn => hn.Hotel.Name == name)
                .ToListAsync();

            return hotels;


        }
        /// <summary>
        /// return  the hotel room details by the room number and for the specific hotel id 
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="roomNumber"></param>
        /// <returns></returns>
        public async Task<HotelRoomDTO> GetHotelRoomsDetails(int hotelId, int roomNumber)
        {
            //var hotelDetails = await _context.HotelRooms
            //    .Include(hotel => hotel.Hotel)
            //    .Include(room => room.Room)
            //    .ThenInclude(roomAmenities => roomAmenities.RoomAmenities)
            //    .ThenInclude(amenity => amenity.Amenity)
            //    .Where(hotel => hotel.HotelID == hotelId && hotel.RoomNumber == roomNumber)
            //    .FirstOrDefaultAsync();


            //return hotelDetails;

            HotelRoomDTO? hotelRoomById = await _context.HotelRooms
                .Select(hr => new HotelRoomDTO()
                {
                    HotelID = hotelId,
                    RoomNumber = hr.RoomNumber,
                    Rate = hr.Rate,
                    IsPetFriendly = hr.IsPetFriendly,
                    RoomID = hr.RoomID,
                    Room = new RoomDTO
                    {
                        ID = hr.Room.ID,
                        Name = hr.Room.Name,
                        Layout = hr.Room.Layout,
                        Amenities = hr.Room.RoomAmenities.Select(am => new AmenityDTO()
                        {
                            Id = am.Amenity.Id,
                            Name = am.Amenity.Name
                        }).ToList()
                    }
                }).FirstOrDefaultAsync(x=> x.HotelID == hotelId && x.RoomNumber == roomNumber);

            return hotelRoomById;
        }
        /// <summary>
        /// Updating the hotel room by passing hotel id and the room number 
        /// and the new data of the hotel room DTO.
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="roomNumber"></param>
        /// <param name="hotelRoom"></param>
        /// <returns></returns>
        public async Task<HotelRoomDTO> UpdateHotelRooms(int hotelId, int roomNumber, HotelRoomDTO hotelRoom)
        {
            // update depend on the id that i will sent 
            
            //var hotel = await _context.HotelRooms.FindAsync(hotelId,roomNumber);

            //if (hotel != null)
            //{
            //    hotel.HotelID = hotelRoom.HotelID;
            //    hotel.RoomID = hotelRoom.RoomID;
            //    hotel.RoomNumber = hotelRoom.RoomNumber;
            //    hotel.Rate = hotelRoom.Rate;
            //    hotel.IsPetFriendly = hotelRoom.IsPetFriendly;

            //    await _context.SaveChangesAsync();
            //}
            
            //return hotelRoom;

            var hotelRoomRecord = await _context.HotelRooms.FindAsync(hotelId,roomNumber);

            if (hotelRoomRecord != null)
            {

                
                    hotelRoomRecord.HotelID = hotelRoom.HotelID;
                    hotelRoomRecord.RoomNumber = hotelRoom.RoomNumber;
                    hotelRoomRecord.Rate = hotelRoom.Rate;
                    hotelRoomRecord.IsPetFriendly = hotelRoom.IsPetFriendly;
                    hotelRoomRecord.RoomID = hotelRoom.RoomID;
                    
               

                _context.Entry(hotelRoomRecord).State = EntityState.Modified;

                await _context.SaveChangesAsync();

            }
                
            hotelRoom = await GetHotelRoomsDetails(hotelRoomRecord.HotelID, hotelRoomRecord.RoomNumber);
                return hotelRoom;

        }
    }
}
