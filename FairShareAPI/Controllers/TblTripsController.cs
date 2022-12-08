using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FairShareAPI.Data;
using FairShareAPI.Models;
using Microsoft.AspNetCore.Cors;

namespace FairShareAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors]
    public class tblTripsController : ControllerBase
    {
        private readonly FairShareDbContext _context;

        public tblTripsController(FairShareDbContext context)
        {
            _context = context;
        }

        // GET: api/tblTrips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<tblTrip>>> GettblTrips()
        {
            return await _context.tblTrips.ToListAsync();
        }

        // GET: api/tblTrips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<tblTrip>> GettblTrip(int id)
        {
            var tblTrip = await _context.tblTrips.FindAsync(id);

            if (tblTrip == null)
            {
                return NotFound();
            }

            return tblTrip;
        }

        // PUT: api/tblTrips/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PuttblTrip(int id, tblTrip tblTrip)
        {
            if (id != tblTrip.fldTripId)
            {
                return BadRequest();
            }

            _context.Entry(tblTrip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblTripExists(id))
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

        // POST: api/tblTrips
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<tblTrip>> PosttblTrip(tblTrip tblTrip)
        {
            _context.tblTrips.Add(tblTrip);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GettblTrip", new { id = tblTrip.fldTripId }, tblTrip);
        }

        // DELETE: api/tblTrips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletetblTrip(int id)
        {
            var tblTrip = await _context.tblTrips.FindAsync(id);
            if (tblTrip == null)
            {
                return NotFound();
            }

            _context.tblTrips.Remove(tblTrip);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool tblTripExists(int id)
        {
            return _context.tblTrips.Any(e => e.fldTripId == id);
        }
    }
}
