using Microsoft.EntityFrameworkCore;
using subscription_system.Areas.Admin.Models.ViewModel.Feature;
using subscription_system.Areas.Admin.Models.ViewModel.PlanFeature;
using subscription_system.Models;
using System.Data;

namespace subscription_system.Services
{
    public interface IPlanFeatureService
    {
        Task<PlanFeaturesViewModel> GetFeaturesViewModel(int planId, string sortOrder, string searchString, int pageNumber, int pageSize);
        Task<Plan> getPlan(int planId);
        Task<int> Add(PlanFeature plan);
        bool ExistPlanFeatureContext(int? id);
        DbSet<PlanFeature> GetPlanFeatureContext();
        DbSet<Plan> GetPlanContext();
        DbSet<Plan> GetFeatureContext();
        Task<PlanFeature> getPlanFeature(int id);
    }
}
