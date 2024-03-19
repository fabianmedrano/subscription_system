using subscription_system.Areas.Admin.Models.ViewModel.Feature;
using subscription_system.Areas.Admin.Models.ViewModel.PlanFeature;
using subscription_system.Models;

namespace subscription_system.Services
{
    public interface IFeatureService
    {
        Task<PlanFeaturesViewModel> GetFeaturesViewModel(int planId, string sortOrder, string searchString, int pageNumber, int pageSize);
        Task<Plan> getPlan(int planId);
    }
}
