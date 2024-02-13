using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using subscription_system.Data;
using subscription_system.Extensions;
using subscription_system.Models;

namespace subscription_system.Areas.User.Controllers
{
    [Area("User")]

    [Route("UserSubscriptions/{userId}/{action}/{id?}")]
    public class PlansController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public PlansController(ApplicationDbContext context)
        {
         
            _context = context;

            
        }

        // GET: User/Plans
        public async Task<IActionResult> Index(int userId)
        {
              return _context.Plan != null ? 
                          View(await _context.Plan.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Plan'  is null.");
        }

        // GET: User/Plans/Details/5
        public async Task<IActionResult> Details(int userId,int? id)
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
       
        // GET: User/Plans/Create
      /*  public IActionResult Subscribe(int userId, int planId)
        {

            System.Console.WriteLine($"holis putitos:{userId}, {planId}");
            return View();
        }
      */
      


        // GET: Admin/Features/Delete/5
        public async Task<IActionResult> Subscribe(int userId, int planId)
        {
            Alert("Tu subscripción se a realizado con exito", Enums.NotificationType.Success, "Existo");

          /*  if (planId == null || _context.Feature == null)
            {
                return NotFound();
            }

            var feature = await _context.Subscriptions
                .FirstOrDefaultAsync(m => m.Id == planId);
            if (feature == null)
            {
                return NotFound();
            }
          */
            return View(/*feature*/);
        }

        // POST: Admin/Features/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Feature == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Feature'  is null.");
            }
            var feature = await _context.Feature.FindAsync(id);
            if (feature != null)
            {
                _context.Feature.Remove(feature);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool PlanExists(int id)
        {
          return (_context.Plan?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
