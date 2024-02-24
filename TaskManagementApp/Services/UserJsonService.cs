using System.Text.Json;
using TaskManagementApp.Models;

namespace TaskManagementApp.Services
{
    public static class UserJsonService
    {
        private static string _JsonFilePath
        {
            get
            {
                //string path = Environment.GetEnvironmentVariable("JsonData__User") ?? "wwwroot/data/UsersData.json";
                string path = Environment.GetEnvironmentVariable("JsonData__User");
                return path;
            }
        }

        public static IEnumerable<User> GetUsers()
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

        public static bool SaveUsersToDB(IEnumerable<User> users)
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

                string jsonData = JsonSerializer.Serialize(users);
                File.WriteAllText(jsonData, _JsonFilePath);

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
