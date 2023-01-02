using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using t4mvc.web.core.ViewModelServices;
using t4mvc.web.Models;

namespace t4mvc.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISearchViewModelService searchViewModelService;

        public HomeController(ILogger<HomeController> logger, ISearchViewModelService searchViewModelService)
        {
            _logger = logger;
            this.searchViewModelService = searchViewModelService;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Home page it");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult OpenSource()
        {
            return View();
        }

        public IActionResult Search(string searchTerm)
        {
            var results = searchViewModelService.SearchAll(searchTerm);

            return View(results);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}