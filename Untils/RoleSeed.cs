using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BookStore.Untils
{
    public class RoleSeed
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Staff", "Customer" };

            foreach (var roleName in roleNames)
            {
                bool roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            await EnsureUserAsync(userManager, "staff@example.com", "Staff");
            await EnsureUserAsync(userManager, "customer@example.com", "Customer");
        }

        private static async Task EnsureUserAsync(UserManager<ApplicationUser> userManager, string email, string roleName)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    Role = roleName
                    // Additional properties
                };

                var result = await userManager.CreateAsync(user, "Password@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
            else
            {
                var isInRole = await userManager.IsInRoleAsync(user, roleName);
                if (!isInRole)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }
}
