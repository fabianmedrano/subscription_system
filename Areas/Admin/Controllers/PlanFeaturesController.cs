using System;
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
using subscription_system.Areas.Admin.Models.ViewModel.Plan;
using Microsoft.AspNetCore.Http.HttpResults;
using subscription_system.Views.Shared.Paginate;
using subscription_system.Services;
namespace subscription_system.Areas.Admin.Controllers {

    [Authorize(Roles = "SystemAdmin")]
    [Area("Admin")]
    [Route("Admin/Plan/{planId}/PlanFeatures/{action}/{id?}")]
    public class PlanFeaturesController : BaseController {
        //  private readonly ApplicationDbContext _context;
        private readonly ILogger<PlanFeaturesController> _logger;

        private readonly IPlanFeatureService _featureService;
        public PlanFeaturesController(PlanFeatureService featureService, ILogger<PlanFeaturesController> logger) {
            _featureService = featureService;
            _logger = logger;
        }

        // GET: Admin/PlanFeatures 

        // TODO: aqui estube tratando de aplicar paginacion????
        public async Task<IActionResult> Index(int planId, string sortOrder, string currentFilter, string searchString, int? pageNumber) {
            int pageSize = 5;
            var viewModel = await _featureService.GetFeaturesViewModel(planId, sortOrder, searchString, pageNumber ?? 1, pageSize);
            return View(viewModel);
        }

        // GET: Admin/PlanFeatures/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (_featureService.PlanFeatureContextExist(id)) return NotFound();

            var planFeature = await _featureService.GetPlanFeature(id ?? 0);
            if (planFeature == null) return NotFound();


            return View(planFeature);
        }


        // POST: Admin/PlanFeatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int planId,/* [Bind("NewFeature.FeatureId")] */AdminPlanFeaturesVM model) {
            try {
                int featureId = model.NewFeature.FeatureId;
                if (!await _featureService.PlanFeatureExists(planId, featureId)) {
                    Alert(Enums.NotificationType.Info, "Esta caracteristica ya esta asociada con este plan");
                    RedirectToAction(nameof(Index), StatusCodes.Status200OK);
                }
                PlanFeature planFeature = new() {
                    PlanId = planId,
                    FeatureId = model.NewFeature.FeatureId
                };

                var saveit = await _featureService.Add(planFeature);



            } catch (DbUpdateException ex) {
                Alert(Enums.NotificationType.Error, "No se pudo ralizar la acción, por favor intentelo nuevamente", "Error al guardar los datos");
                _logger.LogInformation("DbUpdateException", ex.ToString());
            } catch (Exception ex) {
                Alert(Enums.NotificationType.Error, "No se pudo ralizar la acción, por favor intentelo nuevamente", "Error interno");
                _logger.LogInformation("Exception", ex.ToString());
            }
            Alert(Enums.NotificationType.Success, "registro realizado de forma exitosa", "registro exitoso");
            return RedirectToAction(nameof(Index), StatusCodes.Status422UnprocessableEntity);
        }

        // GET: Admin/PlanFeatures/Edit/5
        public async Task<IActionResult> Edit(int planId, int id) {
            if (_featureService.PlanFeatureContextExist(id)) return NotFound();

            ViewData["FeatureId"] = new SelectList(_featureService.GetFeatureContext(), "Id", "Description");

            Task<Plan> plantask = _featureService.GetPlan(planId);
            Plan plan = await plantask;
            if (plan != null)
                ViewData["PlanName"] = plan.Name;


            var planFeature = await _featureService.GetPlanFeature(id);
            if (planFeature == null) {
                return NotFound();
            }

            AdminPlanFeatureVM planFeatureViewModel = new() {
                Id = planFeature.Id,
                FeatureId = planFeature.FeatureId,
                PlanId = planFeature.PlanId,
            };



            if (plan != null)
                ViewData["PlanName"] = plan.Name;
            ViewData["FeatureId"] = new SelectList(_featureService.GetFeatureContext(), "Id", "Description", planFeature.FeatureId);
            ViewData["PlanId"] = new SelectList(_featureService.GetPlanContext(), "Id", "Description", planFeature.PlanId);





            return View(planFeatureViewModel);
        }

        // POST: Admin/PlanFeatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlanId,FeatureId")] AdminPlanFeatureVM planFeatureViewModel) {
            if (id != planFeatureViewModel.Id) {
                return NotFound();
            }
            PlanFeature planFeature = new() {
                Id = planFeatureViewModel.Id,
                PlanId = planFeatureViewModel.PlanId,
                FeatureId = planFeatureViewModel.FeatureId
            };

            if (ModelState.IsValid) {
                try {
                    await _featureService.UpdatePlanFeature(planFeature);

                } catch (DbUpdateConcurrencyException) {
                    if (!_featureService.PlanFeatureExists(planFeatureViewModel.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FeatureId"] = new SelectList(_featureService.GetFeatureContext(), "Id", "Description", planFeature.FeatureId);
            ViewData["PlanId"] = new SelectList(_featureService.GetPlanContext(), "Id", "Description", planFeature.PlanId);
            return View(planFeatureViewModel);
        }

        // GET: Admin/PlanFeatures/Delete/5
        public async Task<IActionResult> Delete(int id) {

            PlanFeature planfeture = await _featureService.GetPlanFeature(id);
            bool deleted = await _featureService.RemovePlanFeature(planfeture);
            if (deleted) { 
                Alert(Enums.NotificationType.Success, "caracteristica eliminada"); 
                } else {
                Alert(Enums.NotificationType.Error, "No fue Posible eliminar la caracteristica");
                }
            return RedirectToAction(nameof(Index));
        }
    }
}
