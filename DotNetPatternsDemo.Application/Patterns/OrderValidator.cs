namespace AdvancedDotNetPatternsDemo.Application.Patterns
{
    // Complex subsystems (simulation only)
    public class OrderValidator
    {
        public bool Validate(Order order)
        {
            Console.WriteLine("Validating order...");
            return true;
        }
    }

    public class OrderRepository
    {
        public void Save(Order order)
        {
            Console.WriteLine("Saving order to database");
        }
    }

    public class PaymentService
    {
        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing payment {amount:C}");
            return true;
        }
    }

    public class NotificationService
    {
        public void SendConfirmation(string customer)
        {
            Console.WriteLine($"Sending confirmation to {customer}");
        }
    }

    // Facade
    public class OrderProcessingFacade
    {
        private readonly OrderValidator _validator = new();
        private readonly OrderRepository _repository = new();
        private readonly PaymentService _payment = new();
        private readonly NotificationService _notification = new();

        public bool ProcessOrder(Order order)
        {
            if (!_validator.Validate(order))
                return false;

            _repository.Save(order);
            bool paymentSuccess = _payment.ProcessPayment(order.TotalAmount);

            if (paymentSuccess)
            {
                _notification.SendConfirmation(order.CustomerName);
                return true;
            }

            return false;
        }
    }
}