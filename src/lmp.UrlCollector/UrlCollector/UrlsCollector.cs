using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace lmp.UrlCollector.UrlCollector
{
    /// <summary>
    /// The implementation of <see cref="IUrlsCollector"/>
    /// </summary>
    public class UrlsCollector : IUrlsCollector
    {
        /// <summary>
        /// Collected urls
        /// </summary>
        public ICollection<string> Urls => _urls;

        public event EventHandler<ProgressUpdatedEventArgs> ProgressUpdated;

        private List<string> _urls;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:lmp.UrlCollector.UrlCollector.UrlsCollector"/> class.
        /// </summary>
        public UrlsCollector()
        {
            _urls = new List<string>();
        }

        /// <summary>
        /// Collects the urls from <paramref name="startPoint"/>
        /// </summary>
        /// <param name="startPoint">The start point of collection</param>
        /// <param name="maxCount">Max collection items</param>
        public void Collect(string startPoint, int maxCount)
        {
            GetUrlsCollection(startPoint);
            var index = 0;
            while (_urls.Count < maxCount)
            {
                var url = _urls[index];
                GetUrlsCollection(url);
                _urls = _urls.Distinct().ToList();
                if (_urls.Count < (index + 1))
                {
                    break;
                }
                index = index + 1;
                UpdateProgress(_urls.Count, maxCount);
            }
        }

        /// <summary>
        /// Gets the urls
        /// </summary>
        /// <param name="url">The start point</param>
        private void GetUrlsCollection(string url)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    Debug.WriteLine($"Start processing: {url}");
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();
                    var response = httpClient.GetAsync(url).GetAwaiter().GetResult();
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.Content.Headers.ContentType.MediaType.Contains("text"))
                        {
                            var html = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                            var urls = GetUrlsValues(html);
                            _urls.AddRange(urls);
                            _urls = _urls.Distinct().ToList();
                            _urls.RemoveAll(string.IsNullOrWhiteSpace);
                        }
                    }
                    stopwatch.Stop();
                    Debug.WriteLine($"Url: {url} Processed time: {stopwatch.ElapsedMilliseconds}ms");
                }
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// Gets urls from HTML page
        /// </summary>
        /// <returns>The urls collection</returns>
        /// <param name="html">The page content</param>
        private ICollection<string> GetUrlsValues(string html)
        {
            var urls = new List<string>();
            var urlTagPattern = "<a(.*?)>(.*?)</a>";
            var urlAddressPattern = "href=\"(((http)|(https))://(.*?))\"";
            var tagsRegex = new Regex(urlTagPattern, RegexOptions.Singleline);
            var addressRegex = new Regex(urlAddressPattern, RegexOptions.Singleline);
            var tags = tagsRegex.Matches(html);
            foreach (Match tag in tags)
            {
                var tagAttributes = tag.Groups[1].ToString();
                var url = addressRegex.Match(tagAttributes).Groups[1].ToString();
                urls.Add(url.Trim());
            }
            return urls;
        }

        /// <summary>
        /// Ons the progress updated.
        /// </summary>
        /// <param name="progress">Updated progress value</param>
        /// <param name="currentValue">Current value.</param>
        /// <param name="maxValue">Max value.</param>
        private void OnProgressUpdated(int progress, int currentValue, int maxValue)
        {
            ProgressUpdated?.Invoke(this, new ProgressUpdatedEventArgs(progress, currentValue, maxValue));
        }

        /// <summary>
        /// Updates the progress value
        /// </summary>
        /// <param name="currentValue">Current value.</param>
        /// <param name="maxValue">Max value.</param>
        private void UpdateProgress(int currentValue, int maxValue)
        {
            var progress = (int)(((double)currentValue / (double)maxValue) * 100);
            OnProgressUpdated(progress, currentValue, maxValue);
        }
    }
}
