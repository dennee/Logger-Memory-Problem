using System;
using lmp.ProcessorWithProblem.DataProcessors;
using Ninject;
using Ninject.Parameters;

namespace lmp.ProcessorWithProblem
{
    /// <summary>
    /// Factory for creation data processors
    /// </summary>
    public class DataProcessorFactory
    {
        /// <summary>
        /// Creates a data processor
        /// </summary>
        /// <returns>A data processor</returns>
        public IDataProcessor<string, string> CreateStringDataProcessor(IKernel _kernel)
        {
            var id = Guid.NewGuid().ToString("N");

            var stringLengthDataProcessorLogger =
                LoggerFactory.Create($"{typeof(StringLengthDataProcessor).FullName}-{id}");
            var stringDataProcessorLogger = LoggerFactory.Create($"{typeof(StringDataProcessor).FullName}-{id}");

            var stringLengthDataProcessor =
                _kernel.Get<IDataProcessor<string, int>>(new ConstructorArgument("logger",
                    stringLengthDataProcessorLogger));
            var stringDataProcessor =
                _kernel.Get<IDataProcessor<string, string>>(
                    new ConstructorArgument("logger", stringDataProcessorLogger),
                    new ConstructorArgument("lengthDataProcessor", stringLengthDataProcessor));

            return stringDataProcessor;
        }
    }
}