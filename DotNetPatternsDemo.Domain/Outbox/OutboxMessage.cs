using AdvancedDotNetPatternsDemo.Domain.DomainEvents;
using System;

namespace AdvancedDotNetPatternsDemo.Domain.Outbox
{
    public class OutboxMessage
    {
        public Guid Id { get; private set; }
        public string Type { get; private set; } = string.Empty;      // Full name of the event type
        public string Payload { get; private set; } = string.Empty;   // JSON serialized event
        public DateTime CreatedAt { get; private set; }
        public DateTime? ProcessedAt { get; private set; }
        public int RetryCount { get; private set; }
        public string? Error { get; private set; }

        private OutboxMessage() { } // For EF

        public static OutboxMessage Create<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent
        {
            return new OutboxMessage
            {
                Id = Guid.NewGuid(),
                Type = typeof(TEvent).AssemblyQualifiedName ?? throw new InvalidOperationException(),
                Payload = System.Text.Json.JsonSerializer.Serialize(domainEvent),
                CreatedAt = DateTime.UtcNow,
                ProcessedAt = null,
                RetryCount = 0
            };
        }

        public void MarkAsProcessed()
        {
            ProcessedAt = DateTime.UtcNow;
        }

        public void MarkAsFailed(string error)
        {
            RetryCount++;
            Error = error;
        }
    }
}