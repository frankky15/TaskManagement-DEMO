using System.Linq;
using System.Linq.Expressions;
using TaskManagementApp.Data;
using TaskManagementApp.Interfaces;
using TaskManagementApp.Models;

namespace TaskManagementApp.Repository
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(InMemoryDB db)
        {
            _inMemoryDB = db;
        }

        private InMemoryDB _inMemoryDB;

        public bool Add(User entity)
        {
            if (entity == null)
                return false;

            try
            {
                if (string.IsNullOrEmpty(entity.Username))
                    return false;

                if (string.IsNullOrEmpty(entity.Email))
                    return false;

                if (string.IsNullOrEmpty(entity.Password))
                    return false;

                var maxID = _inMemoryDB.Users.Max(x => x.ID);
                entity.ID = maxID + 1;

                _inMemoryDB.Users.Add(entity);
                _inMemoryDB.SaveDB();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: There was a problem while trying to add an user. Exeption: {ex}");
                return false;
            }
        }

        public User Get(Expression<Func<User, bool>> _where)
        {
            try
            {
                var queryableUsers = _inMemoryDB.Users.AsQueryable();

                var filteredUsers = queryableUsers.Where(_where);

                if (filteredUsers.Count() == 0)
                {
                    Console.WriteLine("Error: Couldn't find a user that met the criteria.");
                    return null;
                }

                return filteredUsers.First();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: There was a problem while trying to get a user. Exeption: {ex}");
                return null;
            }
        }

        public ICollection<User> GetAll()
        {
            if (_inMemoryDB.Users.Count > 0)
            {
                return _inMemoryDB.Users;
            }
            else
            {
                Console.WriteLine("Warning: Empty Users Data Base.");

                return null;
            }
        }

        public User GetById(int id)
        {
            var user = from _user in _inMemoryDB.Users
                        where _user.ID == id
                        select _user;

            if (user.Count() == 0)
            {
                Console.WriteLine($"Error: Couldn't find User with ID: {id}");
                return null;
            }

            if (user.Count() > 1)
            {
                Console.WriteLine($"Error: There are multiple instances of UserID: {user.First().ID}");
            }

            return user.First();
        }

        public ICollection<User> GetMany(Expression<Func<User, bool>> _where)
        {
            var queryableUsers = _inMemoryDB.Users.AsQueryable();

            var filteredUsers = queryableUsers.Where(_where).ToList();

            if (filteredUsers.Count() == 0)
            {
                Console.WriteLine("Error: Couldn't find any users that met the criteria.");
                return null;
            }

            return filteredUsers;
        }

        public bool UpdateChores(ICollection<int> choreIds, int userId)
        {
            if (choreIds.Count == 0)
            {
                Console.WriteLine("Error: Did not provide any chore ids to add to the user list.");
                return false;
            }

            var user = GetById(userId);

            foreach (var choreId in choreIds)
            {
                if (choreId != 0 && IdExists(choreId))
                    user.ChoreIDs.Add(choreId);

                else
                    Console.WriteLine("Error: Invalid choreId.");
            }

            _inMemoryDB.SaveDB();

            return true;
        }

        public bool IdExists(int id)
        {
            if (_inMemoryDB.Users.Where(x => x.ID == id).Any())
                return true;

            return false;
        }
    }
}
