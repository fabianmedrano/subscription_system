using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using subscription_system.Data;
using subscription_system.Models;

namespace subscription_system.Areas.Admin.Controllers
{
    [Authorize(Roles = "SystemAdmin")]
    [Area("Admin")]
    public class PriceHistoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PriceHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/PriceHistories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PriceHistory.Include(p => p.Plan);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/PriceHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PriceHistory == null)
            {
                return NotFound();
            }

            var priceHistory = await _context.PriceHistory
                .Include(p => p.Plan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (priceHistory == null)
            {
                return NotFound();
            }

            return View(priceHistory);
        }

        // GET: Admin/PriceHistories/Create
        public IActionResult Create()
        {
            ViewData["PlanId"] = new SelectList(_context.Plan, "Id", "Description");
            return View();
        }

        // POST: Admin/PriceHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlanId,OldPrice,NewPrice,ChangeDate")] PriceHistory priceHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(priceHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlanId"] = new SelectList(_context.Plan, "Id", "Description", priceHistory.PlanId);
            return View(priceHistory);
        }

        // GET: Admin/PriceHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PriceHistory == null)
            {
                return NotFound();
            }

            var priceHistory = await _context.PriceHistory.FindAsync(id);
            if (priceHistory == null)
            {
                return NotFound();
            }
            ViewData["PlanId"] = new SelectList(_context.Plan, "Id", "Description", priceHistory.PlanId);
            return View(priceHistory);
        }

        // POST: Admin/PriceHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlanId,OldPrice,NewPrice,ChangeDate")] PriceHistory priceHistory)
        {
            if (id != priceHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(priceHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriceHistoryExists(priceHistory.Id))
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
            ViewData["PlanId"] = new SelectList(_context.Plan, "Id", "Description", priceHistory.PlanId);
            return View(priceHistory);
        }

        // GET: Admin/PriceHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PriceHistory == null)
            {
                return NotFound();
            }

            var priceHistory = await _context.PriceHistory
                .Include(p => p.Plan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (priceHistory == null)
            {
                return NotFound();
            }

            return View(priceHistory);
        }

        // POST: Admin/PriceHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PriceHistory == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PriceHistory'  is null.");
            }
            var priceHistory = await _context.PriceHistory.FindAsync(id);
            if (priceHistory != null)
            {
                _context.PriceHistory.Remove(priceHistory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriceHistoryExists(int id)
        {
            return (_context.PriceHistory?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
