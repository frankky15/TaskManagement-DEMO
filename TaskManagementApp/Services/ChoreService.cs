using TaskManagementApp.Data;
using TaskManagementApp.Interfaces;
using TaskManagementApp.Models;
using TaskManagementApp.Repository;

namespace TaskManagementApp.Services
{
	public class ChoreService : IChoreService
	{
		private IChoreRepository _ChoreRepository { get; set; }

		public ChoreService(IChoreRepository choreRepository)
		{
			_ChoreRepository = choreRepository;
		}

		public IEnumerable<Chore> GetChores()
		{
			// return only the user's chores
			return _ChoreRepository.GetAll();
		}

		public Chore GetChoreById(int id)
		{
			// if id belongs to the user
			var chore = _ChoreRepository.GetById(id);
			return chore;
		}

		public bool DeleteChore(Chore chore)
		{
			// if id belongs to the user
			if (!_ChoreRepository.Delete(chore))
				return false;

			return true;
		}

		public bool AddChore(Chore chore)
		{
			// Set the userID to the user's id 

			if (!_ChoreRepository.Add(chore))
				return false;

			return true;
		}
	}
}
