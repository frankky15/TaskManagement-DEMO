using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskManagementApp.Interfaces;
using TaskManagementApp.Models;
using TaskManagementApp.Services;

namespace TaskManagementApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITestService _testService;

        public HomeController(ILogger<HomeController> logger, ITestService testService)
        {
            _logger = logger;
            _testService = testService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Instructions()
        {
            return View();
        }

        public IActionResult Test()
        {
            var chores = _testService.GetChores();
            return View(chores);
        }

        public IActionResult Description(int id)
        {
            var chore = _testService.GetChoreById(id);
            return View(chore);
        }

        public IActionResult DeleteChore(int id)
        {
            string message = "Chore Successfully deleted";

            var chore = _testService.GetChoreById(id);
            if (!_testService.DeleteChore(chore))
                message = "There was a problem while trying to delete the chore";

            return View((object)message);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
