using AdvancedDotNetPatternsDemo.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

public class TodoCompletedEventHandler : INotificationHandler<TodoCompletedEvent>
{
    private readonly ILogger<TodoCompletedEventHandler> _logger;

    public TodoCompletedEventHandler(ILogger<TodoCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TodoCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Todo completed → Id: {TodoId}", notification.TodoId);
        // Example: send notification, update statistics, etc.
        return Task.CompletedTask;
    }
}