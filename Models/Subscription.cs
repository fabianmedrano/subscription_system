using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace subscription_system.Models
{
    public class Subscription
    {
        public int Id { get; set; }

        /**/
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public User? User { get; set; }


        [ForeignKey("Plan")]
        public int PlanId { get; set; }
        public Plan? Plan { get; set; }

        [ForeignKey("SubscriptionDiscount")]
        public int? DiscountId { get; set; } 
        public SubscriptionDiscount? Discount { get; set; }
        /**/

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } = DateTime.Now;
        public bool IsTrial { get; set; }
        public bool IsActive { get; set; }
        public bool Renewal { get; set; }
     
    }
}
