using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RDTWEB.API
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DebugController : ControllerBase
    {
        public DebugController()
        {
        }

        public string OnGet()
        {
            return "debug";
        }
    }
}
