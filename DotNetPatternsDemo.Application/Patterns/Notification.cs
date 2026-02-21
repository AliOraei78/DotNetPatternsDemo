namespace AdvancedDotNetPatternsDemo.Application.Patterns
{
    // Product interface
    public interface INotification
    {
        void Send(string recipient, string message);
    }

    // Concrete Products
    public class EmailNotification : INotification
    {
        public void Send(string recipient, string message)
        {
            Console.WriteLine($"Email to {recipient}: {message}");
        }
    }

    public class SmsNotification : INotification
    {
        public void Send(string recipient, string message)
        {
            Console.WriteLine($"SMS to {recipient}: {message}");
        }
    }
}