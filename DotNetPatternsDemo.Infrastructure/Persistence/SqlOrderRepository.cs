// Concrete implementations
using AdvancedDotNetPatternsDemo.Application.Patterns;

public class SqlOrderRepository : IOrderRepository
{
    public void Save(Order order)
    {
        Console.WriteLine($"Saving order {order.OrderId} to SQL");
    }
}

public class ConsoleLogger : ILoggerService
{
    public void Log(string message)
    {
        Console.WriteLine($"[LOG] {message}");
    }
}