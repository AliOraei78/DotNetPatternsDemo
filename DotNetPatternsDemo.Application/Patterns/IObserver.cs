// Observer Pattern
namespace AdvancedDotNetPatternsDemo.Application.Patterns
{
    public interface IObserver
    {
        void Update(string message);
    }

    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }

    public class NewsPublisher : ISubject
    {
        private readonly List<IObserver> _observers = new();
        public string LatestNews { get; private set; } = "";

        public void Attach(IObserver observer) => _observers.Add(observer);
        public void Detach(IObserver observer) => _observers.Remove(observer);

        public void PublishNews(string news)
        {
            LatestNews = news;
            Console.WriteLine($"New news published: {news}");
            Notify();
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(LatestNews);
            }
        }
    }

    public class NewsSubscriber : IObserver
    {
        public string Name { get; }

        public NewsSubscriber(string name) => Name = name;

        public void Update(string message)
        {
            Console.WriteLine($"{Name} was notified: {message}");
        }
    }
}