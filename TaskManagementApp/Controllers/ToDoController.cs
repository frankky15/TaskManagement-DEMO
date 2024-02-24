using Microsoft.AspNetCore.Mvc;

namespace TaskManagementApp.Controllers
{
    public class ToDoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
