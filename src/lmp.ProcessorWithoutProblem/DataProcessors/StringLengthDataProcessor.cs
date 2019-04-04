using System;
using NLog;

namespace lmp.UrlsProcessorWithProblem.DataProcessors
{
    /// <summary>
    /// Implementation of <see cref="IDataProcessor{TData,TResponse}"/> for getting string length
    /// </summary>
    public class StringLengthDataProcessor : IDataProcessor<string, int>
    {
        /// <summary>
        /// The ID of processor
        /// </summary>
        public string Id => _id;

        private readonly ILogger _logger;
        private readonly string _id;

        /// <summary>
        /// Initializes an instance of <see cref="StringLengthDataProcessor"/> class
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="id">Processor ID</param>
        public StringLengthDataProcessor(ILogger logger, string id)
        {
            _logger = logger;
            _id = id;
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

        public void Dispose()
        {

        }
    }
}