namespace AsyncInn.Models.InterFaces
{
    public interface IAmenity
    {
        Task<Amenity> Create(Amenity amenity);

        Task<List<Amenity>> GetAmenities();

        Task<Amenity> GetAmenityById(int id);

        Task<Amenity> UpdateAmenity(int id, Amenity amenity);

        Task DeleteAmenity(int id);
    }
}