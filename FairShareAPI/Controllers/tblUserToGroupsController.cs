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
    public class tblUserToGroupsController : ControllerBase
    {
        private readonly FairShareContext _context;

        public tblUserToGroupsController(FairShareContext context)
        {
            _context = context;
        }

        // GET: api/tblUserToGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<tblUserToGroup>>> GettblUserToGroup()
        {
            return await _context.tblUserToGroup.ToListAsync();
        }

        // GET: api/tblUserToGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<tblUserToGroup>> GettblUserToGroup(int id)
        {
            var tblUserToGroup = await _context.tblUserToGroup.FindAsync(id);

            if (tblUserToGroup == null)
            {
                return NotFound();
            }

            return tblUserToGroup;
        }

        // PUT: api/tblUserToGroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PuttblUserToGroup(int id, tblUserToGroup tblUserToGroup)
        {
            if (id != tblUserToGroup.fldUserToGroupID)
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
                if (!tblUserToGroupExists(id))
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

        // POST: api/tblUserToGroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<tblUserToGroup>> PosttblUserToGroup(tblUserToGroup tblUserToGroup)
        {
            _context.tblUserToGroup.Add(tblUserToGroup);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tblUserToGroupExists(tblUserToGroup.fldUserToGroupID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GettblUserToGroup", new { id = tblUserToGroup.fldUserToGroupID }, tblUserToGroup);
        }

        // DELETE: api/tblUserToGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletetblUserToGroup(int id)
        {
            var tblUserToGroup = await _context.tblUserToGroup.FindAsync(id);
            if (tblUserToGroup == null)
            {
                return NotFound();
            }

            _context.tblUserToGroup.Remove(tblUserToGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool tblUserToGroupExists(int id)
        {
            return _context.tblUserToGroup.Any(e => e.fldUserToGroupID == id);
        }
    }
}
