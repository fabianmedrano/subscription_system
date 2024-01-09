using Microsoft.AspNetCore.Identity;

namespace subscription_system.Data.SeedData
{
    public class RolSeedData
    {

        public static async Task Initialize(IServiceProvider serviceProvider) {
            var userRoleManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "PlanCreator", "SubscriptionManager", "UserManager", "PaymentManager", "CustomerSupport", "DataAnalyst", "SystemAdmin" };

        }
    }
}
