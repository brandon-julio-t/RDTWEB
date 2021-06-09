using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RDTWEB.Models;

namespace RDTWEB
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            await using var context =
                new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            var adminId = await EnsureUser(serviceProvider, testUserPw, "admin@email.com");
            await EnsureRole(serviceProvider, adminId, "Admin");

            var managerId = await EnsureUser(serviceProvider, testUserPw, "participant@email.com");
            await EnsureRole(serviceProvider, managerId, "Participant");

            SeedDb(context, adminId);
        }

        private static void SeedDb(ApplicationDbContext context, string adminId)
        {
        }

        private static async Task<string> EnsureUser(
            IServiceProvider serviceProvider,
            string testUserPw,
            string userName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            if (userManager == null)
            {
                return null;
            }

            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = userName,
                    Email = userName,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null) throw new Exception("The password is probably not strong enough!");

            return user.Id;
        }

        private static async Task EnsureRole(
            IServiceProvider serviceProvider,
            string uid,
            string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            if (roleManager == null) throw new Exception("roleManager null");

            if (!await roleManager.RoleExistsAsync(role)) await roleManager.CreateAsync(new IdentityRole(role));

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            if (userManager == null)
            {
                return;
            }

            var user = await userManager.FindByIdAsync(uid);
            if (user == null) throw new Exception("The testUserPw password was probably not strong enough!");

            await userManager.AddToRoleAsync(user, role);
        }
    }
}