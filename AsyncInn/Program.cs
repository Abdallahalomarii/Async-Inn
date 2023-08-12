using AsyncInn.Data;
using AsyncInn.Models;
using AsyncInn.Models.InterFaces;
using AsyncInn.Models.InterFaces.Services;
using AsyncInn.Models.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
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

            builder.Services.AddIdentity<User, IdentityRole>
                (options => 
                options.User.RequireUniqueEmail = true)
                .AddEntityFrameworkStores<AsyncInnDbContext>();


            builder.Services.AddTransient<IUser, UserService>();
            builder.Services.AddTransient<IAmenity,AmenityService>();
            builder.Services.AddTransient<IRoom,RoomService>();
            builder.Services.AddTransient<IHotel,HotelService>();
            builder.Services.AddTransient<IHotelRoom,HotelRoomService>();

            builder.Services.AddScoped<JwtTokenService>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            options.TokenValidationParameters = JwtTokenService.GetTokenValidationParameters(builder.Configuration)
            );

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Create", policy => policy.RequireClaim("permissions", "Create"));
                options.AddPolicy("Read", policy => policy.RequireClaim("permissions", "Read"));
                options.AddPolicy("Update", policy => policy.RequireClaim("permissions", "Update"));
                options.AddPolicy("Delete", policy => policy.RequireClaim("permissions", "Delete"));
              
            });
            builder.Services.AddAuthorization();


            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "School API",
                    Version = "v1",
                });
            });


            var app = builder.Build();

            app.UseSwagger(aptions =>
            {
                aptions.RouteTemplate = "/api/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(aptions =>
            {
                aptions.SwaggerEndpoint("/api/v1/swagger.json", "School API");
                aptions.RoutePrefix = "";
            });

            app.MapControllers();


            app.Run();
        }
    }
}