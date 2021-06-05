using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace RDTWEB.API
{
    [Route("[controller]")]
    [ApiController]
    public class SeedingController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public SeedingController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<string> OnGetAsync()
        {
            var user = new IdentityUser { UserName = "admin@admin.com", Email = "admin@admin.com", EmailConfirmed = true };
            var result = await userManager.CreateAsync(user, "System@dm1n");
            if (result.Succeeded)
            {
                return "seeding success";
            }
            else
            {
                return string.Join("\n", result.Errors.Select(e => e.Description));
            }
        }
    }
}
