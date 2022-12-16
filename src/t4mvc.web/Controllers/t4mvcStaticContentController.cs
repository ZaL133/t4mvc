using Microsoft.AspNetCore.Mvc;
using t4mvc.web.core.Infrastructure;

namespace t4mvc.web.Controllers
{
    [Route("api")]
    public class t4mvcStaticContentController : Controller
    {
        
        [Route("js/{id?}")]
        [HttpGet]
        public FileResult Js(string id)
        {
            var safeId              = Path.GetFileName(id);
            var jsFileVirtualPath   = $"wwwroot/js/views/{safeId}.js";
            var jsPhysicalFile      = Current.MapPath(jsFileVirtualPath);
            if (jsPhysicalFile.Exists)
            {
                return File(jsFileVirtualPath, "text/javascript");
            }
            else
            {
                return File("/js/views/empty.js", "text/javascript");
            }
        }

        [Route("css/{id?}")]
        [HttpGet]
        public FileResult Css(string id)
        {
            var safeId              = Path.GetFileName(id);
            var cssFileVirtualPath  = $"wwwroot/css/views/{safeId}.css";
            var cssPhysicalFile     = Current.MapPath(cssFileVirtualPath);
            if (cssPhysicalFile.Exists)
            {
                return File(cssFileVirtualPath, "text/css");
            }
            else
            {
                return File("/css/views/empty.css", "text/css");
            }
        }
    }
}
