using Microsoft.EntityFrameworkCore;
using subscription_system.Areas.Admin.Models.ViewModel.Feature;
using subscription_system.Areas.Admin.Models.ViewModel.PlanFeature;
using subscription_system.Models;
using System.Data;

namespace subscription_system.Services
{
    public interface IPlanFeatureService
    {
        Task<AdminPlanFeaturesVM> GetFeaturesViewModel(int planId, string sortOrder, string searchString, int pageNumber, int pageSize);
        Task<Plan> GetPlan(int planId);
        Task<bool> Add(PlanFeature plan);
        Task<PlanFeature> GetPlanFeature(int id);
        Task<bool> UpdatePlanFeature(PlanFeature planFeature);
        Task<bool> RemovePlanFeature(PlanFeature planFeature);
        bool PlanFeatureExists(int id);
        Task<bool> PlanFeatureExists(int planId, int featureId);

        /*********************************** START GET CONTEXT ***********************************/
        bool PlanFeatureContextExist(int? id);
        DbSet<PlanFeature> GetPlanFeatureContext();
        DbSet<Plan> GetPlanContext();
        DbSet<Feature> GetFeatureContext();

    }
}
