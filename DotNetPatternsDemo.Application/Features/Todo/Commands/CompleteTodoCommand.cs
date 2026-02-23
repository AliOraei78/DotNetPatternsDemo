using MediatR;

namespace AdvancedDotNetPatternsDemo.Application.Features.Todo.Commands
{
    public record CompleteTodoCommand(Guid TodoId) : IRequest<bool>;

    public class CompleteTodoCommandHandler : IRequestHandler<CompleteTodoCommand, bool>
    {
        private readonly ITodoRepository _repository;
        private readonly IPublisher _publisher;   // ← MediatR Publisher for event publishing

        public CompleteTodoCommandHandler(ITodoRepository repository, IPublisher publisher)
        {
            _repository = repository;
            _publisher = publisher;
        }

        public async Task<bool> Handle(CompleteTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(request.TodoId);
            if (todo == null) return false;

            todo.MarkAsCompleted();
            await _repository.SaveChangesAsync();


            // Publish all domain events
            foreach (var domainEvent in todo.DomainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }

            todo.ClearDomainEvents(); // Clear after publishing

            return true;
        }
    }
}