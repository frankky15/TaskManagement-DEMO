using TaskManagementApp.Models;
using TaskManagementApp.Services;

namespace TaskManagementApp.Data
{
    public class InMemoryDB // DB Representation for Demo Purpuses... (I Know it's a very dirty way of doing it!!)
    {
        public InMemoryDB()
        {
            PopulateUsers();
            PopulateChores();
        }

        public ICollection<User> Users { get; private set; } = new List<User>();
        public ICollection<Chore> Chores { get; private set; } = new List<Chore>();

        private void PopulateUsers()
        {
            var _users = UserJsonService.GetUsers();

            if (_users.Count() == 0)
            {
                Console.WriteLine("Warning: There was a problem while getting the users.");
                return;
            }

            foreach (var user in _users)
            {
                Users.Add(user);
                Console.WriteLine("New User: " + user.Username);
            }
        }

        private void PopulateChores()
        {
            var _chores = ChoreJsonService.GetChores();

            if (_chores.Count() == 0)
            {
                Console.WriteLine("Warning: There was a problem while getting the chores.");
                return;
            }

            foreach (var chore in _chores)
            {
                Chores.Add(chore);
                Console.WriteLine("New Chore: " + chore.Title);
            }
        }

        public bool AddUser(User newUser)
        {
            if (newUser == null)
                return false;

            foreach (var user in Users)
                if (newUser.Username == user.Username || newUser.Email == user.Email)
                    return false;

            var maxID = Users.Max(x => x.ID);
            newUser.ID = maxID + 1;

            Users.Add(newUser);
            SaveDB();

            return true;
        }

        public bool AddChore(Chore newChore)
        {
            if (newChore == null)
                return false;

            var maxID = Chores.Max(x => x.ID);
            newChore.ID = maxID + 1;

            Chores.Add(newChore);
            SaveDB();

            return true;
        }

        private void SaveDB()
        {
            if (!UserJsonService.SaveUsersToDB(Users))
                Console.WriteLine("There was an Unknown Error while trying to save Users to DB.");

            if (!ChoreJsonService.SaveChoresToDB(Chores))
                Console.WriteLine("There was an Unknown Error while trying to save Chores to DB.");
        }
    }
}
