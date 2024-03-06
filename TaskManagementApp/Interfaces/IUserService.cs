using TaskManagementApp.Models;

namespace TaskManagementApp.Interfaces
{
	public interface IUserService
	{
		bool AddUser(User user);
        bool AddChore(int choreId, int userId);
        bool AddChores(ICollection<int> choreIds, int userId);
        bool DeleteChore(int choreId, int userId);
        bool DeleteChores(ICollection<int> choreIds, int userId);
    }
}
