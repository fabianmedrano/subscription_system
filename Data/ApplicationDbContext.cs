using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using subscription_system.Models;
using subscription_system.Areas.Admin.Models.ViewModel.Plan;
using subscription_system.Areas.Admin.Models.ViewModel.PlanHistory;

namespace subscription_system.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
      
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Feature> Feature { get; set; }
        public DbSet<Plan> Plan { get; set; }
        public DbSet<PlanFeature> PlanFeature { get; set; }
        public DbSet<PlanHistory> PlanHistory { get; set; }
        public DbSet<PriceHistory> PriceHistory { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionDiscount> SubscriptionDiscounts { get; set; }
        public DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AdminPlanCreateVM> AdminPlanCreateViewModel { get; set; } = default!;
        public DbSet<AdminPlanHistoryVM> AdminPlanHistoryViewModel { get; set; } = default!;
    }
}