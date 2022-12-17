
using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(t4mvc.Web.Areas.crm.crmHostingStartup))]
namespace t4mvc.Web.Areas.crm
{
    public class crmHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {});
        }
    }
}