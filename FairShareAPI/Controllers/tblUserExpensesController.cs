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
    public class tblUserExpensesController : ControllerBase
    {
        private readonly FairShareContext _context;

        public tblUserExpensesController(FairShareContext context)
        {
            _context = context;
        }

        // GET: api/tblUserExpenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<tblUserExpenses>>> GettblUserExpenses()
        {
            return await _context.tblUserExpenses.ToListAsync();
        }

        // GET: api/tblUserExpenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<tblUserExpenses>> GettblUserExpenses(int id)
        {
            var tblUserExpenses = await _context.tblUserExpenses.FindAsync(id);

            if (tblUserExpenses == null)
            {
                return NotFound();
            }

            return tblUserExpenses;
        }

        // PUT: api/tblUserExpenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PuttblUserExpenses(int id, tblUserExpenses tblUserExpenses)
        {
            if (id != tblUserExpenses.fldExpensesID)
            {
                return BadRequest();
            }

            _context.Entry(tblUserExpenses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblUserExpensesExists(id))
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

        // POST: api/tblUserExpenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<tblUserExpenses>> PosttblUserExpenses(tblUserExpenses tblUserExpenses)
        {
            _context.tblUserExpenses.Add(tblUserExpenses);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tblUserExpensesExists(tblUserExpenses.fldExpensesID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GettblUserExpenses", new { id = tblUserExpenses.fldExpensesID }, tblUserExpenses);
        }

        // DELETE: api/tblUserExpenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletetblUserExpenses(int id)
        {
            var tblUserExpenses = await _context.tblUserExpenses.FindAsync(id);
            if (tblUserExpenses == null)
            {
                return NotFound();
            }

            _context.tblUserExpenses.Remove(tblUserExpenses);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool tblUserExpensesExists(int id)
        {
            return _context.tblUserExpenses.Any(e => e.fldExpensesID == id);
        }
    }
}
