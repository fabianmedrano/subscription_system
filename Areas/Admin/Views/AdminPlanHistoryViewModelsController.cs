using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using subscription_system.Areas.Admin.Models.ViewModel;
using subscription_system.Data;

namespace subscription_system.Areas.Admin.Views
{
    [Area("Admin")]
    public class AdminPlanHistoryViewModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminPlanHistoryViewModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminPlanHistoryViewModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AdminPlanHistoryViewModel.Include(a => a.Plan);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/AdminPlanHistoryViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AdminPlanHistoryViewModel == null)
            {
                return NotFound();
            }

            var adminPlanHistoryViewModel = await _context.AdminPlanHistoryViewModel
                .Include(a => a.Plan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adminPlanHistoryViewModel == null)
            {
                return NotFound();
            }

            return View(adminPlanHistoryViewModel);
        }

        // GET: Admin/AdminPlanHistoryViewModels/Create
        public IActionResult Create()
        {
            ViewData["PlanId"] = new SelectList(_context.Plan, "Id", "Description");
            return View();
        }

        // POST: Admin/AdminPlanHistoryViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlanId,ChangeDate,OldDescription,NewDescription")] AdminPlanHistoryViewModel adminPlanHistoryViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminPlanHistoryViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlanId"] = new SelectList(_context.Plan, "Id", "Description", adminPlanHistoryViewModel.PlanId);
            return View(adminPlanHistoryViewModel);
        }

        // GET: Admin/AdminPlanHistoryViewModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AdminPlanHistoryViewModel == null)
            {
                return NotFound();
            }

            var adminPlanHistoryViewModel = await _context.AdminPlanHistoryViewModel.FindAsync(id);
            if (adminPlanHistoryViewModel == null)
            {
                return NotFound();
            }
            ViewData["PlanId"] = new SelectList(_context.Plan, "Id", "Description", adminPlanHistoryViewModel.PlanId);
            return View(adminPlanHistoryViewModel);
        }

        // POST: Admin/AdminPlanHistoryViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlanId,ChangeDate,OldDescription,NewDescription")] AdminPlanHistoryViewModel adminPlanHistoryViewModel)
        {
            if (id != adminPlanHistoryViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminPlanHistoryViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminPlanHistoryViewModelExists(adminPlanHistoryViewModel.Id))
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
            ViewData["PlanId"] = new SelectList(_context.Plan, "Id", "Description", adminPlanHistoryViewModel.PlanId);
            return View(adminPlanHistoryViewModel);
        }

        // GET: Admin/AdminPlanHistoryViewModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AdminPlanHistoryViewModel == null)
            {
                return NotFound();
            }

            var adminPlanHistoryViewModel = await _context.AdminPlanHistoryViewModel
                .Include(a => a.Plan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adminPlanHistoryViewModel == null)
            {
                return NotFound();
            }

            return View(adminPlanHistoryViewModel);
        }

        // POST: Admin/AdminPlanHistoryViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AdminPlanHistoryViewModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AdminPlanHistoryViewModel'  is null.");
            }
            var adminPlanHistoryViewModel = await _context.AdminPlanHistoryViewModel.FindAsync(id);
            if (adminPlanHistoryViewModel != null)
            {
                _context.AdminPlanHistoryViewModel.Remove(adminPlanHistoryViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminPlanHistoryViewModelExists(int id)
        {
          return (_context.AdminPlanHistoryViewModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
