using Riok.Mapperly.Abstractions;
using subscription_system.Areas.Admin.Models.ViewModel.Plan;
using subscription_system.Models;

namespace subscription_system.Mapper
{
    [Mapper]
    public partial class PlanMapper
    {
        public partial PlanViewModel map(Plan plan);
    }
}
