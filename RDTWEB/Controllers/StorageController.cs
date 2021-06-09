using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace RDTWEB.Controllers
{
    public class StorageController : Controller
    {
        private IWebHostEnvironment Env { get; }

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

            var bytes = System.IO.File.ReadAllBytes(absolutePath);

            return File(bytes, "application/force-download", filename);
        }
    }
}