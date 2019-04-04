using System;
using NLog;

namespace lmp.UrlsProcessorWithProblem.DataProcessors
{
    public interface ILogged
    {
        /// <summary>
        /// Creates and manages instances of <see cref="Logger"/>
        /// </summary>
        LogFactory LogFactory { get; }
    }
}
