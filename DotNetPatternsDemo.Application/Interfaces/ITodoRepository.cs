// ITodoRepository.cs (for Command – write side)
using AdvancedDotNetPatternsDemo.Domain.Entities;
using AdvancedDotNetPatternsDemo.Application.Features.Todo.Dtos;
public interface ITodoRepository
{
    Task AddAsync(TodoItem todo);
    Task<TodoItem?> GetByIdAsync(Guid id);
    Task SaveChangesAsync();
}

// ITodoReadRepository.cs (for Query – read side – can be separated)
public interface ITodoReadRepository
{
    Task<TodoDto?> GetByIdAsync(Guid id);
    Task<List<TodoDto>> GetAllAsync();
}