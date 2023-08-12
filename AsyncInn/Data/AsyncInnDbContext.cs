using AsyncInn.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AsyncInn.Data
{
    public class AsyncInnDbContext : IdentityDbContext<User>
    {
        public AsyncInnDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Hotel> Hotel { get; set; }

        public DbSet<Room> Room { get; set; }

        public DbSet<Amenity> Amenity { get; set; }

        public DbSet<RoomAmenities> RoomAmenities { get; set; }

        public DbSet<HotelRoom> HotelRooms { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel() { ID = 1, Name = "AsyncInn Paris", StreetAddress = "Evil Tower", City = "Paris", State = "Paris", Country = "France", Phone = "005588756" },
                new Hotel() { ID = 2, Name = "AsyncInn London", StreetAddress = "London Bridge", City = "London", State = "England", Country = "UK", Phone = "7782215" },
                new Hotel() { ID = 3, Name = "AsyncInn Jordan", StreetAddress = "Bolivard", City = "Amman", State = "Middle East", Country = "Jordan", Phone = "0775545216" }
                );
            modelBuilder.Entity<Room>().HasData(
                new Room() { ID = 1, Name = "Restful Rainier", Layout = 0 },
                new Room() { ID = 2, Name = "Seahawks Snooze", Layout = 1 },
                new Room() { ID = 3, Name = "The Seattle location", Layout = 2 }
                );

            modelBuilder.Entity<Amenity>().HasData(
                new Amenity() { Id = 1, Name = "AC" },
                new Amenity() { Id = 2, Name = "Coffee Maker" },
                new Amenity() { Id = 3, Name = "ocean view" },
                new Amenity() { Id = 4, Name = "Mini Bar" }
                );

            modelBuilder.Entity<RoomAmenities>().HasKey(
                roomAmenities => new
                {
                    roomAmenities.RoomID,
                    roomAmenities.AmenityId
                }
                );

            modelBuilder.Entity<HotelRoom>().HasKey(
                Keys => new
                {
                    Keys.HotelID,
                    Keys.RoomNumber
                }
                );

            SeedRole(modelBuilder, "District Manager", "Create","Read", "Update","Delete");
            SeedRole(modelBuilder, "Property Manager", "Create", "Read", "Update");
            SeedRole(modelBuilder, "Agent", "Create", "Read", "Update","Delete");
            SeedRole(modelBuilder, "Anonymous users");
        }
        int nextId = 1;
        private void SeedRole(ModelBuilder modelBuilder, string roleName, params string[] permissions)
        {
            var role = new IdentityRole()
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };

            modelBuilder.Entity<IdentityRole>().HasData(role);

            var roleClaim = permissions.Select(permission =>
            new IdentityRoleClaim<string>
            {
                Id = nextId++,
                RoleId = role.Id,
                ClaimType = "permissions",
                ClaimValue = permission
            }).ToArray();

            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(roleClaim);
        }
    }
}
