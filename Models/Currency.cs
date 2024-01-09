using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace subscription_system.Models
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }

        [StringLength(3)]
        public string Code { set; get; } = "";

        [StringLength(50)]
        public string Name { set; get; } = "";

        [Column(TypeName = "decimal(10,2)")]
        public float ExchangeRate { set; get; } = 0;
    }
}
