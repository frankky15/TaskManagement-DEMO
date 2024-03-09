using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Interfaces;
using TaskManagementApp.Models;
using TaskManagementApp.Services;

namespace TaskManagementApp.Controllers
{
	public class ToDoController : Controller
	{
		private readonly IUserService _userService;
		private readonly IChoreService _choreService;
		private readonly IAuthService _authService;

		public ToDoController(IUserService userService, IChoreService choreService, IAuthService authService)
		{
			_userService = userService;
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

			var chore = _choreService.GetChoreById(id);
			if (chore == null)
				return View("ErrorMessage", (object)"Chore Doesn't exist.");

			if (chore.UserID != _authService.GetUserId())
				return View("ErrorMessage", (object)"You Don't have permission to access that chore.");

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

			chore.UserID = _authService.GetUserId();

			if (!_choreService.AddChore(chore))
				return View("ErrorMessage", (object)"There was a problem while trying to create a new chore.");

			if (!_userService.AddChore(chore.ID, chore.UserID))
				return View("ErrorMessage", (object)"There was a problem while trying to save the chore to the user's list.");

			return View("ChoreCreated");
		}

		public IActionResult EditChore(int id)
		{
			if (!_authService.IsUserAuthenticated())
				return View("LoginToContinue", (Object)"edit a chore.");

			var chore = _choreService.GetChoreById(id);
			if (chore == null)
				return View("ErrorMessage", (object)"Chore Doesn't exist.");

			if (chore.UserID != _authService.GetUserId())
				return View("ErrorMessage", (object)"You Don't have permission to access that chore.");

			return View(chore);
		}

		[HttpPost]
		public IActionResult EditChore(Chore chore)
		{
			if (!_authService.IsUserAuthenticated())
				return View("LoginToContinue", (Object)"edit a chore.");

			chore.UserID = _authService.GetUserId();

			if (!_choreService.EditChore(chore))
				return View("ErrorMessage", (object)"There was a problem while trying to edit a chore.");

			return RedirectToAction("Index");
		}

		public IActionResult DeleteConfirm(int id)
		{
			if (!_authService.IsUserAuthenticated())
				return View("LoginToContinue", (Object)"delete a chore.");

			var chore = _choreService.GetChoreById(id);
			if (chore == null)
				return View("ErrorMessage", (object)"Chore Doesn't exist.");

			if (chore.UserID != _authService.GetUserId())
				return View("ErrorMessage", (object)"You Don't have permission to access that chore.");

			return View(chore);
		}

		public IActionResult DeleteChore(int id)
		{
			if (!_authService.IsUserAuthenticated())
				return View("LoginToContinue", (Object)"delete a chore.");

			var chore = _choreService.GetChoreById(id);
			if (chore == null)
				return View("ErrorMessage", (object)"Chore Doesn't exist.");

			if (chore.UserID != _authService.GetUserId())
				return View("ErrorMessage", (object)"You Don't have permission to access that chore.");

			if (!_userService.DeleteChore(chore.ID, chore.UserID))
				return View("ErrorMessage", (object)"There was a problem while trying to Delete the chore from the user's list.");

			if (!_choreService.DeleteChore(chore))
				return View("ErrorMessage", (object)"There was a problem while trying to delete the chore");

			return RedirectToAction("Index");
		}

		public IActionResult CompleteChore(int id)
		{
			if (!_authService.IsUserAuthenticated())
				return View("LoginToContinue", (Object)"complete a chore.");

			var chore = _choreService.GetChoreById(id);
			if (chore == null)
				return View("ErrorMessage", (object)"Chore Doesn't exist.");

			if (chore.UserID != _authService.GetUserId())
				return View("ErrorMessage", (object)"You Don't have permission to access that chore.");

			if (!_choreService.CompleteChore(chore))
				return View("ErrorMessage", (object)"There was a problem while trying to complete the chore");

			return RedirectToAction("Index");
		}
	}
}
