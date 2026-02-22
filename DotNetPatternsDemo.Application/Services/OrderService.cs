using AdvancedDotNetPatternsDemo.Application.Patterns;

public class OrderService
{
    private readonly IOrderRepository _repository;
    private readonly ILoggerService _logger;

    // Constructor Injection
    public OrderService(IOrderRepository repository, ILoggerService logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void CreateOrder(Order order)
    {
        _logger.Log($"Creating new order: {order.OrderId}");
        _repository.Save(order);
    }
}