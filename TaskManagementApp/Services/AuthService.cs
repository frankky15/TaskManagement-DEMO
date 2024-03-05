using TaskManagementApp.Interfaces;
using TaskManagementApp.Models;

namespace TaskManagementApp.Services
{
    public class AuthService : IAuthService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IUserRepository _userRepository;

        public AuthService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public bool ValidateUser(UserCredentials credentials)
        {
            var user = _userRepository.Get(x => x.Username == credentials.Username);
            if (user == null)
            {
                user = _userRepository.Get(x => x.Email == credentials.Email);

                if (user == null)
                {
                    Console.WriteLine($"Error: Username:'{credentials.Username}' or Email:'{credentials.Email}' Does not exist.");
                    return false;
                }
            }

            if (user.Password != credentials.Password)
            {
                Console.WriteLine("Error: Invalid Password.");
                return false;
            }

            var userID = user.ID;

            _httpContextAccessor.HttpContext?.Session.SetInt32("UserID", userID);
            _httpContextAccessor.HttpContext?.Session.SetString("Username", user.Username);

            return IsUserAuthenticated();
        }

        public bool IsUserAuthenticated()
        {
            return !string.IsNullOrEmpty(_httpContextAccessor.HttpContext?.Session.GetString("Username"));
        }

        public int GetUserId()
        {
            // A 0 means either user wasn't authenticated or couldn't find UserID
            if (!IsUserAuthenticated())
                return 0;

            try
            {
                var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("UserID") ?? 0;
                return userId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: There was an unknown error while trying to get UserID. Exeption: {ex}");
                return 0;
            }
        }
    }
}
