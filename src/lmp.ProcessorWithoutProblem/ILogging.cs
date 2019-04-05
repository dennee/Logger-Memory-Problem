using System;
using NLog;

namespace lmp.ProcessorWithoutProblem
{
    public interface ILogging
    {
        /// <summary>
        /// Creates and manages instances of <see cref="Logger"/>
        /// </summary>
        LogFactory LogFactory { get; }
    }
}
