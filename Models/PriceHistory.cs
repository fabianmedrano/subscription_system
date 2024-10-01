using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace subscription_system.Models
{
    public class PriceHistory
    {
        public int Id { get; set; }

        /**/
        [ForeignKey("Plan")]
        public int PlanId { get; set; }
        public Plan? Plan { get; set; }
        /**/

        [Required]
        public float OldPrice {get;set;}

        [Required]
        public float NewPrice { get;set;}

        [Required]
        public DateTime ChangeDate { get; set;} = DateTime.Now;

     

    }
}
