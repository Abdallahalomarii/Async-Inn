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

        /// <summary>
        /// Adding an amenityDTO to Database
        /// </summary>
        /// <param name="amenity"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Delete an amenity from the database by amenity's id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// get all amenity are assigned to the database
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// get an amenity from the database by the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// update an amenity DTO to the database depend
        /// on the id of the amenity that you want o update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amenity"></param>
        /// <returns></returns>
        public async Task<AmenityDTO> UpdateAmenity(int id, AmenityDTO amenity)
        {
            var amenityValue = await GetAmenityById(id);

            if (amenityValue != null)
            {
                Amenity updateAmenity = new Amenity()
                {
                    Id = amenityValue.Id,
                    Name = amenity.Name
                };
                
                _amenity.Entry<Amenity>(updateAmenity).State = EntityState.Modified;

                await _amenity.SaveChangesAsync();

                amenity.Id = updateAmenity.Id;
            }

            return amenity;
        }

    }
}
