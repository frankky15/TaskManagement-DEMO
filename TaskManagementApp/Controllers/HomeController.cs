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
		private readonly IUserService _userService;
		private readonly IAuthService _authService;

		public HomeController(ILogger<HomeController> logger, IUserService userservice, IAuthService authService)
		{
			_logger = logger;
			_userService = userservice;
			_authService = authService;
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
		
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(UserCredentials userCredentials)
		{
			if (!_authService.ValidateUser(userCredentials))
				return View("ErrorMessage", (Object)"Invalid Credentials.");
			
			return View("Index");
		}
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(User user)
		{
			if (!_userService.AddUser(user))
				return View("ErrorMessage", (Object)"There was a problem while creating the user.");

			return View("UserCreated");
		}

		public IActionResult Logout()
		{
			return View();
		}

		public IActionResult ErrorMessage(string message)
		{
			return View((object)message);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
