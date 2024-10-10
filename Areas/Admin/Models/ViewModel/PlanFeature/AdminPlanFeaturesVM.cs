using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using subscription_system.Areas.Admin.Models.ViewModel.Feature;
using subscription_system.Areas.Admin.Models.ViewModel.Plan;
using subscription_system.Models;
using subscription_system.Views.Shared.Paginate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace subscription_system.Areas.Admin.Models.ViewModel.PlanFeature
{
    public class AdminPlanFeaturesVM
    {
    
        public AdminPlanCreateVM Plan { set; get; } = new AdminPlanCreateVM();
        public AdminPlanFeatureVM NewFeature { set; get; } = new AdminPlanFeatureVM();
        public PaginatedList<AdminFeatureVM>? Features { get; set; }
        public string? CurrentSort;
        public string? CurrentFilter;
        public SelectList? FeatureIdSelectList;
    }
}
