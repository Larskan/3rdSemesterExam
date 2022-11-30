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
    public class TblUserToGroupsController : ControllerBase
    {
        private readonly FairShareDbContext _context;

        public TblUserToGroupsController(FairShareDbContext context)
        {
            _context = context;
        }

        // GET: api/TblUserToGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblUserToGroup>>> GetTblUserToGroups()
        {
            return await _context.TblUserToGroups.ToListAsync();
        }

        // GET: api/TblUserToGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblUserToGroup>> GetTblUserToGroup(int id)
        {
            var tblUserToGroup = await _context.TblUserToGroups.FindAsync(id);

            if (tblUserToGroup == null)
            {
                return NotFound();
            }

            return tblUserToGroup;
        }

        // PUT: api/TblUserToGroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblUserToGroup(int id, TblUserToGroup tblUserToGroup)
        {
            if (id != tblUserToGroup.FldUserToGroupId)
            {
                return BadRequest();
            }

            _context.Entry(tblUserToGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblUserToGroupExists(id))
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

        // POST: api/TblUserToGroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblUserToGroup>> PostTblUserToGroup(TblUserToGroup tblUserToGroup)
        {
            _context.TblUserToGroups.Add(tblUserToGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblUserToGroup", new { id = tblUserToGroup.FldUserToGroupId }, tblUserToGroup);
        }

        // DELETE: api/TblUserToGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblUserToGroup(int id)
        {
            var tblUserToGroup = await _context.TblUserToGroups.FindAsync(id);
            if (tblUserToGroup == null)
            {
                return NotFound();
            }

            _context.TblUserToGroups.Remove(tblUserToGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblUserToGroupExists(int id)
        {
            return _context.TblUserToGroups.Any(e => e.FldUserToGroupId == id);
        }
    }
}
