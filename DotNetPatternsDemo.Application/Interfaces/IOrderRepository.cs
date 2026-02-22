// Interfaces
using AdvancedDotNetPatternsDemo.Application.Patterns;

public interface IOrderRepository
{
    void Save(Order order);
}

public interface ILoggerService
{
    void Log(string message);
}