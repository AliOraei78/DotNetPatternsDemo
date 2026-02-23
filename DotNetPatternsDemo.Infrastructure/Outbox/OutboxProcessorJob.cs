using AdvancedDotNetPatternsDemo.Domain.DomainEvents;
using Hangfire;
using MediatR;  // For publishing
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class OutboxProcessorJob
{
    private readonly AppDbContext _dbContext;
    private readonly IPublisher _publisher;  // MediatR publisher
    private readonly ILogger<OutboxProcessorJob> _logger;

    public OutboxProcessorJob(AppDbContext dbContext, IPublisher publisher, ILogger<OutboxProcessorJob> logger)
    {
        _dbContext = dbContext;
        _publisher = publisher;
        _logger = logger;
    }

    [AutomaticRetry(Attempts = 5)]  // Hangfire automatic retry
    public async Task ProcessPendingMessages()
    {
        var pending = await _dbContext.OutboxMessages
            .Where(m => m.ProcessedAt == null)
            .OrderBy(m => m.CreatedAt)
            .Take(20)  // Batch processing
            .ToListAsync();

        foreach (var msg in pending)
        {
            try
            {
                var eventType = Type.GetType(msg.Type);
                if (eventType == null) throw new InvalidOperationException($"Event type not found: {msg.Type}");

                var domainEvent = System.Text.Json.JsonSerializer.Deserialize(msg.Payload, eventType);

                if (domainEvent is IDomainEvent evt)
                {
                    await _publisher.Publish(evt);
                    msg.MarkAsProcessed();
                    _logger.LogInformation("Outbox message processed: {MessageId}", msg.Id);
                }
            }
            catch (Exception ex)
            {
                msg.MarkAsFailed(ex.Message);
                _logger.LogError(ex, "Error processing Outbox message: {MessageId}", msg.Id);
            }
        }

        await _dbContext.SaveChangesAsync();
    }
}