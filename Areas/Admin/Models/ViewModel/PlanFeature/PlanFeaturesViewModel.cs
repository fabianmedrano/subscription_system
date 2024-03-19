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
    public class PlanFeaturesViewModel
    {
    
        public PlanViewModel Plan { set; get; } = new PlanViewModel();
        public PlanFeatureViewModel NewFeature { set; get; } = new PlanFeatureViewModel();
        public PaginatedList<FeatureViewModel> Features { get; set; }
        public string CurrentSort;
        public string CurrentFilter;
        public SelectList FeatureIdSelectList;
    }
}
