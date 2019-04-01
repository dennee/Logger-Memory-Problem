using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using lmp.UrlCollector.UrlCollector;

namespace lmp.UrlCollector
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var parsedArgs = ParseArgs(args);
            Console.WriteLine("The application started with params:");
            foreach(var keyValuePair in parsedArgs)
            {
                Console.WriteLine($"--{keyValuePair.Key}={keyValuePair.Value}");
            }

            var urlCollector = new UrlsCollector();
            urlCollector.ProgressUpdated += UrlCollectorOnProgressUpdated;
            urlCollector.Collect(parsedArgs["url"], int.Parse(parsedArgs["count"]));

            Console.WriteLine("Finished");

            foreach (var url in urlCollector.Urls)
            {
                Console.WriteLine(url);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Parses the arguments collection <paramref name="args"/>
        /// </summary>
        /// <returns>Parsed arguments</returns>
        /// <param name="args">The arguments collection</param>
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
                if(Regex.IsMatch(arg, argumentPattern))
                {
                    var match = Regex.Match(arg, argumentPattern);
                    parsedArgs.TryAdd(match.Groups[1].ToString(), match.Groups[2].ToString());
                }
            }

            return parsedArgs;
        }

        /// <summary>
        /// Handles the progress updating event
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="progress">Current progress</param>
        private static void UrlCollectorOnProgressUpdated(object sender, ProgressUpdatedEventArgs progress)
        {
            DisplayProgress(progress);
        }

        /// <summary>
        /// Displays the progress on the screen
        /// </summary>
        /// <param name="progress">Current progress</param>
        private static void DisplayProgress(ProgressUpdatedEventArgs progress)
        {
            var positionY = Console.CursorTop;

            Console.SetCursorPosition(0, positionY);
            Console.Write($"{progress.Progress}% {progress.CurrentValue}/{progress.MaxValue}");
        }
    }
}
