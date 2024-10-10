using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using subscription_system.Areas.Admin.Models.ViewModel.Feature;
using subscription_system.Areas.Admin.Models.ViewModel.Plan;
using subscription_system.Areas.Admin.Models.ViewModel.PlanFeature;
using subscription_system.Data;
using subscription_system.Mapper;
using subscription_system.Models;
using subscription_system.Views.Shared.Paginate;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

namespace subscription_system.Services
{
    public class PlanFeatureService : IPlanFeatureService
    {
        private readonly ApplicationDbContext _context;
        
        public PlanFeatureService(ApplicationDbContext context) =>    _context = context;
        
        public async Task<AdminPlanFeaturesVM> GetFeaturesViewModel(int planId, string sortOrder, string searchString, int pageNumber, int pageSize)
        {
            PlanMapper planMapper = new ();

            // Lógica para obtener los datos del plan
            var plan = await GetPlan(planId);
            AdminPlanCreateVM planVM = planMapper.map(plan);

            // Lógica para obtener las características del plan
            var featuresQuery = GetFeaturesQuery(planId, searchString, sortOrder);

            // Lógica para la paginación
            var features = await PaginateFeatures(featuresQuery, pageNumber, pageSize);
            var feate = _context.Feature.ToList();
            return new AdminPlanFeaturesVM
            {
                Plan = planVM,
                Features = features,
                CurrentSort = sortOrder,
                CurrentFilter = searchString,
                FeatureIdSelectList = new SelectList(feate, "Id", "Description")
            };
        }
        
        public Task<Plan> GetPlan(int planId) => _context.Plan.Where(p => p.Id == planId).FirstAsync();






        public async Task<int> Add(PlanFeature plan) {
            _context.PlanFeature.Add(plan);
            return await _context.SaveChangesAsync();
        }

        public async Task<PlanFeature> GetPlanFeature(int id) { 
            return await _context.PlanFeature.FirstAsync(p => p.Id == id);
        }

        public async Task<bool> UpdatePlanFeature(PlanFeature planFeature)
        {
            _context.Update(planFeature);
            int rowsAffected = await _context.SaveChangesAsync();
            return (rowsAffected > 0);
        }
        public async Task<bool> Delete(PlanFeature planFeature)
        {
      
            if (planFeature != null)
            {
                _context.PlanFeature.Remove(planFeature);
            }

            int rowsAffected= await _context.SaveChangesAsync();
            return (rowsAffected > 0);
        }

        public bool PlanFeatureExists(int id)
        {
            return (_context.PlanFeature?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        /*********************************** START GET CONTEXT ***********************************/
        public async Task<PlanFeature> GetFeatureContext(int id)
        {
            return await _context.PlanFeature
           .Include(p => p.Feature)
           .Include(p => p.Plan)
           .FirstOrDefaultAsync(m => m.Id == id) ?? new PlanFeature();
        }
        public bool ExistPlanFeatureContext(int? id) => (id == null || _context.PlanFeature == null);

        public DbSet<PlanFeature> GetPlanFeatureContext() => _context.PlanFeature;
        public DbSet<Plan> GetPlanContext() => _context.Plan;

        public DbSet<Feature> GetFeatureContext() => _context.Feature;

        /*********************************** END GET CONTEXT ***********************************/

        //////////
        ///
        private IQueryable<AdminFeatureVM> GetFeaturesQuery(int planId, string searchString, string sortOrder)
        {
            //NOTE: get plan feature 
            var applicationDbContext = _context.Feature
                .Join(_context.PlanFeature, f => f.Id, pf => pf.FeatureId, (f, pf) => new { pf.Id, f.Description, f.Name, pf.PlanId })
                .Where(p => p.PlanId == planId).Select((f) => new AdminFeatureVM { Id = f.Id, Name = f.Name, Description = f.Description });

            // FIX:Aun mas codigo para revisar!!
            if (!String.IsNullOrEmpty(searchString))
                applicationDbContext = applicationDbContext.Where(f => f.Description.Contains(searchString)
                                       || f.Name.Contains(searchString));
          
            switch (sortOrder)
            {
                case "name_desc":
                    applicationDbContext = applicationDbContext.OrderByDescending(s => s.Name);
                    break;
                default:
                    applicationDbContext = applicationDbContext.OrderBy(s => s.Description);
                    break;
            }
            return applicationDbContext;
        }

        private async Task<PaginatedList<AdminFeatureVM>> PaginateFeatures(IQueryable<AdminFeatureVM> featuresQuery, int pageSize, int pageNumber )
        {
            return  await PaginatedList<AdminFeatureVM>.CreateAsync(featuresQuery.AsNoTracking(), pageNumber, pageSize);
        }

      


       
      
      


    }
}
