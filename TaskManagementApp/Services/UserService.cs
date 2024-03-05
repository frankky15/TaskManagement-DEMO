using System.Text.RegularExpressions;
using TaskManagementApp.Data;
using TaskManagementApp.Interfaces;
using TaskManagementApp.Models;
using TaskManagementApp.Repository;

namespace TaskManagementApp.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _UserRepository;

		public UserService(IUserRepository userRepository)
		{
			_UserRepository = userRepository;
		}

		public bool AddUser(User user)
		{
			if (!IsEmailValid(user.Email))
				return false;

			if (!IsPasswordValid(user.Password))
				return false;

			if (!_UserRepository.Add(user))
				return false;

			return true;
		}

		private bool IsPasswordValid(string password)
		{
			// Regular expression pattern
			string pattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$";
			return Regex.IsMatch(password, pattern);
		}

		private bool IsEmailValid(string email)
		{
			// Regular expression pattern
			string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
			return Regex.IsMatch(email, pattern);
		}
	}
}
