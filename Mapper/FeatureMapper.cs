using Riok.Mapperly.Abstractions;
using subscription_system.Areas.Admin.Models.ViewModel.Feature;
using subscription_system.Models;

namespace subscription_system.Mapper {
    [Mapper]
    public partial class FeatureMapper {
        public partial Feature Map(AdminFeatureVM source);

        public partial AdminFeatureVM  Map(Feature source);
        public partial List<AdminFeatureVM> MapList(List<Feature> source);

        public partial List<Feature> MapList(List<AdminFeatureVM> source);
    }
}
