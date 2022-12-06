using FairShareAPI.Data;
using FairShareAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FairShareAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors]
    public class tblGroupToTripsController : ControllerBase
    {
        private readonly FairShareDbContext _context;

        public tblGroupToTripsController(FairShareDbContext context)
        {
            _context = context;
        }

        // GET: api/tblGroupToTrips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<tblGroupToTrip>>> GettblGroupToTrips()
        {
            return await _context.tblGroupToTrips.ToListAsync();
        }

        // GET: api/tblGroupToTrips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<tblGroupToTrip>> GettblGroupToTrip(int id)
        {
            var tblGroupToMoney = await _context.tblGroupToTrips.FindAsync(id);

            if (tblGroupToMoney == null)
            {
                return NotFound();
            }

            return tblGroupToMoney;
        }

        // PUT: api/tblGroupToTrips/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PuttblGroupToTrip(int id, tblGroupToTrip tblGroupToMoney)
        {
            if (id != tblGroupToMoney.fldGroupToTripId)
            {
                return BadRequest();
            }

            _context.Entry(tblGroupToMoney).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblGroupToTripExists(id))
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

        // POST: api/tblGroupToTrips
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<tblGroupToTrip>> PosttblGroupToTrip(tblGroupToTrip tblGroupToTrip)
        {
            _context.tblGroupToTrips.Add(tblGroupToTrip);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GettblGroupToTrip", new { id = tblGroupToTrip.fldGroupToTripId }, tblGroupToTrip);
        }

        // DELETE: api/tblGroupToTrips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletetblGroupToTrip(int id)
        {
            var tblGroupToTrip = await _context.tblGroupToTrips.FindAsync(id);
            if (tblGroupToTrip == null)
            {
                return NotFound();
            }

            _context.tblGroupToTrips.Remove(tblGroupToTrip);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool tblGroupToTripExists(int id)
        {
            return _context.tblGroupToTrips.Any(e => e.fldGroupToTripId == id);
        }
    }
}
