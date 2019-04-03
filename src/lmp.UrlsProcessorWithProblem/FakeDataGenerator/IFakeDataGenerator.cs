using System.Collections;
using System.Collections.Generic;

namespace lmp.UrlsProcessorWithProblem.FakeDataGenerator
{
    /// <summary>
    /// Describes fake data generator
    /// </summary>
    /// <typeparam name="T">Type of generated data</typeparam>
    public interface IFakeDataGenerator<T>
    {
        /// <summary>
        /// Generates fake data collection
        /// </summary>
        /// <param name="count">Count of fake data items</param>
        /// <returns>Generated data</returns>
        ICollection<T> Generate(int count);
    }
}