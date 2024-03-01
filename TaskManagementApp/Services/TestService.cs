using TaskManagementApp.Data;
using TaskManagementApp.Models;
using TaskManagementApp.Repository;

namespace TaskManagementApp.Services
{
    public interface ITestService
    {
        public IEnumerable<Chore> GetChores();
        public Chore GetChoreById(int id);
        public bool DeleteChore(Chore chore);
    }

    public class TestService : ITestService
    {
        private ChoreRepository _ChoreRepository { get; set; }
        private UserRepository _UserRepository { get; set; }
        private InMemoryDB _InMemoryDB { get; set; }

        public TestService(InMemoryDB db)
        {
            try
            {
                _InMemoryDB = db;

                _ChoreRepository = new ChoreRepository(_InMemoryDB);
                _UserRepository = new UserRepository(_InMemoryDB);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was a problem with the Test Service. Exeption: {ex}");
            }
        }

        public IEnumerable<Chore> GetChores()
        {
            return _ChoreRepository.GetAll();
        }

        public Chore GetChoreById(int id)
        {
            var chore = _ChoreRepository.GetById(id);
            return chore;
        }

        public bool DeleteChore(Chore chore)
        {
            if (!_ChoreRepository.Delete(chore))
                return false;

            return true;
        }
    }
}
