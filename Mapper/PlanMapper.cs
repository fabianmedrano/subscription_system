using Riok.Mapperly.Abstractions;
using subscription_system.Areas.Admin.Models.ViewModel.Plan;
using subscription_system.Models;

namespace subscription_system.Mapper
{
    [Mapper]
    public partial class PlanMapper
    {
        public partial AdminPlanCreateVM Map(Plan source);

        public partial Plan Map(AdminPlanCreateVM source);

        public partial List<AdminPlanCreateVM> MapList(List<Plan> source);

        public partial List<Plan> MapList(List<AdminPlanCreateVM> source);


    }
}
