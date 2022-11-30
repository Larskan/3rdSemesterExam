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
    public class TblUsersController : ControllerBase
    {
        private readonly FairShareDbContext _context;

        public TblUsersController(FairShareDbContext context)
        {
            _context = context;
        }

        // GET: api/TblUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblUser>>> GetTblUsers()
        {
            return await _context.TblUsers.ToListAsync();
        }

        // GET: api/TblUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblUser>> GetTblUser(int id)
        {
            var tblUser = await _context.TblUsers.FindAsync(id);

            if (tblUser == null)
            {
                return NotFound();
            }

            return tblUser;
        }

        // PUT: api/TblUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblUser(int id, TblUser tblUser)
        {
            if (id != tblUser.FldUserId)
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
                if (!TblUserExists(id))
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

        // POST: api/TblUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblUser>> PostTblUser(TblUser tblUser)
        {
            _context.TblUsers.Add(tblUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblUser", new { id = tblUser.FldUserId }, tblUser);
        }

        // DELETE: api/TblUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblUser(int id)
        {
            var tblUser = await _context.TblUsers.FindAsync(id);
            if (tblUser == null)
            {
                return NotFound();
            }

            _context.TblUsers.Remove(tblUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblUserExists(int id)
        {
            return _context.TblUsers.Any(e => e.FldUserId == id);
        }
    }
}
