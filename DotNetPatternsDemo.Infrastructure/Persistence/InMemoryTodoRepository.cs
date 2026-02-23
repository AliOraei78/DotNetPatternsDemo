using AdvancedDotNetPatternsDemo.Domain.Entities;

namespace AdvancedDotNetPatternsDemo.Infrastructure.Persistence
{
    public class InMemoryTodoRepository : ITodoRepository
    {
        private readonly List<TodoItem> _todos = new();

        public async Task AddAsync(TodoItem todo)
        {
            _todos.Add(todo);
            await Task.CompletedTask;
        }

        public async Task<TodoItem?> GetByIdAsync(Guid id)
        {
            var item = _todos.FirstOrDefault(t => t.Id == id);
            await Task.CompletedTask;
            return item;
        }

        public async Task SaveChangesAsync()
        {
            // No actual persistence needed in InMemory implementation
            await Task.CompletedTask;
        }
    }
}