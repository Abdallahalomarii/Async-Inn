using AsyncInn.Data;
using AsyncInn.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace AsyncInTest
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;

        protected readonly AsyncInnDbContext _db;

        public Mock()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();
            _db = new AsyncInnDbContext(
                new DbContextOptionsBuilder<AsyncInnDbContext>()
                .UseSqlite(_connection)
                .Options
                );
            _db.Database.EnsureCreated();
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

        public void Dispose()
        {
            _db?.Dispose();

            _connection?.Dispose();
        }
    }
}