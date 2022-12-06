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
    public class tblTripToUserExpensesController : ControllerBase
    {
        private readonly FairShareDbContext _context;

        public tblTripToUserExpensesController(FairShareDbContext context)
        {
            _context = context;
        }

        // GET: api/tblTripToUserExpenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<tblTripToUserExpense>>> GettblTripToUserExpenses()
        {
            return await _context.tblTripToUserExpenses.ToListAsync();
        }

        // GET: api/tblTripToUserExpenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<tblTripToUserExpense>> GettblTripToUserExpense(int id)
        {
            var tblTripToUserExpense = await _context.tblTripToUserExpenses.FindAsync(id);

            if (tblTripToUserExpense == null)
            {
                return NotFound();
            }

            return tblTripToUserExpense;
        }

        // PUT: api/tblTripToUserExpenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PuttblTripToUserExpense(int id, tblTripToUserExpense tblTripToUserExpense)
        {
            if (id != tblTripToUserExpense.fldUserToExpense)
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
                if (!tblTripToUserExpenseExists(id))
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

        // POST: api/tblTripToUserExpenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<tblTripToUserExpense>> PosttblTripToUserExpense(tblTripToUserExpense tblTripToUserExpense)
        {
            _context.tblTripToUserExpenses.Add(tblTripToUserExpense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GettblTripToUserExpense", new { id = tblTripToUserExpense.fldUserToExpense }, tblTripToUserExpense);
        }

        // DELETE: api/tblTripToUserExpenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletetblTripToUserExpense(int id)
        {
            var tblTripToUserExpense = await _context.tblTripToUserExpenses.FindAsync(id);
            if (tblTripToUserExpense == null)
            {
                return NotFound();
            }

            _context.tblTripToUserExpenses.Remove(tblTripToUserExpense);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool tblTripToUserExpenseExists(int id)
        {
            return _context.tblTripToUserExpenses.Any(e => e.fldUserToExpense == id);
        }
    }
}
