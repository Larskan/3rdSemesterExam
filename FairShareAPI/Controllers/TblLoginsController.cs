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
    public class tblLoginsController : ControllerBase
    {
        private readonly FairShareDbContext _context;

        public tblLoginsController(FairShareDbContext context)
        {
            _context = context;
        }

        // GET: api/tblLogins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<tblLogin>>> GettblLogins()
        {
            return await _context.tblLogins.ToListAsync();
        }

        // GET: api/tblLogins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<tblLogin>> GettblLogin(int id)
        {
            var tblLogin = await _context.tblLogins.FindAsync(id);

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
            if (id != tblLogin.fldLoginId)
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
            _context.tblLogins.Add(tblLogin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GettblLogin", new { id = tblLogin.fldLoginId }, tblLogin);
        }

        // DELETE: api/tblLogins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletetblLogin(int id)
        {
            var tblLogin = await _context.tblLogins.FindAsync(id);
            if (tblLogin == null)
            {
                return NotFound();
            }

            _context.tblLogins.Remove(tblLogin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool tblLoginExists(int id)
        {
            return _context.tblLogins.Any(e => e.fldLoginId == id);
        }
    }
}
