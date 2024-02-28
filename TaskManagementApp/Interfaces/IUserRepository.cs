using TaskManagementApp.Models;

namespace TaskManagementApp.Interfaces
{
	public interface IUserRepository : IRepository<User>
	{
        bool UpdateChores(ICollection<int> choreIds, int userId);
    }
}
