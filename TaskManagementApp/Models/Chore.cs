﻿using System.Text.Json;

namespace TaskManagementApp.Models
{
    public class Chore
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public int UserID { get; set; }

        public override string ToString () => JsonSerializer.Serialize<Chore>(this);
    }
}
