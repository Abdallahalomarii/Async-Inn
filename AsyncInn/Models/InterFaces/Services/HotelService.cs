using AsyncInn.Data;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn.Models.InterFaces.Services
{
    public class HotelService : IHotel
    {
        private readonly AsyncInnDbContext _hotel;

        public HotelService(AsyncInnDbContext hotel)
        {
            _hotel = hotel;
        }
        public async Task<Hotel> Create(Hotel hotel)
        {
            _hotel.Hotel.Add(hotel);

            await _hotel.SaveChangesAsync();

            return hotel;
        }

        public async Task DeleteHotel(int id)
        {
            Hotel hotel = await GetHotelById(id);

            _hotel.Entry<Hotel>(hotel).State = EntityState.Deleted;

            await _hotel.SaveChangesAsync();
        }

        public async Task<Hotel> GetHotelById(int id)
        {
            Hotel hotel = await _hotel.Hotel.FindAsync(id);

            return hotel;
        }

        public async Task<List<Hotel>> GetHotels()
        {
            var hotels = await _hotel.Hotel.ToListAsync();

            return hotels;
        }

        public async Task<Hotel> UpdateHotel(int id, Hotel hotel)
        {
            hotel = await GetHotelById(id);

            _hotel.Entry<Hotel>(hotel).State = EntityState.Modified;

            await _hotel.SaveChangesAsync();

            return hotel;
        }
    }
}
