using MediatR;
using AdvancedDotNetPatternsDemo.Domain.Entities;

namespace AdvancedDotNetPatternsDemo.Application.Features.Todo.Commands
{
    public record CreateTodoCommand(string Title, string? Description, DateTime? DueDate)
        : IRequest<Guid>;   // Output: Id of the created task

    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, Guid>
    {
        private readonly ITodoRepository _repository;

        public CreateTodoCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = TodoItem.Create(request.Title, request.Description, request.DueDate);
            await _repository.AddAsync(todo);
            await _repository.SaveChangesAsync();
            return todo.Id;
        }
    }
}