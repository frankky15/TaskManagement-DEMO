using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace TaskManagementApp.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

        public IEnumerable<int> ChoreIDs { get; set; }

        public override string ToString() => JsonSerializer.Serialize<User>(this);

    }
}
