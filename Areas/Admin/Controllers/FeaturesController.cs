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
using subscription_system.Services;

namespace subscription_system.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = "SystemAdmin")]
   
    public class FeaturesController : Controller
    {
        private readonly IFeatureService _featureService;

        public FeaturesController(FeatureService  featureService)
        {
            _featureService = featureService;
        }

        // GET: Admin/Features
        public async Task<IActionResult> Index()
        {
            List<Feature> features = await _featureService.GetFeatures();
           return View(features);
        }

        // GET: Admin/Features/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var feature = await _featureService.GetFeature(id);

            return View(feature);
        }

        // GET: Admin/Features/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Features/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Feature feature)
        {
            if (ModelState.IsValid)
            {
                _featureService.AddFeature(feature);
                return RedirectToAction(nameof(Index));
            }
            return View(feature);
        }

        // GET: Admin/Features/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
                return NotFound();
            

            Feature feature = await _featureService.GetFeature(id);
            if (feature == null)
                return NotFound();

            return View(feature);
        }

        // POST: Admin/Features/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Feature feature)
        {
            if (id != feature.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _featureService.Update(feature);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_featureService.FeatureExists(feature.Id))
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
            return View(feature);
        }

        // GET: Admin/Features/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (_featureService.ContextIsNull())
            {
                return Problem("Entity set 'ApplicationDbContext.Feature'  is null.");
            }
            var feature=  await _featureService.GetFeature(id);
            if (feature != null)
            {
                _featureService.Remove(feature);
            }

            await _featureService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       
    }
}
