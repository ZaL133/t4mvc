using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace t4mvc.web.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles ="admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Areas/admin/Views/Home/Index.cshtml");
        }
    }
}
