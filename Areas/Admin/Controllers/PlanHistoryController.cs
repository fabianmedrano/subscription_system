using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using subscription_system.Areas.Admin.Models.ViewModel.PlanHistory;
using subscription_system.Data;
using subscription_system.Models;

namespace subscription_system.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PlanHistoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public static Plan? plan;

        public PlanHistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminPlanHistoryViewModels
        public async Task<IActionResult> Index(int? id)
            {
            if (_context.PlanHistory == null) return Problem("Entity set 'ApplicationDbContext.PlanHistory'  is null.");

            var PlanHistory = await _context.PlanHistory.Where(plan => plan.Id == id).Include(a => a.Plan).ToListAsync();


            List<AdminPlanHistoryViewModel> planHistory = new();


            /*
            foreach (var item in applicationDbContext)
            {
                
            }*/
            await this.SetPlan(id);
            ViewData["plan"] = plan!.Name;
            /**/

            /**/
            return View( planHistory);

        }

        // GET: Admin/AdminPlanHistoryViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PlanHistory == null)
            {
                return NotFound();
            }

            var adminPlanHistoryViewModel = await _context.PlanHistory
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
            ViewData["plan"] = plan!.Name;
            /* var value= new SelectList(_context.Plan, "Id", "Name");
             ViewData["PlanId"] = value;
             */
            AdminPlanHistoryViewModel adminPlanHistoryViewModel = new ();
            return View(adminPlanHistoryViewModel);
        }

        // POST: Admin/AdminPlanHistoryViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ChangeDate,OldDescription,NewDescription")] AdminPlanHistoryViewModel adminPlanHistoryViewModel)
        {
            if (ModelState.IsValid)
            {
                PlanHistory planHistory = new () {
                    PlanId = plan!.Id,
                    OldDescription =plan.Description,
                    NewDescription = adminPlanHistoryViewModel.NewDescription
                };
                _context.Add(planHistory);

                plan.Description = adminPlanHistoryViewModel.NewDescription;
                _context.Update(plan);

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
        private async Task  SetPlan(int? id) {
            if (id == null)  NotFound();
            plan = await _context.Plan.FirstOrDefaultAsync(m => m.Id == id); 
        }
    }
}
