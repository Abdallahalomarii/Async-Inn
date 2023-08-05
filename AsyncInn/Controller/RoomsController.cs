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

namespace AsyncInn.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoom _context;

        public RoomsController(IRoom context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRoom()
        {
          if (_context == null)
          {
              return NotFound();
          }
            return await _context.GetRooms();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDTO>> GetRoom(int id)
        {
          if (_context == null)
          {
              return NotFound();
          }
            var room = await _context.GetRoomById(id);
            return Ok(room);
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom([FromRoute] int id, [FromBody] AddNewRoomDTO room)
        {

            var updateRoom = await _context.UpdateRoom(id,room);

            return Ok(updateRoom);
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(RoomDTO room)
        {
          if (_context == null)
          {
              return Problem("Entity set 'AsyncInnDbContext.Room'  is null.");
          }
            await _context.Create(room);
            return CreatedAtAction("GetRoom", new { id = room.ID }, room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            if (_context== null)
            {
                return NotFound();
            }
            await _context.DeleteRoom(id);

            return NoContent();
        }

        [HttpPost]
        [Route("{RoomID}/Amenity/{AmenityId}")]
        public async Task<ActionResult<Room>> PostAmenityToRoom([FromRoute] int RoomID, [FromRoute] int AmenityId)
        {
            var x = await _context.AddAmenityToRoom(RoomID, AmenityId);
            return Ok(x);
        }

        [HttpDelete]
        [Route("{RoomID}/Amenity/{AmenityId}")]
        public async Task<IActionResult> DeleteRoomsAmenities(int RoomID, int AmenityId)
        {
            if (_context == null)
            {
                return NotFound();
            }
            await _context.RemoveAmenityFromRoom(RoomID, AmenityId);
            return NoContent();
        }

    }
}
