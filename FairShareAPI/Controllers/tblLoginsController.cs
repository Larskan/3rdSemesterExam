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
    public class tblLoginsController : ControllerBase
    {
        private readonly FairShareContext _context;

        public tblLoginsController(FairShareContext context)
        {
            _context = context;
        }

        // GET: api/tblLogins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<tblLogin>>> GettblLogin()
        {
            return await _context.tblLogin.ToListAsync();
        }

        // GET: api/tblLogins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<tblLogin>> GettblLogin(int id)
        {
            var tblLogin = await _context.tblLogin.FindAsync(id);

            if (tblLogin == null)
            {
                return NotFound();
            }

            return tblLogin;
        }

        // PUT: api/tblLogins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PuttblLogin(int id, tblLogin tblLogin)
        {
            if (id != tblLogin.fldLoginID)
            {
                return BadRequest();
            }

            _context.Entry(tblLogin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblLoginExists(id))
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

        // POST: api/tblLogins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<tblLogin>> PosttblLogin(tblLogin tblLogin)
        {
            _context.tblLogin.Add(tblLogin);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tblLoginExists(tblLogin.fldLoginID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GettblLogin", new { id = tblLogin.fldLoginID }, tblLogin);
        }

        // DELETE: api/tblLogins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletetblLogin(int id)
        {
            var tblLogin = await _context.tblLogin.FindAsync(id);
            if (tblLogin == null)
            {
                return NotFound();
            }

            _context.tblLogin.Remove(tblLogin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool tblLoginExists(int id)
        {
            return _context.tblLogin.Any(e => e.fldLoginID == id);
        }
    }
}
