using System;
using System.Collections.Generic;

namespace lmp.UrlCollector.UrlCollector
{
    /// <summary>
    /// Describes the urls collector
    /// </summary>
    public interface IUrlsCollector
    {
        /// <summary>
        /// Collected urls
        /// </summary>
        ICollection<string> Urls { get; }

        /// <summary>
        /// Occurs when progress updated.
        /// </summary>
        event EventHandler<ProgressUpdatedEventArgs> ProgressUpdated;

        /// <summary>
        /// Collects the urls from <paramref name="startPoint"/>
        /// </summary>
        /// <param name="startPoint">The start point of collection</param>
        /// <param name="maxCount">Max collection items</param>
        void Collect(string startPoint, int maxCount);
    }
}
