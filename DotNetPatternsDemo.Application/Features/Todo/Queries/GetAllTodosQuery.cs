using MediatR;
using AdvancedDotNetPatternsDemo.Application.Features.Todo.Dtos;

namespace AdvancedDotNetPatternsDemo.Application.Features.Todo.Queries
{
    public record GetAllTodosQuery : IRequest<List<TodoDto>>;

    public class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQuery, List<TodoDto>>
    {
        private readonly ITodoReadRepository _readRepository;

        public GetAllTodosQueryHandler(ITodoReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<List<TodoDto>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
        {
            return await _readRepository.GetAllAsync();
        }
    }
}