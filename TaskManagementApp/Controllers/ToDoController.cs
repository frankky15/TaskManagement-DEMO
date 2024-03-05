using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Interfaces;
using TaskManagementApp.Models;
using TaskManagementApp.Services;

namespace TaskManagementApp.Controllers
{
	public class ToDoController : Controller
	{
		private readonly IChoreService _choreService;
		private readonly IAuthService _authService;

		public ToDoController(IChoreService choreService, IAuthService authService)
		{
			_choreService = choreService;
			_authService = authService;
		}

		public IActionResult Index()
		{
			if (!_authService.IsUserAuthenticated())
				return View("LoginToContinue", (Object)"see your chores.");

			var chores = _choreService.GetChores(_authService.GetUserId());
			if (chores == null)
				return View("NoChores");

			return View(chores);
		}

		public IActionResult Details(int id)
		{
			if (!_authService.IsUserAuthenticated())
				return View("LoginToContinue", (Object)"see the details of your chores.");

			var chore = _choreService.GetChoreById(id, _authService.GetUserId());
			return View(chore);
		}

		public IActionResult DeleteChore(int id)
		{
			if (!_authService.IsUserAuthenticated())
				return View("LoginToContinue", (Object)"delete a chore.");

			var chore = _choreService.GetChoreById(id, _authService.GetUserId());

			if (chore == null || !_choreService.DeleteChore(chore, _authService.GetUserId()))
				return View("ErrorMessage", (object)"There was a problem while trying to delete the chore");

			return RedirectToAction("Index");
		}

		public IActionResult DeleteConfirm(int id)
		{
			if (!_authService.IsUserAuthenticated())
				return View("LoginToContinue", (Object)"delete a chore.");

			var chore = _choreService.GetChoreById(id, _authService.GetUserId());
			return View(chore);
		}

		public IActionResult CreateChore()
		{
			if (!_authService.IsUserAuthenticated())
				return View("LoginToContinue", (Object)"create a chore.");

			return View();
		}

		[HttpPost]
		public IActionResult CreateChore(Chore chore)
		{
			if (!_authService.IsUserAuthenticated())
				return View("LoginToContinue", (Object)"create a chore.");

			if (!_choreService.AddChore(chore, _authService.GetUserId()))
			{
				string errorMessage = "There was a problem while trying to create a new chore";

				return RedirectToAction("ErrorMessage", "Home", new { message = errorMessage });
			}

			return View("ChoreCreated");
		}

		public IActionResult ErrorMessage(string message)
		{
			return View((object)message);
		}
	}
}
