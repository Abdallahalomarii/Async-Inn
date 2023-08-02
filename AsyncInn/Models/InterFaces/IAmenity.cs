using AsyncInn.Models.DTO;

namespace AsyncInn.Models.InterFaces
{
    public interface IAmenity
    {
        Task<AmenityDTO> Create(AmenityDTO amenity);

        Task<List<AmenityDTO>> GetAmenities();

        Task<AmenityDTO> GetAmenityById(int id);

        Task<Amenity> UpdateAmenity(int id, Amenity amenity);

        Task DeleteAmenity(int id);
    }
}