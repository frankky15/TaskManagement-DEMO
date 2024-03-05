using TaskManagementApp.Models;

namespace TaskManagementApp.Interfaces
{
	public interface IChoreService
	{
		IEnumerable<Chore> GetChores(int userId);
		Chore GetChoreById(int id, int userId);
		bool DeleteChore(Chore chore, int userId);
		bool AddChore(Chore chore, int userId);
	}
}
