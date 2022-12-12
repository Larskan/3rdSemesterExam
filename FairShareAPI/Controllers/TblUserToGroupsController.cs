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
    public class tblUserToGroupsController : ControllerBase
    {
        private readonly FairShareDbContext _context;

        public tblUserToGroupsController(FairShareDbContext context)
        {
            _context = context;
        }

        // GET: api/tblUserToGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<tblUserToGroup>>> GettblUserToGroups()
        {
            return await _context.tblUserToGroups.ToListAsync();
        }

        // GET: api/tblUserToGroup/5
        [HttpGet("{id}")]
        public async Task<ActionResult<tblUserToGroup>> GettblUserToGroup(int id)
        {
            var tblUserToGroup = await _context.tblUserToGroups.FindAsync(id);

            if (tblUserToGroup == null)
            {
                return NotFound();
            }

            return tblUserToGroup;
        }

        // PUT: api/tblUserToGroup/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PuttblUserToGroup(int id, tblUserToGroup tblUserToGroup)
        {
            if (id != tblUserToGroup.fldUserToGroupId)
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

        // POST: api/tblUserToGroup
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<tblUserToGroup>> PosttblUserToGroup(tblUserToGroup tblUserToGroup)
        {
            _context.tblUserToGroups.Add(tblUserToGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GettblUserToGroup", new { id = tblUserToGroup.fldUserToGroupId }, tblUserToGroup);
        }

        // DELETE: api/tblUserToGroup/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletetblUserToGroup(int id)
        {
            var tblUserToGroup = await _context.tblUserToGroups.FindAsync(id);
            if (tblUserToGroup == null)
            {
                return NotFound();
            }

            _context.tblUserToGroups.Remove(tblUserToGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool tblUserToGroupExists(int id)
        {
            return _context.tblUserToGroups.Any(e => e.fldUserToGroupId == id);
        }
    }
}
