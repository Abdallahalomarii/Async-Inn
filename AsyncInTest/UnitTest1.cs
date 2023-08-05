using AsyncInn.Models.InterFaces.Services;

namespace AsyncInTest
{
    public class UnitTest1 : Mock
    {
        [Fact]
        public async void AddingAmenityToRoom()
        {
            var amenity = await CreateAndSaveAmenity();

            var room = await CreateAndSaveRoom();

            var roomAmenities = new RoomService(_db);

            await roomAmenities.AddAmenityToRoom(room.ID, amenity.Id);

            var actualRoom = await roomAmenities.GetRoomById(room.ID);

            Assert.Contains(actualRoom.Amenities, e => e.Id == amenity.Id);
            
        }
    }
}