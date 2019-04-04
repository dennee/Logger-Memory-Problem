using System;

namespace lmp.UrlsProcessorWithProblem.DataProcessors
{
    /// <summary>
    /// Describes a data processor
    /// </summary>
    /// <typeparam name="TData">Type of processing data</typeparam>
    /// <typeparam name="TResponse">Type of processing response</typeparam>
    public interface IDataProcessor<TData, TResponse> : IDisposable
    {
        /// <summary>
        /// The ID of processor
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Processes the data
        /// </summary>
        /// <param name="data">A processing data</param>
        /// <returns>Processed data</returns>
        TResponse Process(TData data);
    }
}