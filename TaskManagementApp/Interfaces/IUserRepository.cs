using TaskManagementApp.Models;

namespace TaskManagementApp.Interfaces
{
	public interface IUserRepository : IRepository<User>
	{
        bool AddChore(int choreId, int userId);
        bool AddChores(ICollection<int> choreIds, int userId);
        bool DeleteChore(int choreId, int userId);
        bool DeleteChores(ICollection<int> choreIds, int userId);
    }
}
