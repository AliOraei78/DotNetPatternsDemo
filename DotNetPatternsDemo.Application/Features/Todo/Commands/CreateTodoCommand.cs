using MediatR;
using AdvancedDotNetPatternsDemo.Domain.Entities;

namespace AdvancedDotNetPatternsDemo.Application.Features.Todo.Commands
{
    public record CreateTodoCommand(string Title, string? Description, DateTime? DueDate)
        : IRequest<Guid>;   // Output: Id of the created task

    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, Guid>
    {
        private readonly ITodoRepository _repository;
        private readonly IPublisher _publisher;   // ← MediatR Publisher for event publishing

        public CreateTodoCommandHandler(ITodoRepository repository, IPublisher publisher)
        {
            _repository = repository;
            _publisher = publisher;
        }

        public async Task<Guid> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = TodoItem.Create(request.Title, request.Description, request.DueDate);

            await _repository.AddAsync(todo);
            await _repository.SaveChangesAsync();

            // Publish all domain events
            foreach (var domainEvent in todo.DomainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }

            todo.ClearDomainEvents(); // Clear after publishing

            return todo.Id;
        }
    }
}