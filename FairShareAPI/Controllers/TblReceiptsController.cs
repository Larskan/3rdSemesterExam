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
    public class TblReceiptsController : ControllerBase
    {
        private readonly FairShareDbContext _context;

        public TblReceiptsController(FairShareDbContext context)
        {
            _context = context;
        }

        // GET: api/TblReceipts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblReceipt>>> GetTblReceipts()
        {
            return await _context.TblReceipts.ToListAsync();
        }

        // GET: api/TblReceipts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblReceipt>> GetTblReceipt(int id)
        {
            var tblReceipt = await _context.TblReceipts.FindAsync(id);

            if (tblReceipt == null)
            {
                return NotFound();
            }

            return tblReceipt;
        }

        // PUT: api/TblReceipts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblReceipt(int id, TblReceipt tblReceipt)
        {
            if (id != tblReceipt.FldReceiptId)
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
                if (!TblReceiptExists(id))
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

        // POST: api/TblReceipts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblReceipt>> PostTblReceipt(TblReceipt tblReceipt)
        {
            _context.TblReceipts.Add(tblReceipt);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblReceipt", new { id = tblReceipt.FldReceiptId }, tblReceipt);
        }

        // DELETE: api/TblReceipts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblReceipt(int id)
        {
            var tblReceipt = await _context.TblReceipts.FindAsync(id);
            if (tblReceipt == null)
            {
                return NotFound();
            }

            _context.TblReceipts.Remove(tblReceipt);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblReceiptExists(int id)
        {
            return _context.TblReceipts.Any(e => e.FldReceiptId == id);
        }
    }
}
