// TodoDto.cs
namespace AdvancedDotNetPatternsDemo.Application.Features.Todo.Dtos
{
    public record TodoDto(
        Guid Id,
        string Title,
        string? Description,
        bool IsCompleted,
        DateTime CreatedAt,
        DateTime? DueDate);
}