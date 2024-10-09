using Riok.Mapperly.Abstractions;
using subscription_system.Areas.Admin.Models.ViewModel.Plan;
using subscription_system.Models;

namespace subscription_system.Mapper
{
    [Mapper]
    public partial class PlanMapper
    {
        public partial PlanViewModel map(Plan source);

        public partial Plan map(PlanViewModel source);

        public partial List<PlanViewModel> mapList(List<Plan> source);

        public partial List<Plan> mapList(List<PlanViewModel> source);


    }
}
