using AdvancedDotNetPatternsDemo.Application.Features.Todo.Dtos;
using AdvancedDotNetPatternsDemo.Domain.Entities;

namespace AdvancedDotNetPatternsDemo.Infrastructure.Persistence
{
    public class InMemoryTodoReadRepository : ITodoReadRepository
    {
        // Note: In a real-world scenario, the read side might read from a different source
        // (e.g., a view or cache). For simplicity, we share the same InMemory list (shared state).
        private readonly List<TodoItem> _todos;

        // Constructor: receives the list from the write repository
        // (in practice, this could be completely separate)
        public InMemoryTodoReadRepository(InMemoryTodoRepository writeRepo)
        {
            // For simple testing, we share the list.
            // In a real implementation, this is not recommended
            // (better to use projection or event sourcing).
            _todos = typeof(InMemoryTodoRepository)
                .GetField("_todos", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
                .GetValue(writeRepo) as List<TodoItem>
                ?? throw new InvalidOperationException("Could not read the list");
        }

        public async Task<TodoDto?> GetByIdAsync(Guid id)
        {
            var item = _todos.FirstOrDefault(t => t.Id == id);
            await Task.CompletedTask;
            return item == null ? null : MapToDto(item);
        }

        public async Task<List<TodoDto>> GetAllAsync()
        {
            await Task.CompletedTask;
            return _todos.Select(MapToDto).ToList();
        }

        private static TodoDto MapToDto(TodoItem item) => new(
            item.Id,
            item.Title,
            item.Description,
            item.IsCompleted,
            item.CreatedAt,
            item.DueDate);
    }
}