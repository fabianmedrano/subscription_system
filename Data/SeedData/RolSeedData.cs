using Microsoft.AspNetCore.Identity;
using subscription_system.Models;

namespace subscription_system.Data.SeedData
{
    public class RolSeedData
    {

        public static async Task Initialize(IServiceProvider serviceProvider) {
            var userRoleManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();



            /* 
             *
             *DEFINICION DE ROLES 
             * 
            */

            IdentityResult roleResult;
            string[] roleNames = { "PlanCreator", "SubscriptionManager", "UserManager", "PaymentManager", "CustomerSupport", "DataAnalyst", "SystemAdmin" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);

                if (!roleExist) {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            /* 
             *
             *DEFINICION DE ROLES 
             *
            */
            var user = new User
            {
                UserName = "admin",
                Email = "admin@test.com",
            };

            string userPassword = "Password123!";
            var userExist = await userRoleManager.FindByNameAsync(user.UserName);

            if (userExist == null) { 
             var createPaworUser = await userRoleManager.CreateAsync(user, userPassword);
                if (createPaworUser.Succeeded) await userRoleManager.AddToRoleAsync(user, "SystemAdmin");
            }


        }
    }
}
