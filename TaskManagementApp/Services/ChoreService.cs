using TaskManagementApp.Data;
using TaskManagementApp.Interfaces;
using TaskManagementApp.Models;
using TaskManagementApp.Repository;

namespace TaskManagementApp.Services
{
	public class ChoreService : IChoreService
	{
		private readonly IChoreRepository _ChoreRepository;

		public ChoreService(IChoreRepository choreRepository)
		{
			_ChoreRepository = choreRepository;
		}

		public IEnumerable<Chore> GetChores(int userId)
		{
			var chores = _ChoreRepository.GetMany(x => x.UserID == userId);

			return chores;
		}

		public Chore GetChoreById(int id)
		{			
			var chore = _ChoreRepository.GetById(id);
			return chore;
		}

		public bool DeleteChore(Chore chore)
		{
			if (!_ChoreRepository.Delete(chore))
				return false;

			return true;
		}

		public bool AddChore(Chore chore)
		{
			if (!_ChoreRepository.Add(chore))
				return false;

			return true;
		}
	}
}
