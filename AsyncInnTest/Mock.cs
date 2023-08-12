using AsyncInn.Data;
using AsyncInn.Models;
using AsyncInn.Models.DTO;
using AsyncInn.Models.InterFaces;
using AsyncInn.Models.InterFaces.Services;
using AsyncInn.Models.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AsyncInnTest
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;


        protected readonly AsyncInnDbContext _db;
        protected readonly JwtTokenService _JwtTokenService;
        private readonly UserManager<User> _userManager;
        protected readonly IRoom _room;
        protected readonly IUser _user;


        public Mock()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _db = new AsyncInnDbContext(
                new DbContextOptionsBuilder<AsyncInnDbContext>()
                .UseSqlite(_connection).Options);


            _db.Database.EnsureCreated();

            _room = new RoomService(_db);

            _user = new UserService(_userManager, _JwtTokenService);
        }

        protected async Task<Amenity> CreateAndSaveAmenity()
        {
            Amenity amenity = new Amenity()
            {
                Name = "Test"
            };
            _db.Amenity.Add(amenity);
            await _db.SaveChangesAsync();

            return amenity;
        }

        protected async Task<Amenity> CreateAndSaveAmenity2()
        {
            Amenity amenity = new Amenity()
            {
                Name = "Test1"
            };
            _db.Amenity.Add(amenity);
            await _db.SaveChangesAsync();

            return amenity;
        }

        protected async Task<Room> CreateAndSaveRoom()
        {
            Room room = new Room()
            {
                Name = "Test",
                Layout = 1
            };
            _db.Room.Add(room);
            await _db.SaveChangesAsync();
            return room;
        }
        protected async Task<Room> CreateAndSaveRoom2()
        {
            Room room = new Room()
            {
                Name = "Test1",
                Layout = 2
            };
            _db.Room.Add(room);
            await _db.SaveChangesAsync();
            return room;
        }

        protected async Task<Hotel> CreateAndSaveHotel()
        {
            Hotel hotel = new Hotel()
            {
                Name = "Test",
                City = "city",
                Country = "testC",
                State = "tt",
                StreetAddress = "st",
                Phone = "0000"
            };

            _db.Hotel.Add(hotel);

            await _db.SaveChangesAsync();

            return hotel;

        } 
        protected async Task<Hotel> CreateAndSaveHotel2()
        {
            Hotel hotel = new Hotel()
            {
                Name = "Asyncin",
                City = "Amman",
                Country = "Jordan",
                State = "Jr",
                StreetAddress = "st amman ",
                Phone = "0785785578"
            };

            _db.Hotel.Add(hotel);

            await _db.SaveChangesAsync();

            return hotel;

        }

        protected static IUser SetupUserMock(UserDTO expectedResult)
        {
            var userMock = new Mock<IUser>();

            userMock.Setup(u => u.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(expectedResult);

            return userMock.Object;
        }
                
        public void Dispose()
        {
            _db?.Dispose();

            _connection?.Dispose();
        }
    }
}
