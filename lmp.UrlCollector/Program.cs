using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using lmp.UrlCollector.UrlCollector;

namespace lmp.UrlCollector
{
    class MainClass
    {
        /// <summary>
        /// The name of file with urls
        /// </summary>
        private const string FILE_NAME = "urls.txt";

        public static void Main(string[] args)
        {
            var parsedArgs = ParseArgs(args);
            Console.WriteLine("The application started with params:");
            foreach(var keyValuePair in parsedArgs)
            {
                Console.WriteLine($"--{keyValuePair.Key}={keyValuePair.Value}");
            }

            Console.WriteLine($"Collection started: {DateTime.UtcNow.ToString("dd.MM.yyyy hh:mm:ss")}");

            var urlCollector = new UrlsCollector();
            urlCollector.ProgressUpdated += UrlCollectorOnProgressUpdated;
            urlCollector.Collect(parsedArgs["url"], int.Parse(parsedArgs["count"]));

            Console.WriteLine();
            Console.WriteLine($"Collection finished: {DateTime.UtcNow.ToString("dd.MM.yyyy hh:mm:ss")}");

            Console.WriteLine("Try to write found urls to file");

            WriteFile(urlCollector.Urls);

            Console.WriteLine("File was wrote successfully");

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

        /// <summary>
        /// Writes the <paramref name="urls"/> to file
        /// </summary>
        /// <param name="urls">Collected urls</param>
        private static void WriteFile(ICollection<string> urls)
        {
            var content = string.Join("\r\n", urls);
            using (var stream = File.Open(FILE_NAME, FileMode.Create, FileAccess.Write))
            {
                var buffer = Encoding.UTF8.GetBytes(content);
                stream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
