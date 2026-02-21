// Example of SRP violation
public class OrderServiceBad
{
    public void CreateOrder(Order order)
    {
        // Save to database
        // Send email
        // Log activity
        // Calculate discount
    }
}

// Example of adhering to SRP
public interface IOrderRepository { /* ... */ }
public interface IEmailService { /* ... */ }
public interface IDiscountCalculator { /* ... */ }

public class OrderServiceGood
{
    private readonly IOrderRepository _repo;
    private readonly IEmailService _email;
    private readonly IDiscountCalculator _discount;

    public OrderServiceGood(IOrderRepository repo, IEmailService email, IDiscountCalculator discount)
    {
        _repo = repo;
        _email = email;
        _discount = discount;
    }

    public void CreateOrder(Order order)
    {
        // Coordination only – other responsibilities are separated
        var discounted = _discount.Apply(order);
        _repo.Save(discounted);
        _email.SendConfirmation(discounted);
    }
}