using MediatR;

namespace AdvancedDotNetPatternsDemo.Application.Features.Todo.Commands
{
    public record CompleteTodoCommand(Guid TodoId) : IRequest<bool>;

    public class CompleteTodoCommandHandler : IRequestHandler<CompleteTodoCommand, bool>
    {
        private readonly ITodoRepository _repository;

        public CompleteTodoCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CompleteTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(request.TodoId);
            if (todo == null) return false;

            todo.MarkAsCompleted();
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}