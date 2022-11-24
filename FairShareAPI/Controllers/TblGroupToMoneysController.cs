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
    public class TblGroupToMoneysController : ControllerBase
    {
        private readonly FairShareDbContext _context;

        public TblGroupToMoneysController(FairShareDbContext context)
        {
            _context = context;
        }

        // GET: api/TblGroupToMoneys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblGroupToMoney>>> GetTblGroupToMoneys()
        {
            return await _context.TblGroupToMoneys.ToListAsync();
        }

        // GET: api/TblGroupToMoneys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblGroupToMoney>> GetTblGroupToMoney(int id)
        {
            var tblGroupToMoney = await _context.TblGroupToMoneys.FindAsync(id);

            if (tblGroupToMoney == null)
            {
                return NotFound();
            }

            return tblGroupToMoney;
        }

        // PUT: api/TblGroupToMoneys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblGroupToMoney(int id, TblGroupToMoney tblGroupToMoney)
        {
            if (id != tblGroupToMoney.FldGroupToMoneyId)
            {
                return BadRequest();
            }

            _context.Entry(tblGroupToMoney).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblGroupToMoneyExists(id))
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

        // POST: api/TblGroupToMoneys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblGroupToMoney>> PostTblGroupToMoney(TblGroupToMoney tblGroupToMoney)
        {
            _context.TblGroupToMoneys.Add(tblGroupToMoney);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblGroupToMoney", new { id = tblGroupToMoney.FldGroupToMoneyId }, tblGroupToMoney);
        }

        // DELETE: api/TblGroupToMoneys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblGroupToMoney(int id)
        {
            var tblGroupToMoney = await _context.TblGroupToMoneys.FindAsync(id);
            if (tblGroupToMoney == null)
            {
                return NotFound();
            }

            _context.TblGroupToMoneys.Remove(tblGroupToMoney);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblGroupToMoneyExists(int id)
        {
            return _context.TblGroupToMoneys.Any(e => e.FldGroupToMoneyId == id);
        }
    }
}
