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
        /// <summary>
        /// Create a new Hotel DTO
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        public async Task<Hotel> Create(HotelDTO hotel)
        {

            //_hotel.Hotel.Add(hotel);

            //await _hotel.SaveChangesAsync();

            //return hotel;

            Hotel newHotel = new Hotel()
            {
                Name = hotel.Name,
                City = hotel.City,
                Country = hotel.Country,
                State = hotel.State,
                StreetAddress = hotel.StreetAddress,
                Phone = hotel.Phone
            };

            _hotel.Entry(newHotel).State = EntityState.Added;

            await _hotel.SaveChangesAsync();

            return newHotel;
        }
        /// <summary>
        /// Delete A hotel by the id of the hotel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteHotel(int id)
        {
            var hotel = await _hotel.Hotel.FindAsync(id);

            _hotel.Entry<Hotel>(hotel).State = EntityState.Deleted;

            await _hotel.SaveChangesAsync();
        }
        /// <summary>
        /// get a the hotel DTO  by passing the id of the hotel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<HotelDTO> GetHotelById(int id)
        {
            //Hotel hotel = await _hotel.Hotel.FindAsync(id);

            //return hotel;
            HotelDTO? HotelById = await _hotel.Hotel
                .Select(h => new HotelDTO
                {
                    ID = h.ID,
                    Name = h.Name,
                    Country = h.Country,
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

        /// <summary>
        /// get all hotels that you have in the Database
        /// </summary>
        /// <returns></returns>
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
        /// updating a hotel by the id that you want it 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hotel"></param>
        /// <returns></returns>
        public async Task<HotelDTO> UpdateHotel(int id, HotelDTO hotel) 
        {   
            
            var oldHotelRecord = await _hotel.Hotel.FindAsync(id);

            if (oldHotelRecord != null)
            {

                oldHotelRecord.Name = hotel.Name;
                oldHotelRecord.Country = hotel.Country;
                oldHotelRecord.City = hotel.City;
                oldHotelRecord.State = hotel.State;
                oldHotelRecord.StreetAddress = hotel.StreetAddress;
                oldHotelRecord.Phone = hotel.Phone;

                _hotel.Entry<Hotel>(oldHotelRecord).State = EntityState.Modified;
                await _hotel.SaveChangesAsync();

            }

            hotel.ID = id;

            var returnedHotel = await GetHotelById(hotel.ID);
            return returnedHotel;
        }
    }
}
