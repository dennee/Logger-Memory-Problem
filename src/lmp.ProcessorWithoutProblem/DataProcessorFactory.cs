﻿using System;
using lmp.ProcessorWithoutProblem.DataProcessors;
using Ninject;
using Ninject.Parameters;

namespace lmp.ProcessorWithoutProblem
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

            var logFactory = LoggerFactory.Create();

            var stringLengthDataProcessorLogger = logFactory.GetLogger($"{typeof(StringLengthDataProcessor).FullName}-{id}");

            var stringLengthDataProcessor = _kernel.Get<IDataProcessor<string, int>>(
                new ConstructorArgument("logger", stringLengthDataProcessorLogger),
                new ConstructorArgument("id", id));
            var stringDataProcessor =
                _kernel.Get<IDataProcessor<string, string>>(
                    new ConstructorArgument("logFactory", logFactory),
                    new ConstructorArgument("lengthDataProcessor", stringLengthDataProcessor),
                    new ConstructorArgument("id", id));

            return stringDataProcessor;
        }
    }
}