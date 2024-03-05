using TaskManagementApp.Models;

namespace TaskManagementApp.Interfaces
{
    public interface IAuthService
    {
		bool ValidateUser(UserCredentials credentials);
        bool IsUserAuthenticated();
        int GetUserId();
    }
}
