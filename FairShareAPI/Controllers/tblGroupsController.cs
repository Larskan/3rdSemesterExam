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
    public class tblGroupsController : ControllerBase
    {
        private readonly FairShareContext _context;

        public tblGroupsController(FairShareContext context)
        {
            _context = context;
        }

        // GET: api/tblGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<tblGroup>>> GettblGroup()
        {
            return await _context.tblGroup.ToListAsync();
        }

        // GET: api/tblGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<tblGroup>> GettblGroup(int id)
        {
            var tblGroup = await _context.tblGroup.FindAsync(id);

            if (tblGroup == null)
            {
                return NotFound();
            }

            return tblGroup;
        }

        // PUT: api/tblGroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PuttblGroup(int id, tblGroup tblGroup)
        {
            if (id != tblGroup.fldGroupID)
            {
                return BadRequest();
            }

            _context.Entry(tblGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblGroupExists(id))
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

        // POST: api/tblGroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<tblGroup>> PosttblGroup(tblGroup tblGroup)
        {
            _context.tblGroup.Add(tblGroup);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tblGroupExists(tblGroup.fldGroupID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GettblGroup", new { id = tblGroup.fldGroupID }, tblGroup);
        }

        // DELETE: api/tblGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletetblGroup(int id)
        {
            var tblGroup = await _context.tblGroup.FindAsync(id);
            if (tblGroup == null)
            {
                return NotFound();
            }

            _context.tblGroup.Remove(tblGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool tblGroupExists(int id)
        {
            return _context.tblGroup.Any(e => e.fldGroupID == id);
        }
    }
}
