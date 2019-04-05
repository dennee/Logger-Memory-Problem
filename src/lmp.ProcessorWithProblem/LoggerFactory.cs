using NLog;

namespace lmp.ProcessorWithProblem
{
    /// <summary>
    /// Factory for loggers creation
    /// </summary>
    public static class LoggerFactory
    {
        /// <summary>
        /// Creates a logger
        /// </summary>
        /// <returns>A created logger</returns>
        public static ILogger Create<T>() => Create(typeof(T).FullName);

        /// <summary>
        /// Create a logger
        /// </summary>
        /// <returns>A created logger</returns>
        /// <param name="name">Name of logger</param>
        public static ILogger Create(string name) => LogManager.GetLogger(name);
    }
}