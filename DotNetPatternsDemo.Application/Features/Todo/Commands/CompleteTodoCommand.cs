using AdvancedDotNetPatternsDemo.Domain.Outbox;
using MediatR;

namespace AdvancedDotNetPatternsDemo.Application.Features.Todo.Commands
{
    public record CompleteTodoCommand(Guid TodoId) : IRequest<bool>;

    public class CompleteTodoCommandHandler : IRequestHandler<CompleteTodoCommand, bool>
    {
        private readonly ITodoRepository _repository;
        private readonly IPublisher _publisher;   // ← MediatR Publisher for event publishing
        private readonly AppDbContext _dbContext; // ← EF Core DbContext for outbox access
        public CompleteTodoCommandHandler(ITodoRepository repository, IPublisher publisher, AppDbContext dbContext)
        {
            _repository = repository;
            _publisher = publisher;
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(CompleteTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(request.TodoId);
            if (todo == null) return false;

            todo.MarkAsCompleted();

            // Publish all domain events
            foreach (var domainEvent in todo.DomainEvents)
            {
                var outboxMsg = OutboxMessage.Create(domainEvent);
                _dbContext.OutboxMessages.Add(outboxMsg);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            todo.ClearDomainEvents(); // Clear after publishing

            return true;
        }
    }
}