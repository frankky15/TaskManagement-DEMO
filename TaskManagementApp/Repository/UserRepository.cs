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
            throw new NotImplementedException();
        }

        public bool Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Expression<Func<User, bool>> where)
        {
            throw new NotImplementedException();
        }

        public User Get(Expression<Func<User, bool>> where)
        {
            throw new NotImplementedException();
        }

        public ICollection<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<User> GetMany(Expression<Func<User, bool>> where)
        {
            throw new NotImplementedException();
        }

        public bool Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
