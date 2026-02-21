namespace AdvancedDotNetPatternsDemo.Application.Patterns
{
    // Target interface (expected by the new system)
    public interface IPaymentProcessor
    {
        bool ProcessPayment(decimal amount, string cardNumber);
    }

    // Adaptee (legacy/external system)
    public class LegacyPaymentGateway
    {
        public string ExecuteTransaction(decimal value, string card)
        {
            // Simulate legacy processing
            Console.WriteLine($"Legacy Gateway: Processing transaction {value:C} with card {card}");
            return "SUCCESS"; // or "FAIL"
        }
    }

    // Adapter
    public class LegacyPaymentAdapter : IPaymentProcessor
    {
        private readonly LegacyPaymentGateway _legacyGateway;

        public LegacyPaymentAdapter(LegacyPaymentGateway legacyGateway)
        {
            _legacyGateway = legacyGateway ?? throw new ArgumentNullException(nameof(legacyGateway));
        }

        public bool ProcessPayment(decimal amount, string cardNumber)
        {
            var result = _legacyGateway.ExecuteTransaction(amount, cardNumber);
            return result == "SUCCESS";
        }
    }
}