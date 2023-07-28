using AsyncInn.Data;
using AsyncInn.Models.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn.Models.Services
{
    public class HotelRoomService : IHotelRoom
    {

        private readonly AsyncInnDbContext _context;

        public HotelRoomService(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<HotelRoom> Create(HotelRoom hotelRoom)
        {
            _context.HotelRooms.Add(hotelRoom);
            await _context.SaveChangesAsync();

            return hotelRoom;
        }

        public async Task DeleteHotelRooms(int id)
        {
            var deletedHotelRooms = await _context.HotelRooms.FindAsync(id);

            _context.Entry<HotelRoom>(deletedHotelRooms).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<List<HotelRoom>> GetHotelRooms(int hotelId)
        {
            var hotelRooms = await _context.HotelRooms
                .Include(hotel=> hotel.Hotel)
                .Include(room=> room.Room)
                .ThenInclude(roomAmenity=> roomAmenity.RoomAmenities)
                .ThenInclude(amenities=> amenities.Amenity)
                .ToListAsync();

            await _context.SaveChangesAsync();

            return hotelRooms;
        }

        public async Task<HotelRoom> GetHotelRoomsById(int id)
        {
            var hotelRoom = await _context.HotelRooms.FindAsync(id);
            return hotelRoom;
        }

        public async Task<HotelRoom> UpdateHotelRooms(int id, HotelRoom hotelRoom)
        {
            // update depend on the id that i will sent 
            hotelRoom.RoomNumber = id;

            _context.Entry<HotelRoom>(hotelRoom).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return hotelRoom;
        }
    }
}
