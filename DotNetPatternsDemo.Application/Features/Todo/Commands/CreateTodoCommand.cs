using AdvancedDotNetPatternsDemo.Domain.Entities;
using AdvancedDotNetPatternsDemo.Domain.Outbox;
using MediatR;

namespace AdvancedDotNetPatternsDemo.Application.Features.Todo.Commands
{
    public record CreateTodoCommand(string Title, string? Description, DateTime? DueDate)
        : IRequest<Guid>;   // Output: Id of the created task

    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, Guid>
    {
        private readonly ITodoRepository _repository;
        private readonly IPublisher _publisher;   // ← MediatR Publisher for event publishing
        private readonly AppDbContext _dbContext; // ← EF Core DbContext for outbox access

        public CreateTodoCommandHandler(ITodoRepository repository, IPublisher publisher, AppDbContext dbContext)
        {
            _repository = repository;
            _publisher = publisher;
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = TodoItem.Create(request.Title, request.Description, request.DueDate);

            await _repository.AddAsync(todo);

            // Publish all domain events
            foreach (var domainEvent in todo.DomainEvents)
            {
                var outboxMsg = OutboxMessage.Create(domainEvent);
                _dbContext.OutboxMessages.Add(outboxMsg);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            todo.ClearDomainEvents(); // Clear after publishing

            return todo.Id;
        }
    }
}