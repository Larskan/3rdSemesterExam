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
    public class tblGroupsController : ControllerBase
    {
        private readonly FairShareDbContext _context;

        public tblGroupsController(FairShareDbContext context)
        {
            _context = context;
        }

        // GET: api/tblGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<tblGroup>>> GettblGroups()
        {
            return await _context.tblGroups.ToListAsync();
        }

        // GET: api/tblGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<tblGroup>> GettblGroup(int id)
        {
            var tblGroup = await _context.tblGroups.FindAsync(id);

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
            if (id != tblGroup.fldGroupId)
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
            _context.tblGroups.Add(tblGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GettblGroup", new { id = tblGroup.fldGroupId }, tblGroup);
        }

        // DELETE: api/tblGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletetblGroup(int id)
        {
            var tblGroup = await _context.tblGroups.FindAsync(id);
            if (tblGroup == null)
            {
                return NotFound();
            }

            _context.tblGroups.Remove(tblGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool tblGroupExists(int id)
        {
            return _context.tblGroups.Any(e => e.fldGroupId == id);
        }
    }
}
