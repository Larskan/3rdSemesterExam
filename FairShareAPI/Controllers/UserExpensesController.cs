using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FairShareAPI.Data;
using FairShareAPI.Models;

namespace FairShareAPI.Controllers
{
    public class UserExpensesController : Controller
    {
        private readonly FairShareContext _context;

        public UserExpensesController(FairShareContext context)
        {
            _context = context;
        }

        // GET: UserExpenses
        public async Task<IActionResult> Index()
        {
              return View(await _context.UserExpenses.ToListAsync());
        }

        // GET: UserExpenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserExpenses == null)
            {
                return NotFound();
            }

            var userExpenses = await _context.UserExpenses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userExpenses == null)
            {
                return NotFound();
            }

            return View(userExpenses);
        }

        // GET: UserExpenses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserExpenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Expense,Note,ExpenseDate")] UserExpenses userExpenses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userExpenses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userExpenses);
        }

        // GET: UserExpenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserExpenses == null)
            {
                return NotFound();
            }

            var userExpenses = await _context.UserExpenses.FindAsync(id);
            if (userExpenses == null)
            {
                return NotFound();
            }
            return View(userExpenses);
        }

        // POST: UserExpenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Expense,Note,ExpenseDate")] UserExpenses userExpenses)
        {
            if (id != userExpenses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userExpenses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExpensesExists(userExpenses.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userExpenses);
        }

        // GET: UserExpenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserExpenses == null)
            {
                return NotFound();
            }

            var userExpenses = await _context.UserExpenses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userExpenses == null)
            {
                return NotFound();
            }

            return View(userExpenses);
        }

        // POST: UserExpenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserExpenses == null)
            {
                return Problem("Entity set 'FairShareContext.UserExpenses'  is null.");
            }
            var userExpenses = await _context.UserExpenses.FindAsync(id);
            if (userExpenses != null)
            {
                _context.UserExpenses.Remove(userExpenses);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExpensesExists(int id)
        {
          return _context.UserExpenses.Any(e => e.Id == id);
        }
    }
}
