using Microsoft.AspNetCore.Hosting;
using RDTWEB.Areas.Identity;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]

namespace RDTWEB.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}