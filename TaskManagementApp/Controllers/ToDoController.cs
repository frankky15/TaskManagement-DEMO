using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Interfaces;
using TaskManagementApp.Models;
using TaskManagementApp.Services;

namespace TaskManagementApp.Controllers
{
	public class ToDoController : Controller
	{
		private readonly IChoreService _choreService;

		public ToDoController(IChoreService choreService)
		{
			_choreService = choreService;
		}

		public IActionResult Index()
		{
			var chores = _choreService.GetChores();
			if (chores == null)
				return View("NoChores");

			return View(chores);
		}

		public IActionResult Details(int id)
		{
			var chore = _choreService.GetChoreById(id);
			return View(chore);
		}

		public IActionResult DeleteChore(int id)
		{
			var chore = _choreService.GetChoreById(id);

			if (chore == null || !_choreService.DeleteChore(chore))
				return View("ErrorMessage", (object)"There was a problem while trying to delete the chore");

			return RedirectToAction("Index");
		}

		public IActionResult DeleteConfirm(int id)
		{
			var chore = _choreService.GetChoreById(id);
			return View(chore);
		}

		public IActionResult CreateChore()
		{
			return View();
		}

		[HttpPost]
		public IActionResult CreateChore(Chore chore)
		{
			if (!_choreService.AddChore(chore))
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
