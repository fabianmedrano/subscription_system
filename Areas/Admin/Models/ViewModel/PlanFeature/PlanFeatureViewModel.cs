using subscription_system.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using subscription_system.Areas.Admin.Models.ViewModel.Plan;
using subscription_system.Areas.Admin.Models.ViewModel.Feature;

namespace subscription_system.Areas.Admin.Models.ViewModel.PlanFeature
{
    public class PlanFeatureViewModel
    {
        public int Id { get; set; }

        [ForeignKey("Feature"), Required(ErrorMessage = "Debe de seleccionar una característica"),Display(Name ="Funcionalidad")]
        public int FeatureId { get; set; }

        [ForeignKey("Plan"), Required(ErrorMessage = "Debe de existir un plan seleccionado"),Display(Name ="Plan")]
        public int PlanId { get; set; }
        public PlanViewModel Plan { get; set; }

        public FeatureViewModel Feature { get; set; }

    }



}
