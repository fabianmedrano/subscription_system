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
using subscription_system.Services;
using subscription_system.Mapper;

namespace subscription_system.Areas.Admin.Controllers {
    [Area("Admin")]
    public class PlansController : BaseController {
        private readonly IPlanService _planService;
        private readonly ILogger _logger;
        public PlansController(IPlanService planService, ILogger<PlansController> logger) {
            _planService = planService;
            _logger = logger;
        }

        // GET: Admin/AdminPlanCreateViewModels
        public async Task<IActionResult> Index() {
            try {
                List<Plan> plans = await _planService.getPlanListAsync();

                PlanMapper mapper = new PlanMapper();

                var plansModel = mapper.mapList(plans);

                return View(plansModel);
            } catch (Exception ex) {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }

        }




        // GET: Admin/AdminPlanCreateViewModels/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Admin/AdminPlanCreateViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Name,Description,Price,Active,BillingPeriod,TrialPeriod")] 
            PlanViewModel adminPlanCreateViewModel) {
            try {
                if (ModelState.IsValid) {

                    PlanMapper mapper = new PlanMapper();

                    Plan plan = mapper.map(adminPlanCreateViewModel);

                    var inserted = await _planService.AddPlanAsync(plan);
                    return RedirectToAction(nameof(Index), "PlanFeatures", new { area = "Admin", planId = plan.Id });
                }
                return View(adminPlanCreateViewModel);

            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }

        }

        //TODO: no he probado estas funciones
        // GET: Admin/AdminPlanCreateViewModels/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            try {
                if (id == null) return NotFound();

                Plan plan = await _planService.FindPlanAsync((int)id);

                if (plan == null) return NotFound();

                PlanMapper mapper = new PlanMapper();

                PlanViewModel planVM = mapper.map(plan);

                return View(planVM);
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }

        }




        // POST: Admin/AdminPlanCreateViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,Active,BillingPeriod,TrialPeriod")] PlanViewModel adminPlanCreateViewModel) {
            if (id != adminPlanCreateViewModel.Id) return NotFound();

            if (ModelState.IsValid) {
                try {

                    PlanMapper mapper = new PlanMapper();
                    Plan plan = mapper.map(adminPlanCreateViewModel);

                    var updated = await _planService.UpdatePlanAsync(plan);
                    // Rastreo de campos distintos
                    // List<string> changes=changeTracker(plan);

                    Alert(Enums.NotificationType.Success, "Plan actualizado con exito");
                } catch (DbUpdateConcurrencyException ex) {
                    _logger.LogError(ex, ex.Message);
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(adminPlanCreateViewModel);
        }

        // GET: Admin/AdminPlanCreateViewModels/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            /*   if (_context.AdminPlanCreateViewModel == null)
               {
                   return Problem("Entity set 'ApplicationDbContext.AdminPlanCreateViewModel'  is null.");
               }
            */

            try {
                if (id == null) return NotFound();

                Plan plan = await _planService.FindPlanAsync((int)id);

                if (plan == null) return NotFound();

                bool exist = _planService.CheckSubscriptionsExist((int)id);

                if (exist) Alert(Enums.NotificationType.Error, "No es posible eliminar el plan, debido aque existen subscripciones activas con este", "Accion no disponible");

                await _planService.RemovePlanAsync(plan);


                return Json(new { success = true, message = "El plan ha sido eliminado correctamente", title = "Plan eliminado", messagType = "success" });
            } catch (Exception ex) {
                return Json(new { success = false, message = ex.Message, title = "Planes  se encuentra en uso por subscriptores", messageType = "info" });
            }
        }


        // GET: Admin/AdminPlanCreateViewModels/Details/5
        public async Task<IActionResult> Details(int? id) {
            try {
                if (id == null) return NotFound();

                PlanMapper mapper = new PlanMapper();
                Plan plan = await _planService.FindPlanAsync((int)id);

                if (plan == null) return NotFound();

                PlanViewModel adminPlanCreateViewModel = mapper.map(plan);
                return View(adminPlanCreateViewModel);
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }

        }



        // POST: Admin/AdminPlanCreateViewModels/Delete/5

        /*  private bool AdminPlanCreateViewModelExists(int id)
          {
              return (_context.Plan?.Any(e => e.Id == id)).GetValueOrDefault();
          }
        */
        /*           private List<string> changeTracker<T>(T model) where T : class
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
        */

    }


}
