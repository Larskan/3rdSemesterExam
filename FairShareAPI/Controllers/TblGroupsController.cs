using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FairShareAPI.Data;
using FairShareAPI.Models;
using Newtonsoft.Json;

namespace FairShareAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TblGroupsController : ControllerBase
    {
        private readonly FairShareDbContext _context;

        public TblGroupsController(FairShareDbContext context)
        {
            _context = context;
        }

        // GET: api/TblGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblGroup>>> GetTblGroups()
        {
            return await _context.TblGroups.ToListAsync();
        }

       
        // GET: api/TblGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblGroup>> GetTblGroup(int id)
        {
            var tblGroup = await _context.TblGroups.FindAsync(id);

            if (tblGroup == null)
            {
                return NotFound();
            }

            return tblGroup;
        }

        // PUT: api/TblGroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblGroup(int id, TblGroup tblGroup)
        {
            if (id != tblGroup.FldGroupId)
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
                if (!TblGroupExists(id))
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

        // POST: api/TblGroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblGroup>> PostTblGroup(TblGroup tblGroup)
        {
            _context.TblGroups.Add(tblGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblGroup", new { id = tblGroup.FldGroupId }, tblGroup);
        }

        // DELETE: api/TblGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblGroup(int id)
        {
            var tblGroup = await _context.TblGroups.FindAsync(id);
            if (tblGroup == null)
            {
                return NotFound();
            }

            _context.TblGroups.Remove(tblGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblGroupExists(int id)
        {
            return _context.TblGroups.Any(e => e.FldGroupId == id);
        }
    }
}
