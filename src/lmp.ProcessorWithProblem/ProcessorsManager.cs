using System.Collections.Generic;
using System.Linq;
using Ninject;
using NLog;

namespace lmp.UrlsProcessorWithProblem
{
    /// <summary>
    /// Manages of processors
    /// </summary>
    public class ProcessorsManager
    {
        private readonly ILogger _logger;
        private readonly DataProcessorFactory _factory;
        private readonly IKernel _kernel;

        private Queue<string> _queue;

        /// <summary>
        /// Initializes an instance of <see cref="ProcessorsManager"/> class
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="factory">A factory for creation of data processors</param>
        /// <param name="kernel">The Ninject kernel</param>
        public ProcessorsManager(ILogger logger, DataProcessorFactory factory, IKernel kernel)
        {
            _logger = logger;
            _factory = factory;
            _kernel = kernel;
            _queue = new Queue<string>();
        }

        /// <summary>
        /// Uploads data for processing
        /// </summary>
        /// <param name="data">A data collection</param>
        public void UploadData(ICollection<string> data)
        {
            _logger.Info("Try to upload data");
            _queue = new Queue<string>(data);
            _logger.Info("Data uploaded");
        }

        /// <summary>
        /// Processes a data collection
        /// </summary>
        public void Process()
        {
            _logger.Info("Try to process a data collection");
            while (_queue.Any())
            {
                var data = _queue.Dequeue();
                var processor = _factory.CreateStringDataProcessor(_kernel);
                var response = processor.Process(data);
            }
            _logger.Info("A data collection processed");
        }
    }
}