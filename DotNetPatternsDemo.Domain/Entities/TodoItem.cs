using System;

namespace AdvancedDotNetPatternsDemo.Domain.Entities
{
    public class TodoItem
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public bool IsCompleted { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? DueDate { get; private set; }

        // Constructor for initial creation (a domain factory method would be better,
        // but kept simple for now)
        private TodoItem() { } // For EF Core

        public static TodoItem Create(string title, string? description = null, DateTime? dueDate = null)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Task title is required.", nameof(title));

            return new TodoItem
            {
                Id = Guid.NewGuid(),
                Title = title.Trim(),
                Description = description?.Trim(),
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow,
                DueDate = dueDate
            };
        }

        public void MarkAsCompleted()
        {
            if (IsCompleted)
                throw new InvalidOperationException("Task has already been completed.");

            IsCompleted = true;
        }

        public void UpdateTitle(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
                throw new ArgumentException("New title cannot be empty.");

            Title = newTitle.Trim();
        }
    }
}