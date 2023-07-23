using AsyncInn.Data;
using AsyncInn.Models.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn.Models.Services
{
    public class AmenityServices : IAmenity
    {
        private readonly AsyncInnDbContext _amenity;

        public AmenityServices(AsyncInnDbContext amenity)
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
            var amenities = await _amenity.Amenity.FindAsync(id);

            if (amenity != null)
            {
                amenities.Name = amenity.Name;
                
                await _amenity.SaveChangesAsync();
            }
          
            return amenities;
        }
    }
}
