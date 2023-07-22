using AsyncInn.Data;
using AsyncInn.Models;
using AsyncInn.Models.InterFaces;
using AsyncInn.Models.InterFaces.Services;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();


            string? connString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services
                .AddDbContext<AsyncInnDbContext>
                (options => options.UseSqlServer(connString));

            builder.Services.AddTransient<IAmenity,AmenityService>();
            builder.Services.AddTransient<IRoom,RoomService>();
            builder.Services.AddTransient<IHotel,HotelService>();

            var app = builder.Build();

            app.MapControllers();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}