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
    public class TblTripToUserExpensesController : ControllerBase
    {
        private readonly FairShareDbContext _context;

        public TblTripToUserExpensesController(FairShareDbContext context)
        {
            _context = context;
        }

        // GET: api/TblTripToUserExpenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblTripToUserExpense>>> GetTblTripToUserExpenses()
        {
            return await _context.TblTripToUserExpenses.ToListAsync();
        }

        // GET: api/TblTripToUserExpenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblTripToUserExpense>> GetTblTripToUserExpense(int id)
        {
            var tblTripToUserExpense = await _context.TblTripToUserExpenses.FindAsync(id);

            if (tblTripToUserExpense == null)
            {
                return NotFound();
            }

            return tblTripToUserExpense;
        }

        // PUT: api/TblTripToUserExpenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblTripToUserExpense(int id, TblTripToUserExpense tblTripToUserExpense)
        {
            if (id != tblTripToUserExpense.FldUserToExpense)
            {
                return BadRequest();
            }

            _context.Entry(tblTripToUserExpense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblTripToUserExpenseExists(id))
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

        // POST: api/TblTripToUserExpenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblTripToUserExpense>> PostTblTripToUserExpense(TblTripToUserExpense tblTripToUserExpense)
        {
            _context.TblTripToUserExpenses.Add(tblTripToUserExpense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblTripToUserExpense", new { id = tblTripToUserExpense.FldUserToExpense }, tblTripToUserExpense);
        }

        // DELETE: api/TblTripToUserExpenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblTripToUserExpense(int id)
        {
            var tblTripToUserExpense = await _context.TblTripToUserExpenses.FindAsync(id);
            if (tblTripToUserExpense == null)
            {
                return NotFound();
            }

            _context.TblTripToUserExpenses.Remove(tblTripToUserExpense);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblTripToUserExpenseExists(int id)
        {
            return _context.TblTripToUserExpenses.Any(e => e.FldUserToExpense == id);
        }
    }
}
