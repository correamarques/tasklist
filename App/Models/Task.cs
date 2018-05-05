using System;

namespace App.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}