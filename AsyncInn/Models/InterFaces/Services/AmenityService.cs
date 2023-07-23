using AsyncInn.Data;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn.Models.InterFaces.Services
{
    public class AmenityService : IAmenity
    {

        private readonly AsyncInnDbContext _amenity;

        public AmenityService(AsyncInnDbContext amenity)
        {
            _amenity = amenity;
        }

        public async Task<Amenity> Create(Amenity amenity)
        {
            _amenity.Amenity.Add(amenity);

             await _amenity.SaveChangesAsync();

            return amenity;

        }

        public async Task DeleteAmenity(int id)
        {
            Amenity amenity = await GetAmenityById(id);

            _amenity.Entry<Amenity>(amenity).State = EntityState.Deleted;

            await _amenity.SaveChangesAsync();
        }

        public async Task<List<Amenity>> GetAmenities()
        {
            var amenities = await _amenity.Amenity.ToListAsync();

            return amenities;
        }

        public async Task<Amenity> GetAmenityById(int id)
        {
            Amenity? amenity = await _amenity.Amenity.FindAsync(id);
            return amenity;
        }

        public async Task<Amenity> UpdateAmenity(int id, Amenity amenity)
        {
            var amenityValue = await _amenity.Amenity.FindAsync(id);

            if (amenityValue != null)
            {
                amenityValue.Name = amenity.Name;

                await _amenity.SaveChangesAsync();
            }

            return amenityValue;
        }
    }
}
