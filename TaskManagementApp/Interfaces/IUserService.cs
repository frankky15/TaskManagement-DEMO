﻿using TaskManagementApp.Models;

namespace TaskManagementApp.Interfaces
{
	public interface IUserService
	{
		bool AddUser(User user);
		bool ValidateUser(UserCredentials credentials);
    }
}