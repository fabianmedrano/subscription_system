namespace subscription_system.Mapper
{
    using Areas.Admin.Models.ViewModel.PlanFeature;
    using Models;
    using Riok.Mapperly.Abstractions;
    using subscription_system.Areas.Admin.Models.ViewModel.Feature;

    [Mapper]
    public partial class PlanFeatureMapper
    {

        public partial FeatureViewModel map(Feature source);
        public partial PlanFeature map(FeatureViewModel source);

        public partial List<PlanFeature> mapList(List<FeatureViewModel> source);

        public partial List<FeatureViewModel> mapList(List<Feature> source);
    }
}
