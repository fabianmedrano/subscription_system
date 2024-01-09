using Humanizer.Localisation.TimeToClockNotation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace subscription_system.Models
{
    public class Plan
    {
        public int Id { set; get; }

        [Required, Column(TypeName = ("varchar(150)"))]
        public string Name { set; get; } = "";

        [Required]
        public string Description { set; get; } = "";

        [Required, DefaultValue(0)]
        public float Price { set; get; }

        [Required, DefaultValue(true)]
        public bool Active { set; get; }

        [Required, DefaultValue(1)]
        public int BillingPeriod { set; get; }

        [Required, DefaultValue(7)]
        public int TrialPeriod { set; get; }

    }
}
