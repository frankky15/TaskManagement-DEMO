using System.Text.Json;
using TaskManagementApp.Models;

namespace TaskManagementApp.Services
{
    public interface IChoreJsonService
    {
        public IEnumerable<Chore> GetChores();

        public bool SaveChoresToDB(IEnumerable<Chore> chores);
    }

    public class ChoreJsonService : IChoreJsonService
    {
        public ChoreJsonService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        private string _JsonFilePath
        {
            get
            {
                string path = _configuration.GetValue<string>("JsonData:Chore") ?? "wwwroot/data/ChoresData.json";
                return path;
            }
        }

        public IEnumerable<Chore> GetChores()
        {
            try
            {
                if (string.IsNullOrEmpty(_JsonFilePath) || !File.Exists(_JsonFilePath))
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

        public bool SaveChoresToDB(IEnumerable<Chore> chores)
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

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string jsonData = JsonSerializer.Serialize(chores, options);
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
