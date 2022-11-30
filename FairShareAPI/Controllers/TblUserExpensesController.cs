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
    public class TblUserExpensesController : ControllerBase
    {
        private readonly FairShareDbContext _context;

        public TblUserExpensesController(FairShareDbContext context)
        {
            _context = context;
        }

        // GET: api/TblUserExpenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblUserExpense>>> GetTblUserExpenses()
        {
            return await _context.TblUserExpenses.ToListAsync();
        }

        // GET: api/TblUserExpenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblUserExpense>> GetTblUserExpense(int id)
        {
            var tblUserExpense = await _context.TblUserExpenses.FindAsync(id);

            if (tblUserExpense == null)
            {
                return NotFound();
            }

            return tblUserExpense;
        }

        // PUT: api/TblUserExpenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblUserExpense(int id, TblUserExpense tblUserExpense)
        {
            if (id != tblUserExpense.FldExpensesId)
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
                if (!TblUserExpenseExists(id))
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

        // POST: api/TblUserExpenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblUserExpense>> PostTblUserExpense(TblUserExpense tblUserExpense)
        {
            _context.TblUserExpenses.Add(tblUserExpense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblUserExpense", new { id = tblUserExpense.FldExpensesId }, tblUserExpense);
        }

        // DELETE: api/TblUserExpenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblUserExpense(int id)
        {
            var tblUserExpense = await _context.TblUserExpenses.FindAsync(id);
            if (tblUserExpense == null)
            {
                return NotFound();
            }

            _context.TblUserExpenses.Remove(tblUserExpense);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblUserExpenseExists(int id)
        {
            return _context.TblUserExpenses.Any(e => e.FldExpensesId == id);
        }
    }
}
