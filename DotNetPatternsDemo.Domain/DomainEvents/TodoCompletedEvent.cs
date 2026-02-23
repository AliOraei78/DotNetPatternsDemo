namespace AdvancedDotNetPatternsDemo.Domain.DomainEvents
{
    public record TodoCompletedEvent(
        Guid EventId,
        DateTime OccurredOn,
        Guid TodoId,
        DateTime CompletedAt
    ) : IDomainEvent
    {
        public TodoCompletedEvent(Guid todoId)
            : this(Guid.NewGuid(), DateTime.UtcNow, todoId, DateTime.UtcNow)
        { }
    }
}