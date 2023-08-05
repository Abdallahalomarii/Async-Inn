using AsyncInn.Models;
using AsyncInn.Models.DTO;
using AsyncInn.Models.InterFaces;
using AsyncInn.Models.InterFaces.Services;
using AsyncInn.Models.Services;
using AsyncInnTest;

namespace AsyncInnTest
{
    public class UnitTest1 : Mock
    {

        // room and amenity
        [Fact]
        public async void AddAmenityToRoom()
        {
            var room = await CreateAndSaveRoom();

            var amenity = await CreateAndSaveAmenity();

            var roomAmenities = new RoomService(_db);

            await roomAmenities.AddAmenityToRoom(room.ID, amenity.Id);

            var result = await roomAmenities.GetRoomById(room.ID);

            Assert.Contains(result.Amenities, x => x.Id == amenity.Id);
        }

        [Fact]
        public async void RemoveAmenityFromRoom()
        {
            var room = await CreateAndSaveRoom();

            var amenity = await CreateAndSaveAmenity();

            var roomAmenities = new RoomService(_db);

            await roomAmenities.AddAmenityToRoom(room.ID, amenity.Id);

            await roomAmenities.RemoveAmenityFromRoom(room.ID, amenity.Id);

            var result = await roomAmenities.GetRoomById(room.ID);

            Assert.DoesNotContain(result.Amenities, x => x.Id == amenity.Id);
        }

        [Fact]
        public async void UpdateRoomByAddingANewDataAndNewAmenityAndRemoveTheOldOne()
        {
            var room = await CreateAndSaveRoom();

            var amenity = await CreateAndSaveAmenity();

            var amenity2 = await CreateAndSaveAmenity2();




            var roomService = new RoomService(_db);

            await roomService.AddAmenityToRoom(room.ID, amenity.Id);

            var result1 = await roomService.GetRoomById(room.ID);

            Assert.Contains(result1.Amenities, am => am.Id == amenity.Id);

            Assert.DoesNotContain(result1.Amenities, am => am.Id == amenity2.Id);

            var newRoom = new Room()
            {
                Name = "Flat Room",
                Layout = 2
            };

            AddNewRoomDTO newRoomDto = new AddNewRoomDTO()
            {
                Name = newRoom.Name,
                Layout = newRoom.Layout,
                AmenityId = amenity2.Id

            };

            await roomService.UpdateRoom(room.ID, newRoomDto);

            var result2 = await roomService.GetRoomById(room.ID);

            Assert.Contains(result2.Amenities, am => am.Id == amenity2.Id);

            await roomService.RemoveAmenityFromRoom(room.ID, amenity.Id);

            result2 = await roomService.GetRoomById(room.ID);

            Assert.DoesNotContain(result2.Amenities, am => am.Id == amenity.Id);


        }

        [Fact]
          public async Task HotelService_Should_Add_Hotel_Room()
        {
            // Arrange
            var room = await CreateAndSaveRoom();
            var hotel = await CreateAndSaveHotel();
            
            var hotelRoom = new HotelRoomDTO()
            {
                HotelID = hotel.ID,
                RoomNumber = 10,
                Rate = 5.5M,
                IsPetFriendly = true,
                RoomID = room.ID
            };

            var hotelRoomService = new HotelRoomService(_db,_room);

            // Act
            await hotelRoomService.Create(hotelRoom, hotel.ID);

            var hotelService = new HotelService(_db);


            // Assert
            var result = await hotelService.GetHotelById(hotel.ID);
            Assert.Contains(result.Rooms, x => x.RoomID == room.ID && x.HotelID == hotel.ID);
        }

        [Fact]
        public async void UpdateANewHotelRoom()
        {

            var room = await CreateAndSaveRoom();

            var hotel = await CreateAndSaveHotel();

            var hotelRoom = new HotelRoomDTO()
            {
                HotelID = hotel.ID,
                RoomNumber = 10,
                Rate = 5.5M,
                IsPetFriendly = true,
                RoomID = room.ID
            };

            var hotelRoomService = new HotelRoomService(_db,_room);

            await hotelRoomService.Create(hotelRoom, hotel.ID);

            var hotelService = new HotelService(_db);

            
            var result = await hotelService.GetHotelById(hotel.ID);

            Assert.Contains(result.Rooms, x => x.RoomID == room.ID);

            Assert.Contains(result.Rooms, x => x.HotelID == hotel.ID);

             var room2 = await CreateAndSaveRoom2();

            var newHotelRoom = new HotelRoomDTO()
            {
                HotelID = hotel.ID,
                RoomNumber = 10,
                Rate = 9.5M,
                IsPetFriendly = false,
                RoomID = room2.ID
            };

            await hotelRoomService.UpdateHotelRooms(hotel.ID, newHotelRoom.RoomNumber, newHotelRoom);

            var result2 = await hotelService.GetHotelById(hotel.ID);

            Assert.Contains(result2.Rooms, x => x.HotelID == hotel.ID);

            Assert.Contains(result2.Rooms, x => x.RoomID == room2.ID);

            Assert.DoesNotContain(result2.Rooms, x => x.RoomID == room.ID);
        }

        [Fact]
        public async void RemoveHotelRooms()
        {
            var room = await CreateAndSaveRoom();

            var hotel = await CreateAndSaveHotel();

            var hotelRoom = new HotelRoomDTO()
            {
                HotelID = hotel.ID,
                RoomNumber = 10,
                Rate = 5.5M,
                IsPetFriendly = true,
                RoomID = room.ID
            };

            var hotelRoomService = new HotelRoomService(_db, _room);

            await hotelRoomService.Create(hotelRoom, hotel.ID);

            var hotelService = new HotelService(_db);


            var result = await hotelService.GetHotelById(hotel.ID);

            Assert.Contains(result.Rooms, x => x.RoomID == room.ID);

            Assert.Contains(result.Rooms, x => x.HotelID == hotel.ID);

            await hotelRoomService.DeleteHotelRooms(hotel.ID, hotelRoom.RoomNumber);
            result = await hotelService.GetHotelById(hotel.ID);
            Assert.DoesNotContain(result.Rooms, x => x.RoomID == room.ID);
                   
           Assert.DoesNotContain(result.Rooms, x => x.HotelID == hotel.ID);
        }

        [Fact]
        public async void AddHotelAndRemoveIt()
        {
            var hotel = await CreateAndSaveHotel();
            var hotel2 = await CreateAndSaveHotel2();
           
            var hotelService = new HotelService(_db);

            var result = await hotelService.GetHotels();

            Assert.Contains(result, x => x.ID == hotel.ID);
            Assert.Contains(result, x => x.ID == hotel2.ID);

            await hotelService.DeleteHotel(hotel.ID);

            result = await hotelService.GetHotels();

            Assert.DoesNotContain(result, x => x.ID == hotel.ID);
            Assert.Contains(result, x => x.ID == hotel2.ID);

        }
    }
}
