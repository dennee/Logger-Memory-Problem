using System;
using Newtonsoft.Json;
using NLog;

namespace lmp.UrlsProcessorWithProblem.DataProcessors
{
    /// <summary>
    /// Implementation of <see cref="IDataProcessor{TData, TResponse}"/> for processing a string data
    /// </summary>
    public class StringDataProcessor : IDataProcessor<string, string>, ILogged
    {
        /// <summary>
        /// The ID of processor
        /// </summary>
        public string Id => _id;

        /// <summary>
        /// Creates and manages instances of <see cref="T:NLog.Logger"/>
        /// </summary>
        public LogFactory LogFactory { get; }

        private readonly ILogger _logger;
        private readonly IDataProcessor<string, int> _lengthDataProcessor;

        private readonly string _id;

        /// <summary>
        /// Initializes an instance of <see cref="StringDataProcessor"/> class
        /// </summary>
        /// <param name="logFactory">Creates and manages instance of <see cref="Logger"/></param>
        /// <param name="id">Processor ID</param>
        public StringDataProcessor(LogFactory logFactory, IDataProcessor<string, int> lengthDataProcessor, string id)
        {
            LogFactory = logFactory;
            _lengthDataProcessor = lengthDataProcessor;
            _id = id;
            _logger = LogFactory.GetLogger($"{typeof(StringDataProcessor).FullName}-{_id}");
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

        public void Dispose()
        {
            LogFactory.Dispose();
        }
    }
}