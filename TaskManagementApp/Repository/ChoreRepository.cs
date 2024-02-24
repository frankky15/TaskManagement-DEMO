using System.Linq.Expressions;
using TaskManagementApp.Data;
using TaskManagementApp.Interfaces;
using TaskManagementApp.Models;

namespace TaskManagementApp.Repository
{
    public class ChoreRepository : IChoreRepository
    {
        public ChoreRepository(InMemoryDB db)
        {
            _inMemoryDB = db;
        }

        private InMemoryDB _inMemoryDB;

        public bool Add(Chore entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Chore entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Expression<Func<Chore, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Chore Get(Expression<Func<Chore, bool>> where)
        {
            throw new NotImplementedException();
        }

        public ICollection<Chore> GetAll()
        {
            throw new NotImplementedException();
        }

        public Chore GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Chore> GetMany(Expression<Func<Chore, bool>> where)
        {
            throw new NotImplementedException();
        }

        public bool Update(Chore entity)
        {
            throw new NotImplementedException();
        }
    }
}
