using Microsoft.Extensions.Hosting;
using subscription_system.Areas.Admin.Models.ViewModel.Feature;
using subscription_system.Areas.Admin.Models.ViewModel.Plan;
using subscription_system.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace subscription_system.Areas.Admin.Models.ViewModel.PlanFeature
{
    public class PlanFeaturesViewModel
    {
    
        public PlanViewModel Plan { set; get; } = new PlanViewModel();
        public PlanFeatureViewModel NewFeature { set; get; } = new PlanFeatureViewModel();
        public ICollection<FeatureViewModel> Features { get; set; } = new List<FeatureViewModel>();
    }
}
