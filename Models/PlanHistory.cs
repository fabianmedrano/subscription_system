using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace subscription_system.Models
{
    public class PlanHistory
    {
        public int Id { get; set; }

        /**/
        [ForeignKey("Plan")]
        public int PlanId { get; set; }
        public Plan? Plan { get; set; }
        /**/

        [Required]
        public DateTime ChangeDate { get; set; } = DateTime.Now;
        
        [Required]
        public string OldDescription { get; set; } = "";
        
        [Required]
        public string NewDescription { get; set; } = "";
    }
}
