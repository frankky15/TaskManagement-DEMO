using System.Text.RegularExpressions;
using TaskManagementApp.Data;
using TaskManagementApp.Interfaces;
using TaskManagementApp.Models;
using TaskManagementApp.Repository;

namespace TaskManagementApp.Services
{
	public class UserService : IUserService
	{
		private IUserRepository _UserRepository { get; set; }

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

		public bool ValidateUser(UserCredentials userCredentials)
		{
			var user = _UserRepository.Get(x => x.Username == userCredentials.Username);
			if (user == null)
			{
				user = _UserRepository.Get(x => x.Email == userCredentials.Email);

				if (user == null)
				{
					Console.WriteLine($"Error: Username:'{userCredentials.Username}' or Email:'{userCredentials.Email}' Does not exist.");
					return false;
				}
			}

			if (user.Password != userCredentials.Password)
			{
				Console.WriteLine("Error: Invalid Password.");
				return false;
			}

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
