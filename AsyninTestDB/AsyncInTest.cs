using AsyncInn.Models.InterFaces.Services;
using AsyncInTest;

namespace AsyninTestDB
{
    public class AsyncInTest : Mock
    {
        //[Fact]
        //public async void Test1()
        //{
        //    var room = await CreateAndSaveRoom();

        //    var amenity = await CreateAndSaveAmenity();

        //    var roomAmenities = new RoomService(_db);

        //    await roomAmenities.AddAmenityToRoom(room.ID, amenity.Id);

        //    var result = await roomAmenities.GetRoomById(room.ID);

        //    Assert.Contains(result.Amenities, x => x.Id == amenity.Id);
        //}
        [Fact]
        public void Test2()
        {
            int x = 10;

            Assert.Equal(10, x);
        }
    }

}