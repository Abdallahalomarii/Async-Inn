﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsyncInn.Data;
using AsyncInn.Models;

namespace AsyncInn.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenitiesController : ControllerBase
    {
        private readonly AsyncInnDbContext _context;

        public AmenitiesController(AsyncInnDbContext context)
        {
            _context = context;
        }

        // GET: api/Amenities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Amenity>>> GetAmenity()
        {
          if (_context.Amenity == null)
          {
              return NotFound();
          }
            return await _context.Amenity.ToListAsync();
        }

        // GET: api/Amenities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Amenity>> GetAmenity(int id)
        {
          if (_context.Amenity == null)
          {
              return NotFound();
          }
            var amenity = await _context.Amenity.FindAsync(id);

            if (amenity == null)
            {
                return NotFound();
            }

            return amenity;
        }

        // PUT: api/Amenities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmenity(int id, Amenity amenity)
        {
            if (id != amenity.Id)
            {
                return BadRequest();
            }

            _context.Entry(amenity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmenityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Amenities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Amenity>> PostAmenity(Amenity amenity)
        {
          if (_context.Amenity == null)
          {
              return Problem("Entity set 'AsyncInnDbContext.Amenity'  is null.");
          }
            _context.Amenity.Add(amenity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAmenity", new { id = amenity.Id }, amenity);
        }

        // DELETE: api/Amenities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenity(int id)
        {
            if (_context.Amenity == null)
            {
                return NotFound();
            }
            var amenity = await _context.Amenity.FindAsync(id);
            if (amenity == null)
            {
                return NotFound();
            }

            _context.Amenity.Remove(amenity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AmenityExists(int id)
        {
            return (_context.Amenity?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
