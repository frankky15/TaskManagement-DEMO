using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace TaskManagementApp.Models
{
    public class Chore
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
		public string Description { get; set; } = string.Empty;
        [Required]
		public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public int UserID { get; set; }

        public override string ToString () => JsonSerializer.Serialize<Chore>(this);
    }
}
