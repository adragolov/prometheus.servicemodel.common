namespace Prometheus.ServiceModel.Common.Model
{
    /// <summary>
    ///     Data transfer object model that carries public information about a microservice.
    /// </summary>
    public class DTOPublicServiceInfo
    {
        /// <summary>
        ///     Gets the unique identifier of the service instance.
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        ///     Gets the public facing service name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets the description for the service.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets the version string for the service.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        ///     Gets or sets the deployment environment name for the service, such as Development, Staging, Production.
        /// </summary>
        public string EnvironmentName { get; set; }
        
        /// <summary>
        ///     Gets an indication, if the service host has performed a bootstrap.
        /// </summary>
        public bool HasBootstrapped { set; get; }

        /// <summary>
        ///     Gets an indication, if the service host is running.
        /// </summary>
        public bool IsRunning { get; set; }

        /// <summary>
        ///     Stores the timestamp for the next iteration of the service host.
        /// </summary>
        public System.DateTime? NextIterationAt { get; set; }

        /// <summary>
        ///     Gets the collection of available host bindings.
        /// </summary>
        public System.Collections.Generic.IEnumerable<string> Bindings { get; set; }
    }
}