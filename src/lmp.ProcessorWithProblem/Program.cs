using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using lmp.UrlsProcessorWithProblem.FakeDataGenerator;
using Ninject;
using Ninject.Parameters;
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
            try
            {
                var count = int.Parse(GetArgValue(args, "count"));

                _kernel = new StandardKernel(new IocModule());

                var processorsManager = _kernel.Get<ProcessorsManager>(new ConstructorArgument("kernel", _kernel));
                var dataGenerator = _kernel.Get<IFakeDataGenerator<string>>();

                logger.Info("Processing started");

                var data = dataGenerator.Generate(count);

                processorsManager.UploadData(data);
                processorsManager.Process();
                logger.Info("Processing finished");

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, ex.Message);
            }
        }

        /// <summary>
        /// Gets an argument value from <paramref name="args"/>
        /// </summary>
        /// <param name="args">Arguments collection</param>
        /// <param name="key">An argument key</param>
        /// <returns>An argument value</returns>
        private static string GetArgValue(string[] args, string key)
        {
            var parsedArgs = ParseArgs(args);

            return parsedArgs[key];
        }

        /// <summary>
        /// Parses arguments collection to dictionary
        /// </summary>
        /// <param name="args">Arguments collection</param>
        /// <returns>Parsed arguments</returns>
        private static IDictionary<string, string> ParseArgs(string[] args)
        {
            var parsedArgs = new ConcurrentDictionary<string, string>();

            if (args.Length == 0)
            {
                throw new ArgumentException("The arguments collection is empty");
            }

            var argumentPattern = "--([a-z]+)=(.*?)(\\s|$)";

            foreach (var arg in args)
            {
                if (Regex.IsMatch(arg, argumentPattern))
                {
                    var match = Regex.Match(arg, argumentPattern);
                    parsedArgs.TryAdd(match.Groups[1].ToString(), match.Groups[2].ToString());
                }
            }

            return parsedArgs;
        }
    }
}
