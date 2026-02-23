using AdvancedDotNetPatternsDemo.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AdvancedDotNetPatternsDemo.Application.Features.Todo.Events
{
    public class TodoCreatedEventHandler : INotificationHandler<TodoCreatedEvent>
    {
        private readonly ILogger<TodoCreatedEventHandler> _logger;

        public TodoCreatedEventHandler(ILogger<TodoCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(TodoCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "New todo created → Title: {Title} | Id: {TodoId}",
                notification.Title,
                notification.TodoId);

            // You can perform side effects here: send email, update read model, etc.
            return Task.CompletedTask;
        }
    }
}