using AsyncInn.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AsyncInn.Data
{
    public class AsyncInnDbContext : DbContext
    {
        public AsyncInnDbContext(DbContextOptions options) : base(options)         
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel() { ID = 1, Name = "AsyncInn Paris", StreetAddress = "Evil Tower", City = "Paris", State = "Paris", Country = "France", Phone = "005588756" },
                new Hotel() { ID = 2, Name = "AsyncInn London",StreetAddress = "London Bridge", City = "London", State = "England", Country = "UK",Phone="7782215" },
                new Hotel() { ID = 3, Name = "AsyncInn Jordan",StreetAddress="Bolivard", City = "Amman",State="Middle East", Country="Jordan" ,Phone="0775545216" }
                );
            modelBuilder.Entity<Room>().HasData(
                new Room() { ID=1, Name= "Restful Rainier", Layout=0},
                new Room() { ID=2, Name= "Seahawks Snooze", Layout=1},
                new Room() { ID=3, Name= "The Seattle location", Layout=2}
                );

            modelBuilder.Entity<Amenity>().HasData(
                new Amenity() { Id = 1, Name = "AC" },
                new Amenity() { Id = 2, Name = "Coffee Maker" },
                new Amenity() { Id = 3, Name = "ocean view" },
                new Amenity() { Id = 4, Name = "Mini Bar" }
                );
            
        }
        

        public DbSet<Hotel> Hotel { get; set; }

        public DbSet<Room> Room { get; set; }
        public DbSet<Amenity> Amenity { get; set; }
    }
}
