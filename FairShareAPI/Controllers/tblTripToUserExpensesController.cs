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
    public class tblTripToUserExpensesController : ControllerBase
    {
        private readonly FairShareContext _context;

        public tblTripToUserExpensesController(FairShareContext context)
        {
            _context = context;
        }

        // GET: api/tblTripToUserExpenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<tblTripToUserExpenses>>> GettblTripToUserExpenses()
        {
            return await _context.tblTripToUserExpenses.ToListAsync();
        }

        // GET: api/tblTripToUserExpenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<tblTripToUserExpenses>> GettblTripToUserExpenses(int id)
        {
            var tblTripToUserExpenses = await _context.tblTripToUserExpenses.FindAsync(id);

            if (tblTripToUserExpenses == null)
            {
                return NotFound();
            }

            return tblTripToUserExpenses;
        }

        // PUT: api/tblTripToUserExpenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PuttblTripToUserExpenses(int id, tblTripToUserExpenses tblTripToUserExpenses)
        {
            if (id != tblTripToUserExpenses.fldUserToExpense)
            {
                return BadRequest();
            }

            _context.Entry(tblTripToUserExpenses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblTripToUserExpensesExists(id))
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
        public async Task<ActionResult<tblTripToUserExpenses>> PosttblTripToUserExpenses(tblTripToUserExpenses tblTripToUserExpenses)
        {
            _context.tblTripToUserExpenses.Add(tblTripToUserExpenses);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tblTripToUserExpensesExists(tblTripToUserExpenses.fldUserToExpense))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GettblTripToUserExpenses", new { id = tblTripToUserExpenses.fldUserToExpense }, tblTripToUserExpenses);
        }

        // DELETE: api/tblTripToUserExpenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletetblTripToUserExpenses(int id)
        {
            var tblTripToUserExpenses = await _context.tblTripToUserExpenses.FindAsync(id);
            if (tblTripToUserExpenses == null)
            {
                return NotFound();
            }

            _context.tblTripToUserExpenses.Remove(tblTripToUserExpenses);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool tblTripToUserExpensesExists(int id)
        {
            return _context.tblTripToUserExpenses.Any(e => e.fldUserToExpense == id);
        }
    }
}
