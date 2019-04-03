using Newtonsoft.Json;
using NLog;

namespace lmp.UrlsProcessorWithProblem.DataProcessors
{
    /// <summary>
    /// Implementation of <see cref="IDataProcessor{TData, TResponse}"/> for processing a string data
    /// </summary>
    public class StringDataProcessor : IDataProcessor<string, string>
    {
        private readonly ILogger _logger;
        private readonly IDataProcessor<string, int> _lengthDataProcessor;

        /// <summary>
        /// Initializes an instance of <see cref="StringDataProcessor"/> class
        /// </summary>
        /// <param name="logger">Logger</param>
        public StringDataProcessor(ILogger logger, IDataProcessor<string, int> lengthDataProcessor)
        {
            _logger = logger;
            _lengthDataProcessor = lengthDataProcessor;
        }

        /// <summary>
        /// Processes the data
        /// </summary>
        /// <param name="data">A processing data</param>
        /// <returns>Processed data</returns>
        public string Process(string data)
        {
            _logger.Info("Try to process a data");
            var length = _lengthDataProcessor.Process(data);
            _logger.Info("A data processed");

            return JsonConvert.SerializeObject(new {length = length});
        }
    }
}