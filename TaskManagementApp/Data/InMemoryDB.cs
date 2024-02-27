using TaskManagementApp.Models;
using TaskManagementApp.Services;

namespace TaskManagementApp.Data
{
    public class InMemoryDB // DB Representation for Demo Purpuses... (I Know it's a very dirty way of doing it!!)
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

        public ICollection<User> Users { get; private set; } = new List<User>();
        public ICollection<Chore> Chores { get; private set; } = new List<Chore>();

        private void PopulateUsers()
        {
            var _users = _userJsonService.GetUsers();

            if (_users.Count() == 0)
            {
                Console.WriteLine("Warning: There was a problem while getting the users.");
                return;
            }

            foreach (var user in _users)
            {
                Users.Add(user);
                //Console.WriteLine("New User: " + user.Username);
            }
        }

        private void PopulateChores()
        {
            var _chores = _choreJsonService.GetChores();

            if (_chores.Count() == 0)
            {
                Console.WriteLine("Warning: There was a problem while getting the chores.");
                return;
            }

            foreach (var chore in _chores)
            {
                Chores.Add(chore);
                //Console.WriteLine("New Chore: " + chore.Title);
            }
        }

        public bool AddUser(User user)
        {
            if (user == null)
                return false;

            foreach (var _user in Users)
                if (user.Username == _user.Username || user.Email == _user.Email)
                    return false;

            var maxID = Users.Max(x => x.ID);
            user.ID = maxID + 1;

            Users.Add(user);
            SaveDB();

            return true;
        }

        public bool AddUserChores(ICollection<int> choreIds, int targetUserId)
        {
            if (choreIds.Count() < 1)
            {
                Console.WriteLine("Error: No Ids where passed to the method.");
                return false;
            }

            var targetUser = Users.Where(x => x.ID == targetUserId).First();
            if (targetUser == null)
            {
                Console.WriteLine($"Could not find user {targetUserId}");
                return false;
            }

            foreach (var id in choreIds)
            {
                targetUser.ChoreIDs.Add(id);
            }

            return true;
        }

        public bool AddChore(Chore chore)
        {
            if (chore == null)
                return false;

            var maxID = Chores.Max(x => x.ID);
            chore.ID = maxID + 1;

            Chores.Add(chore);
            SaveDB();

            return true;
        }

        public bool UpdateChore(Chore chore)
        {
            if (chore == null)
                return false;

            var targetChore = new Chore();
            bool coincidence = false;

            foreach (var _chore in Chores)
            {
                if (_chore.ID == chore.ID)
                {
                    targetChore = _chore;
                }
            }

            if (!coincidence)
                return false;

            targetChore = chore;
            SaveDB();

            return true;
        }

        public bool DeleteChore(Chore chore)
        {
            if (chore == null)
                return false;

            if (!Chores.Remove(chore))
                return false;

            foreach (var _user in Users)
            {
                if (chore.UserID == _user.ID)
                    foreach (var userChore in _user.ChoreIDs)
                    {
                        if (chore.ID == userChore)
                        {
                            var newChoreIds = _user.ChoreIDs.ToList();

                            if (newChoreIds.Remove(chore.ID))
                                _user.ChoreIDs = newChoreIds;

                            else
                            {
                                Console.WriteLine("There was an unknown error while trying to remove Chore from User's ChoreIDs");
                            }
                        }
                    }
            }

            SaveDB();

            return true;
        }

        private void SaveDB()
        {
            if (!_userJsonService.SaveUsersToDB(Users))
                Console.WriteLine("There was an Unknown Error while trying to save Users to DB.");

            if (!_choreJsonService.SaveChoresToDB(Chores))
                Console.WriteLine("There was an Unknown Error while trying to save Chores to DB.");
        }
    }
}
