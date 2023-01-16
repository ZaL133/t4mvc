
using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(t4mvc.Web.Areas.consulting.consultingHostingStartup))]
namespace t4mvc.Web.Areas.consulting
{
    public class consultingHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {});
        }
    }
}