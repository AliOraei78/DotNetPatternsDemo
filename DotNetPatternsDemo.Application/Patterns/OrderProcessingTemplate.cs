// Template Method Pattern
namespace AdvancedDotNetPatternsDemo.Application.Patterns
{
    public abstract class OrderProcessingTemplate
    {
        // Template Method – overall algorithm
        public void ProcessOrder()
        {
            ValidateOrder();
            CalculateTotal();
            ProcessPayment();
            if (IsPaymentSuccessful())
            {
                SaveOrder();
                SendConfirmation();
            }
            else
            {
                HandlePaymentFailure();
            }
        }

        protected abstract void ValidateOrder();
        protected abstract void CalculateTotal();
        protected abstract void ProcessPayment();
        protected abstract bool IsPaymentSuccessful();
        protected abstract void SaveOrder();
        protected abstract void SendConfirmation();

        protected virtual void HandlePaymentFailure()
        {
            Console.WriteLine("Payment was unsuccessful. Please try again.");
        }
    }

    public class OnlineOrderProcessing : OrderProcessingTemplate
    {
        protected override void ValidateOrder()
            => Console.WriteLine("Validating online order...");

        protected override void CalculateTotal()
            => Console.WriteLine("Calculating total with tax and shipping...");

        protected override void ProcessPayment()
            => Console.WriteLine("Processing online payment (payment gateway)...");

        protected override bool IsPaymentSuccessful()
            => true; // simulation

        protected override void SaveOrder()
            => Console.WriteLine("Saving order to the database...");

        protected override void SendConfirmation()
            => Console.WriteLine("Sending order confirmation email...");
    }
}