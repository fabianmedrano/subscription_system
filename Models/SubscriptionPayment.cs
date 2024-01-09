using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace subscription_system.Models
{
    public class SubscriptionPayment
    {
        public SubscriptionPayment()
        {
            Currency = new Currency();
            Subscription = new Subscription();
        }

        public int Id { get; set; }


        /**/
        [ForeignKey("Subscription")]
        public int SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }

        [ForeignKey("Currency")]
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        /**/


        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [Required,DefaultValue(0)]
        public float AmountPay { get; set; } = 0;
        

    }
}
