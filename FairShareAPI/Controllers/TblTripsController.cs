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
    [Route("[controller]")]
    [ApiController]
    public class TblTripsController : ControllerBase
    {
        private readonly FairShareDbContext _context;

        public TblTripsController(FairShareDbContext context)
        {
            _context = context;
        }

        // GET: api/TblTrips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblTrip>>> GetTblTrips()
        {
            return await _context.TblTrips.ToListAsync();
        }

        // GET: api/TblTrips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblTrip>> GetTblTrip(int id)
        {
            var tblTrip = await _context.TblTrips.FindAsync(id);

            if (tblTrip == null)
            {
                return NotFound();
            }

            return tblTrip;
        }

        // PUT: api/TblTrips/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblTrip(int id, TblTrip tblTrip)
        {
            if (id != tblTrip.FldTripId)
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
                if (!TblTripExists(id))
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

        // POST: api/TblTrips
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblTrip>> PostTblTrip(TblTrip tblTrip)
        {
            _context.TblTrips.Add(tblTrip);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblTrip", new { id = tblTrip.FldTripId }, tblTrip);
        }

        // DELETE: api/TblTrips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblTrip(int id)
        {
            var tblTrip = await _context.TblTrips.FindAsync(id);
            if (tblTrip == null)
            {
                return NotFound();
            }

            _context.TblTrips.Remove(tblTrip);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblTripExists(int id)
        {
            return _context.TblTrips.Any(e => e.FldTripId == id);
        }
    }
}
