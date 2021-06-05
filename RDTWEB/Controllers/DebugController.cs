using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RDTWEB.Data;

namespace RDTWEB.API
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DebugController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public DebugController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public string OnGet()
        {
            return string.Join("\n", context.AspNetUserRoles);
        }
    }
}
