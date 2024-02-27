using System.Text.Json;
using TaskManagementApp.Models;

namespace TaskManagementApp.Services
{
    public interface IUserJsonService
    {
        public IEnumerable<User> GetUsers();

        public bool SaveUsersToDB(IEnumerable<User> users);
    }

    public class UserJsonService : IUserJsonService
    {
        public UserJsonService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        private string _JsonFilePath
        {
            get
            {
                string path = _configuration.GetValue<string>("JsonData:User") ?? "wwwroot/data/UsersData.json";
                return path;
            }
        }

        public IEnumerable<User> GetUsers()
        {
            try
            {
                if (string.IsNullOrEmpty(_JsonFilePath) || !File.Exists(_JsonFilePath))
                {
                    Console.WriteLine($"Error: JSON file path is invalid or file does not exist. Path: {_JsonFilePath}");
                    return Enumerable.Empty<User>();
                }

                string jsonData = File.ReadAllText(_JsonFilePath);
                var users = JsonSerializer.Deserialize<User[]>(jsonData);

                return users ?? Enumerable.Empty<User>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ocurred while reading json data: {ex}");
                return Enumerable.Empty<User>();
            }
        }

        public bool SaveUsersToDB(IEnumerable<User> users)
        {
            if (users == null)
                return false;

            try
            {
                if (string.IsNullOrEmpty(_JsonFilePath) || !File.Exists(_JsonFilePath))
                {
                    Console.WriteLine($"Error: JSON file path is invalid or file does not exist. Path: {_JsonFilePath}");
                    return false;
                }

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string jsonData = JsonSerializer.Serialize(users, options);
                File.WriteAllText(_JsonFilePath, jsonData);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ocurred while writing json data: {ex}");
                return false;
            }
        }
    }
}
