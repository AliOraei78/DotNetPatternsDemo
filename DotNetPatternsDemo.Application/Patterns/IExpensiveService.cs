namespace AdvancedDotNetPatternsDemo.Application.Patterns
{
    public interface IExpensiveService
    {
        string GetData(string query);
    }

    public class RealExpensiveService : IExpensiveService
    {
        public string GetData(string query)
        {
            Console.WriteLine($"Actual request to server for: {query}");
            Thread.Sleep(2000); // simulate delay
            return $"Data received for {query}";
        }
    }

    public class CachingProxy : IExpensiveService
    {
        private readonly IExpensiveService _realService;
        private readonly Dictionary<string, string> _cache = new();

        public CachingProxy(IExpensiveService realService)
        {
            _realService = realService;
        }

        public string GetData(string query)
        {
            if (_cache.TryGetValue(query, out var cached))
            {
                Console.WriteLine($"Retrieved from cache: {query}");
                return cached;
            }

            var result = _realService.GetData(query);
            _cache[query] = result;
            return result;
        }
    }
}