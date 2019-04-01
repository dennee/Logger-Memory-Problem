using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public event EventHandler<int> ProgressUpdated;

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
            var webClient = new WebClient();
            var startPointContent = webClient.DownloadString(startPoint);
            var urls = GetUrls(startPointContent);
            _urls.AddRange(urls);
            var index = 0;
            while(_urls.Count < maxCount)
            {
                var url = _urls[index];
                var html = webClient.DownloadString(url);
                urls = GetUrls(html);
                _urls.AddRange(urls);
                _urls.Distinct();
                index = _urls.Count < (index + 1) ? 0 : index + 1;
                UpdateProgress(_urls.Count, maxCount);
            }
        }

        /// <summary>
        /// Gets urls from HTML page
        /// </summary>
        /// <returns>The urls collection</returns>
        /// <param name="html">The page content</param>
        private ICollection<string> GetUrls(string html)
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
        private void OnProgressUpdated(int progress)
        {
            ProgressUpdated?.Invoke(this, progress);
        }

        /// <summary>
        /// Updates the progress value
        /// </summary>
        /// <param name="currentValue">Current value.</param>
        /// <param name="maxValue">Max value.</param>
        private void UpdateProgress(int currentValue, int maxValue)
        {
            var progress = currentValue / maxValue;
            OnProgressUpdated(progress);
        }
    }
}
