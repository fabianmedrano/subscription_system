using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using subscription_system.Areas.Admin.Models.ViewModel;
using subscription_system.Data;
using subscription_system.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;

namespace subscription_system.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PlansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminPlanCreateViewModels
        public async Task<IActionResult> Index()
        {
            if(_context.Plan == null) return Problem("Entity set 'ApplicationDbContext.AdminPlanCreateViewModel'  is null.");
            
            var plans = await _context.Plan.ToListAsync();

            List<AdminPlanViewModel> plansModel = new List<AdminPlanViewModel>();

            foreach (var item in plans)
            {

                AdminPlanViewModel plan = new AdminPlanViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    Active = item.Active,
                    BillingPeriod = item.BillingPeriod,
                    TrialPeriod = item.TrialPeriod
                };
                plansModel.Add( plan);
            }
            return View(plansModel);
        }

        // GET: Admin/AdminPlanCreateViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Plan == null)
            {
                return NotFound();
            }

            var plan = await _context.Plan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plan == null)
            {
                return NotFound();
            }
            AdminPlanViewModel adminPlanCreateViewModel = new AdminPlanViewModel
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                Price = plan.Price,
                Active = plan.Active,
                BillingPeriod = plan.BillingPeriod,
                TrialPeriod = plan.TrialPeriod
            };

            return View(adminPlanCreateViewModel);
        }

        // GET: Admin/AdminPlanCreateViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminPlanCreateViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Active,BillingPeriod,TrialPeriod")] AdminPlanViewModel adminPlanCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                Plan plan = new Plan
                {
                    Name = adminPlanCreateViewModel.Name,
                    Description = adminPlanCreateViewModel.Description,
                    Price = adminPlanCreateViewModel.Price,
                    Active = adminPlanCreateViewModel.Active,
                    BillingPeriod = adminPlanCreateViewModel.BillingPeriod,
                    TrialPeriod = adminPlanCreateViewModel.TrialPeriod
                };
                _context.Add(plan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adminPlanCreateViewModel);
        }

        // GET: Admin/AdminPlanCreateViewModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Plan == null)
            {
                return NotFound();
            }

            var adminPlanCreateViewModel = await _context.Plan.FindAsync(id);
            if (adminPlanCreateViewModel == null)
                return NotFound();

            AdminPlanViewModel plan = new AdminPlanViewModel{
                Id = adminPlanCreateViewModel.Id,
                Name = adminPlanCreateViewModel.Name,
                Description = adminPlanCreateViewModel.Description,
                Price = adminPlanCreateViewModel.Price,
                Active = adminPlanCreateViewModel.Active,
                BillingPeriod = adminPlanCreateViewModel.BillingPeriod,
                TrialPeriod = adminPlanCreateViewModel.TrialPeriod
            };
            return View(plan);
        }

        // POST: Admin/AdminPlanCreateViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,Active,BillingPeriod,TrialPeriod")] AdminPlanViewModel adminPlanCreateViewModel)
        {
            if (id != adminPlanCreateViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Plan plan = new Plan
                    {
                        Id= adminPlanCreateViewModel.Id,
                        Name = adminPlanCreateViewModel.Name,
                        Description = adminPlanCreateViewModel.Description,
                        Price = adminPlanCreateViewModel.Price,
                        Active = adminPlanCreateViewModel.Active,
                        BillingPeriod = adminPlanCreateViewModel.BillingPeriod,
                        TrialPeriod = adminPlanCreateViewModel.TrialPeriod
                    };
                    _context.Update(plan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminPlanCreateViewModelExists(adminPlanCreateViewModel.Id))
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
            return View(adminPlanCreateViewModel);
        }

        // GET: Admin/AdminPlanCreateViewModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Plan == null)
            {
                return NotFound();
            }

            var plan = await _context.Plan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plan == null)
            {
                return NotFound();
            }

            return View(plan);
        }

        // POST: Admin/AdminPlanCreateViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AdminPlanCreateViewModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AdminPlanCreateViewModel'  is null.");
            }
            var Plan = await _context.Plan.FindAsync(id);
            if (Plan != null)
            {
                _context.Plan.Remove(Plan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminPlanCreateViewModelExists(int id)
        {
            return (_context.Plan?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
