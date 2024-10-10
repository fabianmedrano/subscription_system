namespace subscription_system.Mapper
{
    using Areas.Admin.Models.ViewModel.PlanFeature;
    using Models;
    using Riok.Mapperly.Abstractions;
    using subscription_system.Areas.Admin.Models.ViewModel.Feature;

    [Mapper]
    public partial class PlanFeatureMapper
    {

       
        public partial PlanFeature Map(AdminFeatureVM source);



       
    }
}
