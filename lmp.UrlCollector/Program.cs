using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
    }
}
