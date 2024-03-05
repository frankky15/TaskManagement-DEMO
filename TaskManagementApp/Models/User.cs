using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace TaskManagementApp.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = string.Empty;

        public ICollection<int> ChoreIDs { get; set; } = new List<int>();

        public override string ToString() => JsonSerializer.Serialize<User>(this);

    }
}
