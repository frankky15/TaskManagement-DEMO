using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskManagementApp.Interfaces;
using TaskManagementApp.Models;
using TaskManagementApp.Services;

namespace TaskManagementApp.Controllers
{
	public class AuthenticationController : Controller
	{
		private readonly IUserService _userService;
		private readonly IAuthService _authService;

		public AuthenticationController(IUserService userservice, IAuthService authService)
		{
			_userService = userservice;
			_authService = authService;
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
			
			return RedirectToAction("Index", "Home");
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
			_authService.Logout();
			return RedirectToAction("Index", "Home");
		}

		public IActionResult ErrorMessage(string message)
		{
			return View((object)message);
		}
	}
}
