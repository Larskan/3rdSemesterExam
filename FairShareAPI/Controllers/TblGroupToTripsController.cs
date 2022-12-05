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
    public class TblGroupToTripsController : ControllerBase
    {
        private readonly FairShareDbContext _context;

        public TblGroupToTripsController(FairShareDbContext context)
        {
            _context = context;
        }

        // GET: api/TblGroupToTrips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblGroupToTrip>>> GetTblGroupToTrips()
        {
            return await _context.TblGroupToTrips.ToListAsync();
        }

        // GET: api/TblGroupToTrips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblGroupToTrip>> GetTblGroupToTrip(int id)
        {
            var tblGroupToMoney = await _context.TblGroupToTrips.FindAsync(id);

            if (tblGroupToMoney == null)
            {
                return NotFound();
            }

            return tblGroupToMoney;
        }

        // PUT: api/TblGroupToTrips/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblGroupToTrip(int id, TblGroupToTrip tblGroupToMoney)
        {
            if (id != tblGroupToMoney.FldGroupToTripId)
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
                if (!TblGroupToTripExists(id))
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

        // POST: api/TblGroupToTrips
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblGroupToTrip>> PostTblGroupToTrip(TblGroupToTrip tblGroupToTrip)
        {
            _context.TblGroupToTrips.Add(tblGroupToTrip);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblGroupToTrip", new { id = tblGroupToTrip.FldGroupToTripId }, tblGroupToTrip);
        }

        // DELETE: api/TblGroupToTrips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblGroupToTrip(int id)
        {
            var tblGroupToTrip = await _context.TblGroupToTrips.FindAsync(id);
            if (tblGroupToTrip == null)
            {
                return NotFound();
            }

            _context.TblGroupToTrips.Remove(tblGroupToTrip);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblGroupToTripExists(int id)
        {
            return _context.TblGroupToTrips.Any(e => e.FldGroupToTripId == id);
        }
    }
}
