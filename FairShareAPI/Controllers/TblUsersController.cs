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
    public class tblUsersController : ControllerBase
    {
        private readonly FairShareDbContext _context;

        public tblUsersController(FairShareDbContext context)
        {
            _context = context;
        }

        // GET: api/tblUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<tblUser>>> GettblUsers()
        {
            return await _context.tblUsers.ToListAsync();
        }

        // GET: api/tblUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<tblUser>> GettblUser(int id)
        {
            var tblUser = await _context.tblUsers.FindAsync(id);

            if (tblUser == null)
            {
                return NotFound();
            }

            return tblUser;
        }

        // PUT: api/tblUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PuttblUser(int id, tblUser tblUser)
        {
            if (id != tblUser.fldUserId)
            {
                return BadRequest();
            }

            _context.Entry(tblUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblUserExists(id))
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

        // POST: api/tblUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<tblUser>> PosttblUser(tblUser tblUser)
        {
            _context.tblUsers.Add(tblUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GettblUser", new { id = tblUser.fldUserId }, tblUser);
        }

        // DELETE: api/tblUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletetblUser(int id)
        {
            var tblUser = await _context.tblUsers.FindAsync(id);
            if (tblUser == null)
            {
                return NotFound();
            }

            _context.tblUsers.Remove(tblUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool tblUserExists(int id)
        {
            return _context.tblUsers.Any(e => e.fldUserId == id);
        }
    }
}
