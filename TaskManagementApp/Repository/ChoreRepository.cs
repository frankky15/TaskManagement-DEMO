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

        private readonly InMemoryDB _inMemoryDB;

        public bool Add(Chore entity)
        {
            if (entity == null)
                return false;

            try
            {
                if (string.IsNullOrEmpty(entity.Title))
                {
                    Console.WriteLine("Error: Title is null or empty.");
                    return false;
                }

                if (entity.Description == null) //Description is Optional and can be empty.
                    entity.Description = string.Empty;

                var maxID = 0;

                if (_inMemoryDB.Chores.Any())
                    maxID = _inMemoryDB.Chores.Max(x => x.ID);

                entity.ID = maxID + 1;

                _inMemoryDB.Chores.Add(entity);

                _inMemoryDB.SaveDB();
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
            if (entity == null)
                return false;

            try
            {
                if (!_inMemoryDB.Chores.Remove(entity))
                {
                    Console.WriteLine("Error: could not remove chore from db.");
                    return false;
                }

                _inMemoryDB.SaveDB();

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
                    Delete(chore);
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

                if (filteredChores.Count() == 0)
                {
                    Console.WriteLine("Error: Couldn't find a chore that met the criteria.");
                    return null;
                }

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

                return null;
            }
        }

        public Chore GetById(int id)
        {
            var chore = from _chore in _inMemoryDB.Chores
                        where _chore.ID == id
                        select _chore;

            if (chore.Count() == 0)
            {
                Console.WriteLine($"Error: Couldn't find Chore with ID: {id}");
                return null;
            }

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

            if (filteredChores.Count() == 0)
            {
                Console.WriteLine("Error: Couldn't find any chores that met the criteria.");
                return null;
            }

            return filteredChores;
        }

        public bool Update(Chore entity)
        {
            if (entity == null)
                return false;

            try
            {
                if (!IdExists(entity.ID))
                {
                    Console.WriteLine($"Error: Chore with ID: {entity.ID} does not exist.");
                }

                if (string.IsNullOrEmpty(entity.Title))
                {
                    Console.WriteLine("Error: Title is null or empty.");
                    return false;
                }

                if (entity.Description == null) //Description is Optional and can be empty.
                {
                    Console.WriteLine("Error: description is null.");
                    return false;
                }

                var chore = GetById(entity.ID);

                chore.Title = entity.Title;
                chore.Description = entity.Description;
                chore.DueDate = entity.DueDate;

                _inMemoryDB.SaveDB();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                return false;
            }

            return true;
        }

        public bool IdExists(int id)
        {
            if (_inMemoryDB.Chores.Where(x => x.ID == id).Any())
                return true;

            return false;
        }
    }
}
