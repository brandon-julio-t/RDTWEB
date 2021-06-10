using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace RDTWEB.Controllers
{
    public class StorageController : Controller
    {
        public StorageController(IWebHostEnvironment env)
        {
            Env = env;
        }

        private IWebHostEnvironment Env { get; }

        [Route("Storage/{**path}")]
        public ActionResult Index(string path)
        {
            var absolutePath = Path.Combine(Env.ContentRootPath, "Storage", path);
            var split = path.Split("/");
            var filename = split.Last();
            return File(System.IO.File.ReadAllBytes(absolutePath), "application/force-download", filename);
        }
    }
}