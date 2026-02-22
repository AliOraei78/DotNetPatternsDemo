// Mediator Pattern
namespace AdvancedDotNetPatternsDemo.Application.Patterns
{
    // Mediator interface
    public interface IChatMediator
    {
        void SendMessage(string message, User user);
    }

    // Concrete Mediator
    public class ChatRoom : IChatMediator
    {
        private readonly List<User> _users = new();

        public void RegisterUser(User user) => _users.Add(user);

        public void SendMessage(string message, User sender)
        {
            foreach (var user in _users)
            {
                if (user != sender)
                {
                    user.Receive(message);
                }
            }
        }
    }

    // Colleague
    public abstract class User
    {
        protected IChatMediator Mediator;
        public string Name { get; }

        protected User(IChatMediator mediator, string name)
        {
            Mediator = mediator;
            Name = name;
        }

        public abstract void Receive(string message);

        public void Send(string message)
        {
            Console.WriteLine($"{Name} sent: {message}");
            Mediator.SendMessage(message, this);
        }
    }

    public class ConcreteUser : User
    {
        public ConcreteUser(IChatMediator mediator, string name)
            : base(mediator, name) { }

        public override void Receive(string message)
        {
            Console.WriteLine($"{Name} received: {message}");
        }
    }
}