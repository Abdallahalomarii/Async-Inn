using AsyncInn.Data;
using AsyncInn.Models.DTO;
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

        public async Task<AmenityDTO> Create(AmenityDTO amenity)
        {
            // _amenity.Amenity.Add(amenity);
            //await _amenity.SaveChangesAsync();

            //return amenity;

            var newAmenity = new Amenity()
            {
                Name = amenity.Name,
            };
            _amenity.Amenity.Add(newAmenity);

             await _amenity.SaveChangesAsync();

            amenity.Id = newAmenity.Id;

            return amenity;

        }

        public async Task DeleteAmenity(int id)
        {
            AmenityDTO amenity = await GetAmenityById(id);

            var newAmenity = new Amenity()
            {
                Id = amenity.Id,
                Name = amenity.Name,
            };

            _amenity.Entry<Amenity>(newAmenity).State = EntityState.Deleted;

            await _amenity.SaveChangesAsync();
        }

        public async Task<List<AmenityDTO>> GetAmenities()
        {

            //var amenities = await _amenity.Amenity
            //    .Include(a => a.Rooms)
            //        .ThenInclude(ra => ra.Room) 
            //    .ToListAsync();

            //var result = amenities.Select(a => new Amenity
            //{
            //    Id = a.Id,
            //    Name = a.Name,

            //    Rooms = a.Rooms.Select(ra => new RoomAmenities
            //    {
            //        RoomID = ra.RoomID,
            //        AmenityId = ra.AmenityId,
            //        Room = new Room
            //        {
            //            ID = ra.Room.ID,
            //            Name = ra.Room.Name,
            //            Layout = ra.Room.Layout

            //        }
            //    }).ToList()
            //}).ToList();

            //return amenities;

            var amenities = await _amenity.Amenity
                .Select(amenity => new AmenityDTO
                {
                    Id = amenity.Id,
                    Name = amenity.Name
                }).ToListAsync();

            return amenities;



        }

        public async Task<AmenityDTO> GetAmenityById(int id)
        {
            //Amenity? amenity = await _amenity.Amenity.FindAsync(id);
            //return amenity;

            AmenityDTO? amenityDto = await _amenity.Amenity
                .Select(amenity => new AmenityDTO
                {
                    Id= amenity.Id,
                    Name = amenity.Name
                }).FirstOrDefaultAsync(amenityId => amenityId.Id == id);

            return amenityDto;

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
