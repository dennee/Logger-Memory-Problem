using NLog;

namespace lmp.ProcessorWithProblem.DataProcessors
{
    /// <summary>
    /// Implementation of <see cref="IDataProcessor{TData,TResponse}"/> for getting string length
    /// </summary>
    public class StringLengthDataProcessor : IDataProcessor<string, int>
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes an instance of <see cref="StringLengthDataProcessor"/> class
        /// </summary>
        /// <param name="logger">Logger</param>
        public StringLengthDataProcessor(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Processes the data
        /// </summary>
        /// <param name="data">A processing data</param>
        /// <returns>Processed data</returns>
        public int Process(string data)
        {
            _logger.Info("Try to get string length");

            return data.Length;
        }
    }
}