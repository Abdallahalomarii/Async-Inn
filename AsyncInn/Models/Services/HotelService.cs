using AsyncInn.Data;
using AsyncInn.Models.DTO;
using AsyncInn.Models.InterFaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn.Models.Services
{
    public class HotelService : IHotel
    {
        private readonly AsyncInnDbContext _hotel;

        public HotelService(AsyncInnDbContext hotel)
        {
            _hotel = hotel;
        }
        public async Task<Hotel> Create(Hotel hotel)
        {

            _hotel.Hotel.Add(hotel);

            await _hotel.SaveChangesAsync();

            return hotel;
        }

        public async Task DeleteHotel(int id)
        {
            HotelDTO hotel = await GetHotelById(id);

            _hotel.Entry<HotelDTO>(hotel).State = EntityState.Deleted;


            await _hotel.SaveChangesAsync();
        }

        public async Task<HotelDTO> GetHotelById(int id)
        {
            //Hotel hotel = await _hotel.Hotel.FindAsync(id);

            //return hotel;
            HotelDTO? HotelById = await _hotel.Hotel
                .Select(h => new HotelDTO
                {
                    ID = h.ID,
                    Name = h.Name,
                    City = h.City,
                    State = h.State,
                    StreetAddress = h.StreetAddress,
                    Phone = h.Phone,
                    Rooms = h.HotelRooms.Select(hr => new HotelRoomDTO
                    {
                        HotelID = hr.HotelID,
                        RoomNumber = hr.RoomNumber,
                        Rate = hr.Rate,
                        IsPetFriendly = hr.IsPetFriendly,
                        RoomID = hr.RoomID,
                        Room = new RoomDTO
                        {
                            ID = hr.Room.ID,
                            Name = hr.Room.Name,
                            Layout = hr.Room.Layout,
                            Amenities = hr.Room.RoomAmenities.Select(am => new AmenityDTO()
                            {
                                Id = am.Amenity.Id,
                                Name = am.Amenity.Name
                            }).ToList()

                        }

                    }).ToList()
                }).FirstOrDefaultAsync(x => x.ID == id);

            return HotelById;
        }

        public async Task<List<HotelDTO>> GetHotels()
        {
            //var hotels = await _hotel.Hotel.ToListAsync();

            //return hotels;

            return await _hotel.Hotel
                .Select(h => new HotelDTO
                {
                    ID = h.ID,
                    Name = h.Name,
                    City = h.City,
                    State = h.State,
                    StreetAddress = h.StreetAddress,
                    Phone = h.Phone,
                    Rooms = h.HotelRooms.Select(hr => new HotelRoomDTO
                    {
                        HotelID = hr.HotelID,
                        RoomNumber = hr.RoomNumber,
                        Rate = hr.Rate,
                        IsPetFriendly = hr.IsPetFriendly,
                        RoomID = hr.RoomID,
                        Room = new RoomDTO
                        {
                            ID = hr.Room.ID,
                            Name = hr.Room.Name,
                            Layout = hr.Room.Layout,
                            Amenities = hr.Room.RoomAmenities.Select(am => new AmenityDTO()
                            {
                                Id = am.Amenity.Id,
                                Name = am.Amenity.Name
                            }).ToList()
                            
                        }

                    }).ToList()
                })
                .ToListAsync();
        }
        /// <summary>
        /// the whole record should be updated
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hotel"></param>
        /// <returns></returns>
        public async Task<Hotel> UpdateHotel(int id, Hotel hotel)
        {
            hotel.ID = id;
            _hotel.Entry<Hotel>(hotel).State = EntityState.Modified;

            await _hotel.SaveChangesAsync();

            return hotel;
        }
    }
}
