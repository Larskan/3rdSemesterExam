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
    [Route("/")]
    [ApiController]
    public class ReceiptsController : ControllerBase
    {
        private readonly FairShareContext _context;

        public ReceiptsController(FairShareContext context)
        {
            _context = context;
        }

        // GET: api/Receipts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Receipt>>> GetReceipt()
        {
            return await _context.Receipt.ToListAsync();
        }

        // GET: api/Receipts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Receipt>> GetReceipt(string id)
        {
            var receipt = await _context.Receipt.FindAsync(id);

            if (receipt == null)
            {
                return NotFound();
            }

            return receipt;
        }

        // PUT: api/Receipts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceipt(string id, Receipt receipt)
        {
            if (id != receipt.Id)
            {
                return BadRequest();
            }

            _context.Entry(receipt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceiptExists(id))
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

        // POST: api/Receipts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Receipt>> PostReceipt(Receipt receipt)
        {
            _context.Receipt.Add(receipt);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ReceiptExists(receipt.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetReceipt", new { id = receipt.Id }, receipt);
        }

        // DELETE: api/Receipts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceipt(string id)
        {
            var receipt = await _context.Receipt.FindAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }

            _context.Receipt.Remove(receipt);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReceiptExists(string id)
        {
            return _context.Receipt.Any(e => e.Id == id);
        }
    }
}
