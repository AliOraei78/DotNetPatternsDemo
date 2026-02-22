// Factory Method Pattern
namespace AdvancedDotNetPatternsDemo.Application.Patterns
{
    // Creator – base class with Factory Method
    public abstract class NotificationFactory
    {
        // Factory Method – implemented by subclasses
        protected abstract INotification CreateNotification();

        // Core operation that uses the Factory Method
        public void SendNotification(string recipient, string message)
        {
            var notification = CreateNotification();
            notification.Send(recipient, message);
        }
    }

    // Concrete Creators
    public class EmailNotificationFactory : NotificationFactory
    {
        protected override INotification CreateNotification()
        {
            return new EmailNotification();
        }
    }

    public class SmsNotificationFactory : NotificationFactory
    {
        protected override INotification CreateNotification()
        {
            return new SmsNotification();
        }
    }
}