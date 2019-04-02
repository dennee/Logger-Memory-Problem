using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.Config;

namespace lmp.UrlsProcessorWithProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            LogManager.Configuration = new XmlLoggingConfiguration("NLog.config");

            var logger = LoggerFactory.Create<Program>();

            logger.Info("Processor started");

            Console.ReadKey();
        }
    }
}
