using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Restaurant.Web.Areas.Identity.IdentityHostingStartup))]
namespace Restaurant.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}