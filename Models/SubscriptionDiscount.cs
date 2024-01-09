using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace subscription_system.Models
{
    [Index(nameof(Code),IsUnique = true)]
    public class SubscriptionDiscount
    {
        public int Id { get; set; }

        [Required, Column(TypeName ="char(8)")]
        public string Code { get; set; } = "";
        
        [Required]
        public string Description { get; set; } = "";

        [Required, DefaultValue(0)]
        public float Amount { get; set; } = 0;

        [Required]
        public DateTime ExpirationDate { get; set; }
    }
}
