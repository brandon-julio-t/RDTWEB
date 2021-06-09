using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RDTWEB.Models;

namespace RDTWEB
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            await using var context =
                new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            var transaction = await context.Database.BeginTransactionAsync();
            await context.Database.ExecuteSqlRawAsync("delete from Answers");
            await context.Database.ExecuteSqlRawAsync("delete from Questions; DBCC CHECKIDENT (Questions, RESEED, 0)");
            await context.Database.ExecuteSqlRawAsync("delete from QuestionSets; DBCC CHECKIDENT (QuestionSets, RESEED, 0)");
            await context.Database.ExecuteSqlRawAsync("delete from AspNetUsers");
            await transaction.CommitAsync();
            
            var adminId = await EnsureUser(serviceProvider, testUserPw, "admin@email.com");
            await EnsureRole(serviceProvider, adminId, "Admin");
            
            await EnsureUserRole(serviceProvider, testUserPw, "bran@email.com", "Participant");
            await EnsureUserRole(serviceProvider, testUserPw, "onel@email.com", "Participant");
            await EnsureUserRole(serviceProvider, testUserPw, "cleo@email.com", "Participant");
            await EnsureUserRole(serviceProvider, testUserPw, "stan@email.com", "Participant");
            await EnsureUserRole(serviceProvider, testUserPw, "gaby@email.com", "Participant");
            await EnsureUserRole(serviceProvider, testUserPw, "clar@email.com", "Participant");
            await EnsureUserRole(serviceProvider, testUserPw, "cent@email.com", "Participant");
            await EnsureUserRole(serviceProvider, testUserPw, "joph@email.com", "Participant");
            
            SeedDb(context, adminId);
        }

        private static void SeedDb(DbContext context, string adminId)
        {
            Enumerable.Range(1, 10)
                .Select(i => new QuestionSet
                {
                    Title = $"Question Set #{i}",
                    Questions = new List<Question>
                    {
                        new()
                        {
                            Body = "The correct answer is 1",
                            Type = "Multiple Choice",
                            Choices = new List<string> {"One", "1", "one", "I"},
                            CorrectChoiceIndex = 1
                        },
                        new()
                        {
                            Body = "The correct answer is True",
                            Type = "Boolean (true or false)",
                            BooleanAnswer = true
                        },
                        new()
                        {
                            Body = "The fifth answer is correct",
                            Type = "Multiple Answer List",
                            Choices = new List<string> {"Correct", "fifth answer", "Fifth Answer", "wrong", "wrong answer"},
                            CorrectChoiceIndex = 4
                        },
                        new() {Body = "In 250 words, please tell us about yourself", Type = "Essay"},
                        new() {Body = "Please upload your resume", Type = "Submit File"}
                    }
                })
                .ToList()
                .ForEach(questionSet => context.AddRange(questionSet));

            context.SaveChanges();
        }

        private static async Task EnsureUserRole(
            IServiceProvider serviceProvider,
            string testUserPw,
            string userName,
            string role)
        {
            var userId = await EnsureUser(serviceProvider, testUserPw, userName);
            await EnsureRole(serviceProvider, userId, role);
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
            if (userManager == null) return;

            var user = await userManager.FindByIdAsync(uid);
            if (user == null) throw new Exception("The testUserPw password was probably not strong enough!");
            await userManager.AddToRoleAsync(user, role);
        }
    }
}