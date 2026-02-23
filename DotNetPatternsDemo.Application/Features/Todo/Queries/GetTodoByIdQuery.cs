using MediatR;
using AdvancedDotNetPatternsDemo.Application.Features.Todo.Dtos;   // Describe later

namespace AdvancedDotNetPatternsDemo.Application.Features.Todo.Queries
{
    public record GetTodoByIdQuery(Guid Id) : IRequest<TodoDto?>;

    public class GetTodoByIdQueryHandler : IRequestHandler<GetTodoByIdQuery, TodoDto?>
    {
        private readonly ITodoReadRepository _readRepository;

        public GetTodoByIdQueryHandler(ITodoReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<TodoDto?> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
        {
            return await _readRepository.GetByIdAsync(request.Id);
        }
    }
}