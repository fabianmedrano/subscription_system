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
using subscription_system.Data;
using subscription_system.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using subscription_system.Extensions;
using subscription_system.Areas.Admin.Models.ViewModel.Plan;

namespace subscription_system.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PlansController : BaseController
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

            List<PlanViewModel> plansModel = new List<PlanViewModel>();

            foreach (var item in plans)
            {

                PlanViewModel plan = new PlanViewModel
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
            PlanViewModel adminPlanCreateViewModel = new PlanViewModel
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
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Active,BillingPeriod,TrialPeriod")] PlanViewModel adminPlanCreateViewModel)
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
                return RedirectToAction(nameof(Index), "PlanFeatures", new { area = "Admin", planId = plan.Id });
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



            PlanViewModel plan = new PlanViewModel{
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,Active,BillingPeriod,TrialPeriod")] PlanViewModel adminPlanCreateViewModel)
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
                    /* Rastreo de campos distintos*/
                     List<string> changes=changeTracker(plan);

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
             if (_context.AdminPlanCreateViewModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AdminPlanCreateViewModel'  is null.");
            }
              
            var Plan = await _context.Plan.FindAsync(id);
            if (Plan == null)
            {
                return NotFound();
            }

            bool exist = await _context.Subscriptions.AnyAsync(s => s.PlanId == id);
            if (exist) Alert(Enums.NotificationType.Error, "No es posible eliminar el plan, debido aque existen subscripciones activas con este","Accion no disponible");
            
            _context.Plan.Remove(Plan);
            await _context.SaveChangesAsync();
            

            Alert(Enums.NotificationType.Success,"Plan eliminado correctamente" );
            return RedirectToAction(nameof(Index));
        }

        // POST: Admin/AdminPlanCreateViewModels/Delete/5
      
        private bool AdminPlanCreateViewModelExists(int id)
        {
            return (_context.Plan?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private List<string> changeTracker<T>(T model) where T : class
        {
            var entry = _context.Entry(model);

            var changedProperties = new List<string>();

            foreach (var prop in entry.Properties)
            {
                if (prop.IsModified)
                {
                    changedProperties.Add(prop.Metadata.Name);
                }
            }
            return changedProperties;
        }
    }


}
