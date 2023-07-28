using AsyncInn.Data;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

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

            var amenities = await _amenity.Amenity
                .Include(a => a.Rooms)
                    .ThenInclude(ra => ra.Room) 
                .ToListAsync();

            var result = amenities.Select(a => new Amenity
            {
                Id = a.Id,
                Name = a.Name,
            
                Rooms = a.Rooms.Select(ra => new RoomAmenities
                {
                    RoomID = ra.RoomID,
                    AmenityId = ra.AmenityId,
                    Room = new Room
                    {
                        ID = ra.Room.ID,
                        Name = ra.Room.Name,
                        Layout = ra.Room.Layout
                        
                    }
                }).ToList()
            }).ToList();

            return result;



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
