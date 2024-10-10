using subscription_system.Areas.Admin.Models.ViewModel.Plan;
using subscription_system.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace subscription_system.Areas.Admin.Models.ViewModel.PlanHistory
{
    public class AdminPlanHistoryVM
    {

        public int Id { get; set; }
        [ForeignKey("Plan")]
        public int PlanId { get; set; }
        public AdminPlanCreateVM? Plan { get; set; }
        /**/

        [Display(Name = "Fecha de modificación"), Editable(false)]
        public DateTime ChangeDate { get; set; }

        [Display(Name = "Descripción antigua"), Editable(false)]
        public string OldDescription { get; set; } = "";

        [Display(Name = "Nueva Descripción"), Required(ErrorMessage = "Debes de ingrear la nueva descripción")]
        public string NewDescription { get; set; } = "";
    }
}
