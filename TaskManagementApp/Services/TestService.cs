using TaskManagementApp.Data;
using TaskManagementApp.Repository;

namespace TaskManagementApp.Services
{
    public interface ITestService
    {

    }

    public class TestService : ITestService
    {
        private ChoreRepository _ChoreRepository { get; set; }
        private UserRepository _UserRepository { get; set; }

        public TestService()
        {
            try
            {
                //_ChoreRepository = new ChoreRepository(InMemoryDB.Instance);
                //_UserRepository = new UserRepository(InMemoryDB.Instance);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was a problem with the Test Service. Exeption: {ex}");
            }
        }
    }
}
