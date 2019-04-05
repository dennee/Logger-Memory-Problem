using System;
using System.Collections.Generic;
using NLog;

namespace lmp.ProcessorWithProblem.FakeDataGenerator
{
    /// <summary>
    /// Implementation of <see cref="IFakeDataGenerator{T}"/>
    /// </summary>
    public class StringFakeDataGenerator : IFakeDataGenerator<string>
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initilizes an instance of <see cref="StringFakeDataGenerator"/> class
        /// </summary>
        /// <param name="logger">Logger</param>
        public StringFakeDataGenerator(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Generates fake data collection
        /// </summary>
        /// <param name="count">Count of fake data items</param>
        /// <returns>Generated data</returns>
        public ICollection<string> Generate(int count)
        {
            _logger.Info("Try to generate a fake data collection");
            var data = new List<string>();
            for (int i = 0; i < count; i++)
            {
                data.Add($"fake data {i}");
            }
            _logger.Info("A fake data collection generated");

            return data;
        }
    }
}