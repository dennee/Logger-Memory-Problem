namespace lmp.UrlCollector.UrlCollector
{
    public class ProgressUpdatedEventArgs
    {
        /// <summary>
        /// Current progress
        /// </summary>
        public int Progress { get; }

        /// <summary>
        /// Current items number
        /// </summary>
        public int CurrentValue { get; }

        /// <summary>
        /// Max items
        /// </summary>
        public int MaxValue { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="ProgressUpdatedEventArgs"/> class
        /// </summary>
        /// <param name="progress">Current progress</param>
        /// <param name="currentValue">Current progress value</param>
        /// <param name="maxValue">Max items</param>
        public ProgressUpdatedEventArgs(int progress, int currentValue, int maxValue)
        {
            Progress = progress;
            CurrentValue = currentValue;
            MaxValue = maxValue;
        }
    }
}