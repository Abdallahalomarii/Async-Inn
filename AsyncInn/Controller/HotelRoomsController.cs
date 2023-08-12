using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsyncInn.Data;
using AsyncInn.Models;
using AsyncInn.Models.InterFaces;
using AsyncInn.Models.DTO;
using Microsoft.AspNetCore.Authorization;

namespace AsyncInn.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoom _context;

        public HotelRoomsController(IHotelRoom context)
        {
            _context = context;
        }

        // GET: api/HotelRooms
        [AllowAnonymous]
        [Authorize(Roles = "District Manager , Property Manager, Agent", Policy = "Read")]
        [HttpGet]
        [Route("/api/Hotels/{hotelId}/Rooms")]
        public async Task<ActionResult<IEnumerable<HotelRoomDTO>>> GetHotelRooms([FromRoute] int hotelId)
        {
          if (_context == null)
          {
              return NotFound();
          }

             var hotelRooms =  await _context.GetHotelRooms(hotelId);

            return Ok(hotelRooms);
        }

        // GET: api/HotelRooms/5

        [AllowAnonymous]

        [Authorize(Roles = "District Manager , Property Manager, Agent", Policy = "Read")]

        [HttpGet("/api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<HotelRoomDTO>> GetHotelRoom(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.GetHotelRoomsDetails(hotelId, roomNumber);

            if (hotelRoom == null)
            {
                return NotFound();
            }

            return Ok(hotelRoom);
        }

        // PUT: api/HotelRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [Authorize(Roles = "District Manager , Property Manager, Agent", Policy = "Update")]


        [HttpPut]
        [Route("/api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<IActionResult> PutHotelRoom([FromRoute] int hotelId, [FromRoute] int roomNumber, [FromBody] HotelRoomDTO hotelRoom)
        {
           
            var updateHotelRoom = await _context.UpdateHotelRooms(hotelId,roomNumber, hotelRoom);

            return Ok(updateHotelRoom);
        }

        // POST: api/HotelRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [Authorize(Roles = "District Manager , Property Manager", Policy = "Create")]


        [HttpPost]
        [Route("/api/Hotels/{hotelId}/Rooms")]
        public async Task<ActionResult<HotelRoomDTO>> PostHotelRoom(HotelRoomDTO hotelRoom, int hotelId)
        {
            var addedHotelRoom = await _context.Create(hotelRoom, hotelId);
            return Ok(addedHotelRoom);
        }

        // DELETE: api/HotelRooms/5

        [Authorize(Roles = "District Manager", Policy = "Delete")]

        [HttpDelete]
        [Route("/api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        public  async Task<IActionResult> DeleteHotelRoom(int hotelId, int roomNumber)
        {
            if (_context == null)
            {
                return NotFound();
            }
            await _context.DeleteHotelRooms(hotelId,roomNumber);

            return NoContent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("/api/Hotels/byName/{name}")]
        public async Task<ActionResult<IEnumerable<HotelRoom>>> getHotelRoomsByName([FromRoute] string name)
        {
            if (_context == null)
            {
                return NotFound();
            }

            var hotelRooms = await _context.GetHotelRoomsByName(name);

            return Ok(hotelRooms);
        }

    }
}
