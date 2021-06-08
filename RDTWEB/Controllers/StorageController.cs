using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RDTWEB.Controllers
{
    public class StorageController : Controller
    {
        public IWebHostEnvironment Env { get; }
        
        public StorageController(IWebHostEnvironment env)
        {
            Env = env;
        }

        [Route("Storage/{**path}")]
        public ActionResult Index(string path)
        {
            var absolutePath = Path.Combine(Env.ContentRootPath, "Storage", path);

            var split = path.Split("/");
            var filename = split.Last();

            byte[] bytes = System.IO.File.ReadAllBytes(absolutePath);

            return File(bytes, "application/force-download", filename);
        }
    }
}
