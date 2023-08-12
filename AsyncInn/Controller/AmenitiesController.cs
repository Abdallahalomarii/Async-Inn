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
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace AsyncInn.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenity _context;

        public AmenitiesController(IAmenity context)
        {
            _context = context;
        }

        // GET: api/Amenities
        [AllowAnonymous]
        [Authorize(Roles = "District Manager , Property Manager", Policy = "Read")]

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmenityDTO>>> GetAmenity()
        {
          if (_context == null)
          {
              return NotFound();
          }
            return await _context.GetAmenities();
        }

        // GET: api/Amenities/5
        [AllowAnonymous]
        [Authorize(Roles = "District Manager , Property Manager", Policy = "Read")]
        [HttpGet("{id}")]
        public async Task<ActionResult<AmenityDTO>> GetAmenity(int id)
        {
          if (_context == null)
          {
              return NotFound();
          }
            var amenity = await _context.GetAmenityById(id);

            if (amenity == null)
            {
                return NotFound();
            }

            return amenity;
        }

        // PUT: api/Amenities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "District Manager" , Policy ="Update")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmenity([FromRoute] int id, [FromBody] AmenityDTO amenity)
        {
            //if (id != amenity.Id)
            //{
            //    return BadRequest();
            //}

            var amenities = await _context.UpdateAmenity(id, amenity);

            return Ok(amenities);
         }

        // POST: api/Amenities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "District Manager", Policy = "Create")]
        [HttpPost]
        public async Task<ActionResult<AmenityDTO>> PostAmenity(AmenityDTO amenity)
        {
          if (_context == null)
          {
              return Problem("Entity set 'AsyncInnDbContext.Amenity'  is null.");
          }
            await _context.Create(amenity);
            return CreatedAtAction(nameof(GetAmenity), new { id = amenity.Id }, amenity);
        }

        // DELETE: api/Amenities/5
        [Authorize(Roles = "District Manager", Policy ="Delete")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenity(int id)
        {
            if (_context == null)
            {
                return NotFound();
            }
            var amenity = await _context.GetAmenityById(id);
            if (amenity == null)
            {
                return NotFound();
            }

            await _context.DeleteAmenity(id);

            return NoContent();
        }

        
    }
}
