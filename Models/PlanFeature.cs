using System.ComponentModel.DataAnnotations.Schema;

namespace subscription_system.Models
{
    public class PlanFeature
    {
        public int Id { get; set; }

        [ForeignKey("Plan")]
        public int PlanId { get; set; }
        public Plan? Plan { get; set; }

        [ForeignKey("Feature")]
        public int FeatureId { get; set; }
        public Feature? Feature { get; set; }



    }
}
