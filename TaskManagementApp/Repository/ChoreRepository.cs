using Microsoft.VisualBasic;
using System;
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
            try
            {
                if (!_inMemoryDB.AddChore(entity))
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: There was a problem while trying to add a chore. Exeption: {ex}");
                return false;
            }

        }

        public bool Delete(Chore entity)
        {
            try
            {
                if (!_inMemoryDB.DeleteChore(entity))
                {
                    Console.WriteLine($"Error: ChoreTitle: {entity.Title} does not exist in the db.");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: There was a problem while trying to delete a chore. Exeption: {ex}");
                return false;
            }
        }

        public bool Delete(Expression<Func<Chore, bool>> _where)
        {
            try
            {
                var queryableChores = _inMemoryDB.Chores.AsQueryable();

                var filteredChores = queryableChores.Where(_where).ToList();

                foreach (var chore in filteredChores)
                {
                    if (!_inMemoryDB.DeleteChore(chore))
                        return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: There was a problem while trying to delete multiple chores. Exeption: {ex}");
                return false;
            }
        }

        public Chore Get(Expression<Func<Chore, bool>> _where)
        {
            try
            {
                var queryableChores = _inMemoryDB.Chores.AsQueryable();

                var filteredChores = queryableChores.Where(_where);

                return filteredChores.First();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: There was a problem while trying to get a chore. Exeption: {ex}");
                return null;
            }
        }

        public ICollection<Chore> GetAll()
        {
            if (_inMemoryDB.Chores.Count > 0)
            {
                return _inMemoryDB.Chores;
            }
            else
            {
                Console.WriteLine("Warning: Empty Chore Data Base.");

                return new List<Chore>();
            }
        }

        public Chore GetById(int id)
        {
            var chore = from obj in _inMemoryDB.Chores
                        where obj.ID == id
                        select obj;

            if (chore.Count() == 0)
                return null;

            if (chore.Count() > 1)
            {
                Console.WriteLine($"Error: There are multiple instances of ChoreID: {chore.First().ID}");
            }

            return chore.First();
        }

        public ICollection<Chore> GetMany(Expression<Func<Chore, bool>> _where)
        {
            var queryableChores = _inMemoryDB.Chores.AsQueryable();

            var filteredChores = queryableChores.Where(_where).ToList();

            return filteredChores;
        }

        public bool Update(Chore entity)
        {
            if (!_inMemoryDB.UpdateChore(entity))
            {
                Console.WriteLine($"Error: Couldn't update Chore. ChoreID: {entity.ID}");
                return false;
            }

            return true;
        }
    }
}
