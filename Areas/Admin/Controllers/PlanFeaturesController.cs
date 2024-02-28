﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using subscription_system.Data;
using subscription_system.Extensions;
using subscription_system.Models;
using subscription_system.Areas.Admin.Models.ViewModel.PlanFeature;
using Microsoft.Extensions.Logging;
using Riok.Mapperly;
using subscription_system.Mapper;
using static NuGet.Packaging.PackagingConstants;
using subscription_system.Areas.Admin.Models.ViewModel.Feature;
using System.ComponentModel.DataAnnotations;
namespace subscription_system.Areas.Admin.Controllers
{

    //   [Authorize(Roles = "SystemAdmin")]
    [Area("Admin")]
    [Route("Admin/Plan/{planId}/PlanFeatures/{action}/{id?}")]
    public class PlanFeaturesController : BaseController
    {
        private readonly ApplicationDbContext _context;
        
        public PlanFeaturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/PlanFeatures
        public async Task<IActionResult> Index(int planId)
        {
            Task<Plan> plantask = getPlan(planId);
       
            var applicationDbContext = _context.Feature
                .Join(_context.PlanFeature,  f=> f.Id ,pf=> pf.PlanId, (f, pf) => new { f.Id,f.Description,f.Name, pf.PlanId })
                .Where(p => p.PlanId == planId).Select( (f) => new Feature { Id = f.Id,Name =f.Name,Description =f.Description} );


            Plan plan = await plantask;
            if (plan != null)
                ViewData["PlanName"] = plan.Name;

            var planFeature = await applicationDbContext.ToListAsync();

            PlanFeatureMapper map =new PlanFeatureMapper();
            var planFeatureView = (planFeature != null)?map.mapList(planFeature): new List<FeatureViewModel>();
            PlanFeaturesViewModel PlanFeaturesViewModel = new PlanFeaturesViewModel
            {
                //TODO:ESTA PARTE DEL CODIGO DEBE DE MEJORAR
      Id = plan!.Id,
      Name = plan!.Name,
       Description = plan!.Name,
                Price = plan!.Price,
                Active = plan!.Active,

                BillingPeriod = plan!.BillingPeriod,

                TrialPeriod  = plan!.TrialPeriod,

                Features = planFeatureView
            };
            return View(PlanFeaturesViewModel!);
        }

        // GET: Admin/PlanFeatures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PlanFeature == null)
            {
                return NotFound();
            }

            var planFeature = await _context.PlanFeature
                .Include(p => p.Feature)
                .Include(p => p.Plan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planFeature == null)
            {
                return NotFound();
            }

            return View(planFeature);
        }

        // GET: Admin/PlanFeatures/Create
        public async Task<IActionResult>  Create(int planId)
        {
            Task<Plan> plantask = getPlan(planId);
            ViewData["FeatureId"] = new SelectList(_context.Feature, "Id", "Description");
            Plan plan =await plantask;
            if (plan != null)
                ViewData["PlanName"] = plan.Name;

            return View();
        }

        // POST: Admin/PlanFeatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int planId , [Bind("Id,FeatureId")] PlanFeatureViewModel model )
        {
            
            
            if (ModelState.IsValid)
            {
                PlanFeatureViewModel planFeature = new PlanFeatureViewModel
                {
                    PlanId = planId,
                    FeatureId = model.FeatureId
                };
                _context.Add(planFeature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           /* ViewData["FeatureId"] = new SelectList(_context.Feature, "Id", "Description", planFeature.FeatureId);
            ViewData["PlanId"] = new SelectList(_context.Plan, "Id", "Description");*/
            return View(model);
        }

        // GET: Admin/PlanFeatures/Edit/5
        public async Task<IActionResult> Edit(int planId, int? id)
        {
            if (id == null || _context.PlanFeature == null)
            {
                return NotFound();
            }


            ViewData["FeatureId"] = new SelectList(_context.Feature, "Id", "Description");
            
            Task<Plan> plantask = getPlan(planId);
            Plan plan = await plantask;
            if (plan != null)
                ViewData["PlanName"] = plan.Name;


            var planFeature = await _context.PlanFeature.FindAsync(id);
            if (planFeature == null)
            {
                return NotFound();
            }

            PlanFeatureViewModel planFeatureViewModel = new PlanFeatureViewModel
            {
                Id = planFeature.Id,
                FeatureId = planFeature.FeatureId,
                PlanId = planFeature.PlanId,
            };
            
            
            
            if (plan != null)
                ViewData["PlanName"] = plan.Name;
                ViewData["FeatureId"] = new SelectList(_context.Feature, "Id", "Description", planFeature.FeatureId);
            ViewData["PlanId"] = new SelectList(_context.Plan, "Id", "Description", planFeature.PlanId);

          



            return View(planFeatureViewModel);
        }

        // POST: Admin/PlanFeatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlanId,FeatureId")] PlanFeatureViewModel planFeatureViewModel)
        {
            if (id != planFeatureViewModel.Id)
            {
                return NotFound();
            }
            PlanFeature planFeature = new PlanFeature
            {
                Id = planFeatureViewModel.Id,
                PlanId = planFeatureViewModel.PlanId,
                FeatureId = planFeatureViewModel.FeatureId
            };

            if (ModelState.IsValid)
            {
                try
                {
                
                    _context.Update(planFeature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanFeatureExists(planFeatureViewModel.Id))
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
            ViewData["FeatureId"] = new SelectList(_context.Feature, "Id", "Description", planFeature.FeatureId);
            ViewData["PlanId"] = new SelectList(_context.Plan, "Id", "Description", planFeature.PlanId);
            return View(planFeature);
        }

        // GET: Admin/PlanFeatures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PlanFeature == null)
            {
                return NotFound();
            }

            var planFeature = await _context.PlanFeature
                .Include(p => p.Feature)
                .Include(p => p.Plan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planFeature == null)
            {
                return NotFound();
            }

            return View(planFeature);
        }

        // POST: Admin/PlanFeatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PlanFeature == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PlanFeature'  is null.");
            }
            var planFeature = await _context.PlanFeature.FindAsync(id);
            if (planFeature != null)
            {
                _context.PlanFeature.Remove(planFeature);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanFeatureExists(int id)
        {
          return (_context.PlanFeature?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private  Task<Plan> getPlan(int planId) {
           return _context.Plan.Where(p => p.Id == planId).FirstAsync();
           
        }
    }
}
