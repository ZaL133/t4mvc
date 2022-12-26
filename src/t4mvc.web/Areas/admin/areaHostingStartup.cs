
using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(t4mvc.web.Areas.admin.crmHostingStartup))]
namespace t4mvc.web.Areas.admin
{
    public class crmHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {});
        }
    }
}