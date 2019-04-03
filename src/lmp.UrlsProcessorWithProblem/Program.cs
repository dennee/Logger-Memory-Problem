using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using NLog;
using NLog.Config;

namespace lmp.UrlsProcessorWithProblem
{
    class Program
    {
        private static IKernel _kernel;

        static void Main(string[] args)
        {
            LogManager.Configuration = new XmlLoggingConfiguration("NLog.config");

            var logger = LoggerFactory.Create<Program>();

            _kernel = new StandardKernel(new IocModule());

            logger.Info("Processor started");

            Console.ReadKey();
        }
    }
}
