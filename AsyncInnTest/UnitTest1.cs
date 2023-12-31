using AsyncInn.Controller;
using AsyncInn.Models;
using AsyncInn.Models.DTO;
using AsyncInn.Models.InterFaces;
using AsyncInn.Models.InterFaces.Services;
using AsyncInn.Models.Services;
using AsyncInnTest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using System.Security.Claims;

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

            var hotelRoomService = new HotelRoomService(_db, _room);

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

            var hotelRoomService = new HotelRoomService(_db, _room);

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

        [Fact]
        public async Task Register_User_As_District_Manager()
        {
            // Arrange
            var userMock = new Mock<IUser>();
            var userManagerMock = new Mock<UserManager<User>>(MockBehavior.Strict, null, null, null, null, null, null, null, null);
            var jwtTokenServiceMock = new Mock<JwtTokenService>(null, null);

            var roles = new List<Claim> { new Claim(ClaimTypes.Role, "District Manager") };
            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(roles));

            var controller = new UserController(userMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = userPrincipal
                }
            };

            var registerDto = new RegisterDTO
            {
                UserName = "TestUser",
                Email = "test@example.com",
                PhoneNumber = "123456789",
                Password = "P@ssw0rd",
                Roles = new List<string> { "Agent" } // Adjust the roles as needed
            };

            var expectedResult = new UserDTO
            {
                ID = "UserId",
                Username = registerDto.UserName,
                Token = "MockedToken",
                Roles = new List<string> { "Agent" } // Adjust the roles as needed
            };

            userMock.Setup(u => u.Register(It.IsAny<RegisterDTO>(), It.IsAny<ModelStateDictionary>(), It.IsAny<ClaimsPrincipal>()))
                            .ReturnsAsync(expectedResult);

            // Act
            var result = await controller.Register(registerDto);

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
            var userDto = Assert.IsType<UserDTO>(actionResult.Value);

            Assert.Equal(expectedResult.Username, userDto.Username);
            Assert.Equal(expectedResult.Roles, userDto.Roles);

        }
        [Fact]
        public async Task SignIn_User_Successfully()
        {
            // Arrange
            var expectedResult = new UserDTO
            {
                ID = "UserId",
                Username = "TestUser",
                Token = "MockedToken",
                Roles = new List<string> { "Agent" }
            };

            var userMock = SetupUserMock(expectedResult);
            var controller = new UserController(userMock);

            var loginDto = new LoginDTO
            {
                Username = "TestUser",
                Password = "P@ssw0rd"
            };

            // Act
            var result = await controller.SignIn(loginDto);

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
            var userDto = Assert.IsType<UserDTO>(actionResult.Value);

            Assert.Equal(expectedResult.Username, userDto.Username);
            Assert.Equal(expectedResult.Roles, userDto.Roles);
        }

       
    }
}
