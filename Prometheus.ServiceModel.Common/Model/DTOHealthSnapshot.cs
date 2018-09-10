using System.Collections;

namespace Prometheus.ServiceModel.Common.Model
{
    /// <summary>
    ///     Represents a data transfer object model for a service health snapshot.
    /// </summary>
    public class DTOHealthSnapshot
    {
        /// <summary>
        ///     Gets or sets the environment name, such as Development, Staging, Production.
        /// </summary>
        public string EnvironmentName { get; set; }

        /// <summary>
        ///     Gets or sets the user friendly text of the application host.
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        ///    Gets a value that indicates whether the current application domain is being unloaded
        //     or the common language runtime (CLR) is shutting down.
        /// </summary>
        public bool HasShutdownStarted { get; set; }

        /// <summary>
        ///     Gets the NetBIOS name of this local computer.
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        ///     Gets a unique identifier for the current managed thread.
        /// </summary>
        public int CurrentManagedThreadId { get; set; }

        /// <summary>
        ///     Gets the fully qualified path of the current working directory.
        /// </summary>
        public string CurrentDirectory { get; set; }

        /// <summary>
        ///     Gets the UI string representing the information about the underlying OS system & its version.
        /// </summary>
        public string OSVersion { get; set; }

        /// <summary>
        ///    Gets the number of processors on the current machine.
        ///    The 32-bit signed integer that specifies the number of processors on the current
        //     machine. There is no default. If the current machine contains multiple processor
        //     groups, this property returns the number of logical processors that are available
        //     for use by the common language runtime (CLR).
        /// </summary>
        public int ProcessorCount { get; set; }

        /// <summary>
        ///     Determines whether the current process is a 64-bit process.
        /// </summary>
        public bool Is64BitProcess { get; set; }

        /// <summary>
        ///     Gets the number of bytes in the operating system's memory page.
        /// </summary>
        public int SystemPageSize { get; }

        /// <summary>
        ///    Gets a value indicating whether the current process is running in user interactive
        //     mode.
        /// </summary>
        public bool UserInteractive { get; set; }

        /// <summary>
        ///     Gets the user name of the person who is currently logged on to the Windows operating
        //      system.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     Gets the network domain name associated with the current user.
        /// </summary>
        public string UserDomainName { get; set; }

        /// <summary>
        ///     Gets the running version of the CLR runtime.
        /// </summary>
        public string CLRVersion { get; set; }

        /// <summary>
        ///     Gets the amount of physical memory mapped to the process context.
        /// </summary>
        public long WorkingSet { get; set; }

        /// <summary>
        ///     Gets the fully qualified path of the system directory.
        /// </summary>
        public string SystemDirectory { get; set; }

        /// <summary>
        ///     Determines whether the current operating system is a 64-bit operating system.
        /// </summary>
        public bool Is64BitOperatingSystem { get; set; }

        /// <summary>
        ///     Gets the collection of environment variables available at runtime.
        /// </summary>
        public IDictionary EnvironmentVars { get; set; }

        /// <summary>
        ///     Gets the command line arguments of the executable process.
        /// </summary>
        public string[] CommandLineArgs { get; set; }

        /// <summary>
        ///     Gets the total processor time for this process.
        /// </summary>
        public System.TimeSpan TotalCPUTime { get; set; }

        /// <summary>
        ///     Gets the user processor time for this process.
        /// </summary>
        public System.TimeSpan UserCPUTime { get; set; }

        /// <summary>
        ///     Gets the number of threads that are running in the associated process.
        /// </summary>
        public int ThreadCount { get; set; }
    }
}
