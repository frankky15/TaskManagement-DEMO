using TaskManagementApp.Models;
using TaskManagementApp.Services;

namespace TaskManagementApp.Data
{
    public interface IInMemoryDB
    {
        bool SaveDB();
    }

    public class InMemoryDB : IInMemoryDB // Crude DB Representation for Demo Purpuses...
    {
        public InMemoryDB(IUserJsonService userJsonService, IChoreJsonService choreJsonService)
        {
            _userJsonService = userJsonService;
            _choreJsonService = choreJsonService;

            PopulateUsers();
            PopulateChores();
        }

        private readonly IUserJsonService _userJsonService;
        private readonly IChoreJsonService _choreJsonService;

        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Chore> Chores { get; set; } = new List<Chore>();

        private bool PopulateUsers()
        {
            var _users = _userJsonService.GetUsers();

            if (_users.Count() == 0)
            {
                Console.WriteLine("Warning: There was a problem while getting the users.");
                return false;
            }

            foreach (var user in _users)
            {
                Users.Add(user);
                //Console.WriteLine("Loaded User: " + user.Username + " UserID: " + user.ID);
            }

            return true;
        }

        private bool PopulateChores()
        {
            var _chores = _choreJsonService.GetChores();

            if (_chores.Count() == 0)
            {
                Console.WriteLine("Warning: There was a problem while getting the chores.");
                return false;
            }

            foreach (var chore in _chores)
            {
                Chores.Add(chore);
                //Console.WriteLine("Loaded Chore: " + chore.Title + " ChoreID: " + chore.ID);
            }

            return true;
        }

        public bool SaveDB()
        {
            if (!_userJsonService.SaveUsersToDB(Users))
            {
                Console.WriteLine("There was an Unknown Error while trying to save Users to DB.");
                return false;
            }

            if (!_choreJsonService.SaveChoresToDB(Chores))
            {
                Console.WriteLine("There was an Unknown Error while trying to save Chores to DB.");
                return false;                
            }

            return true;
        }
    }
}
