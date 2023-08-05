using AsyncInn.Data;
using AsyncInn.Models;
using AsyncInn.Models.InterFaces;
using AsyncInn.Models.InterFaces.Services;
using AsyncInn.Models.Services;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();


            builder.Services.AddControllers().AddNewtonsoftJson(options =>
       options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
     );

            string? connString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services
                .AddDbContext<AsyncInnDbContext>
                (options => options.UseSqlServer(connString));

            builder.Services.AddTransient<IAmenity,AmenityService>();
            builder.Services.AddTransient<IRoom,RoomService>();
            builder.Services.AddTransient<IHotel,HotelService>();
            builder.Services.AddTransient<IHotelRoom,HotelRoomService>();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "AsyncInn API",
                    Version = "v1"

                });
            });

            var app = builder.Build();

            app.UseSwagger(opt =>
            {
                opt.RouteTemplate = "/api/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/api/v1/swagger.json", "AsyncIn API");
                opt.RoutePrefix = "docs";
            });

            app.MapControllers();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}