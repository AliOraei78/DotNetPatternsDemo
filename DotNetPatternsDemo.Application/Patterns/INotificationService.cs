namespace AdvancedDotNetPatternsDemo.Application.Patterns
{
    // Component
    public interface INotificationService
    {
        void Send(string recipient, string message);
    }

    // Concrete Component
    public class BasicNotificationService : INotificationService
    {
        public void Send(string recipient, string message)
        {
            Console.WriteLine($"Basic send: to {recipient} → {message}");
        }
    }

    // Abstract Decorator
    public abstract class NotificationDecorator : INotificationService
    {
        protected readonly INotificationService _wrappedService;

        protected NotificationDecorator(INotificationService wrappedService)
        {
            _wrappedService = wrappedService ?? throw new ArgumentNullException(nameof(wrappedService));
        }

        public virtual void Send(string recipient, string message)
        {
            _wrappedService.Send(recipient, message);
        }
    }

    // Concrete Decorators
    public class LoggingNotificationDecorator : NotificationDecorator
    {
        public LoggingNotificationDecorator(INotificationService wrapped) : base(wrapped) { }

        public override void Send(string recipient, string message)
        {
            Console.WriteLine($"[LOG] Starting send to {recipient} at {DateTime.Now:HH:mm:ss}");
            base.Send(recipient, message);
            Console.WriteLine($"[LOG] Send completed");
        }
    }

    public class TimestampNotificationDecorator : NotificationDecorator
    {
        public TimestampNotificationDecorator(INotificationService wrapped) : base(wrapped) { }

        public override void Send(string recipient, string message)
        {
            string enhancedMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
            base.Send(recipient, enhancedMessage);
        }
    }
}