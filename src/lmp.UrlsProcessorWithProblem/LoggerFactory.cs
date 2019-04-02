using NLog;

namespace lmp.UrlsProcessorWithProblem
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
        public static ILogger Create<T>()
        {
            return LogManager.GetLogger(typeof(T).FullName);
        }
    }
}