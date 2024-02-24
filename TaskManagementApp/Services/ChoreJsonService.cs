using System.Text.Json;
using TaskManagementApp.Models;

namespace TaskManagementApp.Services
{
    public static class ChoreJsonService
    {
        private static string _JsonFilePath
        {
            get
            {
                
                string path = Environment.GetEnvironmentVariable("JsonData__Chore");
                //string path = "wwwroot/data/ChoresData.json";
                return path;
            }
        }

        public static IEnumerable<Chore> GetChores()
        {
            try
            {
                if (!File.Exists(_JsonFilePath))
                {
                    Console.WriteLine($"Error: JSON file path is invalid or file does not exist. Path: {_JsonFilePath}");
                    return Enumerable.Empty<Chore>();
                }

                string jsonData = File.ReadAllText(_JsonFilePath);
                var chores = JsonSerializer.Deserialize<Chore[]>(jsonData);

                return chores ?? Enumerable.Empty<Chore>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ocurred while reading json data: {ex}");
                return Enumerable.Empty<Chore>();
            }
        }

        public static bool SaveChoresToDB(IEnumerable<Chore> chores)
        {
            if (chores == null)
                return false;

            try
            {
                if (string.IsNullOrEmpty(_JsonFilePath) || !File.Exists(_JsonFilePath))
                {
                    Console.WriteLine($"Error: JSON file path is invalid or file does not exist. Path: {_JsonFilePath}");
                    return false;
                }

                string jsonData = JsonSerializer.Serialize(chores);
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
