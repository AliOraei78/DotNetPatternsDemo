using System;

namespace AdvancedDotNetPatternsDemo.Application.Patterns
{
    /// <summary>
    /// Modern Singleton implementation using Lazy<T> (thread-safe and lazy-loaded)
    /// </summary>
    public sealed class LoggerSingleton
    {
        // Lazy initialization with built-in thread-safety
        private static readonly Lazy<LoggerSingleton> _lazyInstance =
            new Lazy<LoggerSingleton>(() => new LoggerSingleton());

        // Public access point
        public static LoggerSingleton Instance => _lazyInstance.Value;

        // Private constructor to prevent direct instantiation
        private LoggerSingleton()
        {
            Console.WriteLine("LoggerSingleton created - only once");
        }

        public void Log(string message)
        {
            Console.WriteLine($"[LOG {DateTime.Now:HH:mm:ss}] {message}");
        }
    }
}