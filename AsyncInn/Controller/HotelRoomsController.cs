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
        [HttpGet]
        [Route("/api/Hotels/{hotelId}/Rooms")]
        public async Task<ActionResult<IEnumerable<HotelRoom>>> GetHotelRooms([FromRoute] int hotelId)
        {
          if (_context == null)
          {
              return NotFound();
          }

             var hotelRooms =  await _context.GetHotelRooms(hotelId);

            return Ok(hotelRooms);
        }

        // GET: api/HotelRooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelRoom>> GetHotelRoom(int id)
        {
          if (_context == null)
          {
              return NotFound();
          }
            var hotelRoom = await _context.GetHotelRoomsById(id);

            if (hotelRoom == null)
            {
                return NotFound();
            }

            return Ok(hotelRoom);
        }

        // PUT: api/HotelRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotelRoom([FromRoute] int id,[FromBody] HotelRoom hotelRoom)
        {
           
            var updateHotelRoom = await _context.UpdateHotelRooms(id, hotelRoom);

            

            return Ok(updateHotelRoom);
        }

        // POST: api/HotelRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom(HotelRoom hotelRoom)
        {
            if (_context == null)
            {
                return Problem("Entity set 'AsyncInnDbContext.HotelRooms'  is null.");
            }
            await _context.Create(hotelRoom);

            return CreatedAtAction("GetHotelRoom", new { id = hotelRoom.RoomNumber }, hotelRoom);
        }

        // DELETE: api/HotelRooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelRoom(int id)
        {
            if (_context == null)
            {
                return NotFound();
            }
            var hotelRoom =  _context.DeleteHotelRooms(id);

            return NoContent();
        }

    }
}
