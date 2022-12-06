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
    public class tblUserExpensesController : ControllerBase
    {
        private readonly FairShareDbContext _context;

        public tblUserExpensesController(FairShareDbContext context)
        {
            _context = context;
        }

        // GET: api/tblUserExpenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<tblUserExpense>>> GettblUserExpenses()
        {
            return await _context.tblUserExpenses.ToListAsync();
        }

        // GET: api/tblUserExpenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<tblUserExpense>> GettblUserExpense(int id)
        {
            var tblUserExpense = await _context.tblUserExpenses.FindAsync(id);

            if (tblUserExpense == null)
            {
                return NotFound();
            }

            return tblUserExpense;
        }

        // PUT: api/tblUserExpenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PuttblUserExpense(int id, tblUserExpense tblUserExpense)
        {
            if (id != tblUserExpense.fldExpensesId)
            {
                return BadRequest();
            }

            _context.Entry(tblUserExpense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblUserExpenseExists(id))
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
        public async Task<ActionResult<tblUserExpense>> PosttblUserExpense(tblUserExpense tblUserExpense)
        {
            _context.tblUserExpenses.Add(tblUserExpense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GettblUserExpense", new { id = tblUserExpense.fldExpensesId }, tblUserExpense);
        }

        // DELETE: api/tblUserExpenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletetblUserExpense(int id)
        {
            var tblUserExpense = await _context.tblUserExpenses.FindAsync(id);
            if (tblUserExpense == null)
            {
                return NotFound();
            }

            _context.tblUserExpenses.Remove(tblUserExpense);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool tblUserExpenseExists(int id)
        {
            return _context.tblUserExpenses.Any(e => e.fldExpensesId == id);
        }
    }
}
