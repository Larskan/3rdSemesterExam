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
    [Route("api/")]
    [ApiController]
    public class UserExpensesController : ControllerBase
    {
        private readonly FairShareContext _context;

        public UserExpensesController(FairShareContext context)
        {
            _context = context;
        }

        // GET: api/UserExpenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserExpenses>>> GetUserExpenses()
        {
            return await _context.UserExpenses.ToListAsync();
        }

        // GET: api/UserExpenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserExpenses>> GetUserExpenses(int id)
        {
            var userExpenses = await _context.UserExpenses.FindAsync(id);

            if (userExpenses == null)
            {
                return NotFound();
            }

            return userExpenses;
        }

        // PUT: api/UserExpenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserExpenses(int id, UserExpenses userExpenses)
        {
            if (id != userExpenses.Id)
            {
                return BadRequest();
            }

            _context.Entry(userExpenses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExpensesExists(id))
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

        // POST: api/UserExpenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserExpenses>> PostUserExpenses(UserExpenses userExpenses)
        {
            _context.UserExpenses.Add(userExpenses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserExpenses", new { id = userExpenses.Id }, userExpenses);
        }

        // DELETE: api/UserExpenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserExpenses(int id)
        {
            var userExpenses = await _context.UserExpenses.FindAsync(id);
            if (userExpenses == null)
            {
                return NotFound();
            }

            _context.UserExpenses.Remove(userExpenses);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExpensesExists(int id)
        {
            return _context.UserExpenses.Any(e => e.Id == id);
        }
    }
}
