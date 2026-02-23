using AdvancedDotNetPatternsDemo.Domain.Entities;

namespace AdvancedDotNetPatternsDemo.Domain.DomainEvents
{
    public record TodoCreatedEvent(
        Guid EventId,
        DateTime OccurredOn,
        Guid TodoId,
        string Title,
        string? Description,
        DateTime CreatedAt,
        DateTime? DueDate
    ) : IDomainEvent
    {
        public TodoCreatedEvent(TodoItem todo)
            : this(
                Guid.NewGuid(),
                DateTime.UtcNow,
                todo.Id,
                todo.Title,
                todo.Description,
                todo.CreatedAt,
                todo.DueDate)
        { }
    }
}