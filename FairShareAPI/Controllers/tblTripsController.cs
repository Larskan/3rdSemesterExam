using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FairShareAPI.Data;
using FairShareAPI.Models;

namespace FairShareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class tblTripsController : ControllerBase
    {
        private readonly FairShareContext _context;

        public tblTripsController(FairShareContext context)
        {
            _context = context;
        }

        // GET: api/tblTrips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<tblTrip>>> GettblTrip()
        {
            return await _context.tblTrip.ToListAsync();
        }

        // GET: api/tblTrips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<tblTrip>> GettblTrip(int id)
        {
            var tblTrip = await _context.tblTrip.FindAsync(id);

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
            if (id != tblTrip.fldTripID)
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
            _context.tblTrip.Add(tblTrip);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tblTripExists(tblTrip.fldTripID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GettblTrip", new { id = tblTrip.fldTripID }, tblTrip);
        }

        // DELETE: api/tblTrips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletetblTrip(int id)
        {
            var tblTrip = await _context.tblTrip.FindAsync(id);
            if (tblTrip == null)
            {
                return NotFound();
            }

            _context.tblTrip.Remove(tblTrip);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool tblTripExists(int id)
        {
            return _context.tblTrip.Any(e => e.fldTripID == id);
        }
    }
}
