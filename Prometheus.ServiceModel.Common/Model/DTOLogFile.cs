namespace Prometheus.ServiceModel.Common.Model
{
    /// <summary>
    ///     Meta data object with information about a log file.
    /// </summary>
    public class DTOLogFile
    {
        /// <summary>
        ///     Gets the name of the log file.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets the last modification timestamp for the file.
        /// </summary>
        public System.DateTime LastUpdateUtc { get; set; }
    }
}