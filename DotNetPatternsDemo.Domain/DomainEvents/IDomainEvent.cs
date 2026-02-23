using System;
using MediatR;

namespace AdvancedDotNetPatternsDemo.Domain.DomainEvents
{
    public interface IDomainEvent : INotification
    {
        Guid EventId { get; }
        DateTime OccurredOn { get; }
    }
}