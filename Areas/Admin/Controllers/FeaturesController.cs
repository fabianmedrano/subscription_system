using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using subscription_system.Extensions;
using subscription_system.Models;
using subscription_system.Services;

namespace subscription_system.Areas.Admin.Controllers {
    [Area("Admin")]

    [Authorize(Roles = "SystemAdmin")]

    public class FeaturesController : BaseController {
        private readonly IFeatureService _featureService;

        public FeaturesController(IFeatureService featureService) {
            _featureService = featureService;

        }

        // GET: Admin/Features
        public async Task<IActionResult> Index() {
            try {
                List<Feature> features = await _featureService.GetFeaturesAsync();
                return View(features);
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }

        }

        // GET: Admin/Features/Details/5
        public async Task<IActionResult> Details(int id) {
            try {
                var feature = await _featureService.GetFeatureAsync(id);
                return View(feature);
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: Admin/Features/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Admin/Features/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Feature feature) {
            if (ModelState.IsValid) {
                await _featureService.AddFeatureAsync(feature);
                return RedirectToAction(nameof(Index));
            }
            return View(feature);
        }

        // GET: Admin/Features/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null)
                return NotFound();


            Feature feature = await _featureService.GetFeatureAsync(id.Value);
            if (feature == null)
                return NotFound();

            return View(feature);
        }

        // POST: Admin/Features/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Feature feature) {
            if (id != feature.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    await _featureService.UpdateFeatureAsync(feature);
                } catch (DbUpdateConcurrencyException) {
                    if (!_featureService.FeatureExists(feature.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(feature);
        }

        // GET: Admin/Features/Delete/5
        public async Task<IActionResult> Delete(int id) {

            var feature = await _featureService.GetFeatureAsync(id);
            if (feature != null) {
                await _featureService.RemoveFeatureAsync(feature);
            }

            await _featureService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
