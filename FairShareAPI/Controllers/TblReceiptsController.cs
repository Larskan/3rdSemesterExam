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
    public class tblReceiptsController : ControllerBase
    {
        private readonly FairShareDbContext _context;

        public tblReceiptsController(FairShareDbContext context)
        {
            _context = context;
        }

        // GET: api/tblReceipts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<tblReceipt>>> GettblReceipts()
        {
            return await _context.tblReceipts.ToListAsync();
        }

        // GET: api/tblReceipts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<tblReceipt>> GettblReceipt(int id)
        {
            var tblReceipt = await _context.tblReceipts.FindAsync(id);

            if (tblReceipt == null)
            {
                return NotFound();
            }

            return tblReceipt;
        }

        // PUT: api/tblReceipts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PuttblReceipt(int id, tblReceipt tblReceipt)
        {
            if (id != tblReceipt.fldReceiptId)
            {
                return BadRequest();
            }

            _context.Entry(tblReceipt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblReceiptExists(id))
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

        // POST: api/tblReceipts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<tblReceipt>> PosttblReceipt(tblReceipt tblReceipt)
        {
            _context.tblReceipts.Add(tblReceipt);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GettblReceipt", new { id = tblReceipt.fldReceiptId }, tblReceipt);
        }

        // DELETE: api/tblReceipts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletetblReceipt(int id)
        {
            var tblReceipt = await _context.tblReceipts.FindAsync(id);
            if (tblReceipt == null)
            {
                return NotFound();
            }

            _context.tblReceipts.Remove(tblReceipt);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool tblReceiptExists(int id)
        {
            return _context.tblReceipts.Any(e => e.fldReceiptId == id);
        }
    }
}
