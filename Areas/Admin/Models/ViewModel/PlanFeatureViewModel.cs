using subscription_system.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace subscription_system.Areas.Admin.Models.ViewModel
{
    public class PlanFeatureViewModel
    {
        public int Id { get; set; }

        [ForeignKey("Feature"),Required(ErrorMessage ="Debe de seleccionar una característica")]
            public int FeatureId { get; set; }

        [ForeignKey("Plan"),Required(ErrorMessage ="Debe de existir un plan seleccionado")]
        public int PlanId { get; set; }

    

    }
}
