using TaskManagementApp.Models;

namespace TaskManagementApp.Interfaces
{
	public interface IChoreService
	{
		IEnumerable<Chore> GetChores(int userId);
		Chore GetChoreById(int id);
		bool DeleteChore(Chore chore);
		bool AddChore(Chore chore);
		bool EditChore(Chore chore);
		bool CompleteChore(Chore chore);
	}
}
