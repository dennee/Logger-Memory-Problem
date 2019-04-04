using NLog;
using NLog.Config;

namespace lmp.UrlsProcessorWithProblem
{
    /// <summary>
    /// Factory for loggers creation
    /// </summary>
    public static class LoggerFactory
    {
        private static LoggingConfiguration _configuration;

        /// <summary>
        /// Uploads the loggin configuration
        /// </summary>
        /// <param name="configuration">Logging configuration</param>
        public static void UploadConfiguration(LoggingConfiguration configuration) => _configuration = configuration;

        /// <summary>
        /// Creates a log factory
        /// </summary>
        /// <returns>A created log factory</returns>
        public static LogFactory Create() => new LogFactory(_configuration);
    }
}