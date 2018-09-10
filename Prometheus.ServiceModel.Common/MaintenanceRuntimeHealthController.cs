using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Prometheus.Core.Model;
using System;
using System.Diagnostics;

namespace Prometheus.ServiceModel.Common
{
    /// <summary>
    ///     Maintenance controller with endpoints for inspecting the service host health at runtime.
    ///     Requires priviledges for with claims specific for a Maintenance Team.
    /// </summary>
    [Authorize(AuthenticationSchemes = "Bearer", Policy = "RequireMaintenanceTeam")]
    public class MaintenanceRuntimeHealthController : Controller
    {
        /// <summary>
        ///     Gets a reference to the hosting environment meta data service.
        /// </summary>
        public IHostingEnvironment HostingEnvironment { get; protected set; }

        /// <summary>
        ///     Gets a reference to the service host reference.
        /// </summary>
        public IServiceHost ServiceHost { get; protected set; }
        
        /// <summary>
        ///     Creates a new instance of the controller.
        /// </summary>
        /// <param name="hosting">
        ///     The hosting environment meta data service. Required.
        /// </param>
        public MaintenanceRuntimeHealthController(IHostingEnvironment hosting, IServiceHost serviceHost)
        {
            HostingEnvironment = hosting ?? throw new ArgumentNullException(nameof(hosting));
            ServiceHost = serviceHost ?? throw new ArgumentNullException(nameof(serviceHost));
        }

        /// <summary>
        ///     Retrieves a detailed health ping information about the microservice host.
        /// </summary>
        [HttpGet, Route("health")]
        public IActionResult GetHealthSnapshot()
        {
            try
            {
                var currentProcess = Process.GetCurrentProcess();

                var result = new Model.DTOHealthSnapshot
                {
                    EnvironmentName = HostingEnvironment.EnvironmentName,
                    ApplicationName = HostingEnvironment.ApplicationName,
                    CLRVersion = Environment.Version.ToString(),
                    CommandLineArgs = Environment.GetCommandLineArgs(),
                    EnvironmentVars = Environment.GetEnvironmentVariables(),
                    CurrentDirectory = Environment.CurrentDirectory,
                    CurrentManagedThreadId = Environment.CurrentManagedThreadId,
                    HasShutdownStarted = Environment.HasShutdownStarted,
                    Is64BitOperatingSystem = Environment.Is64BitOperatingSystem,
                    Is64BitProcess = Environment.Is64BitProcess,
                    MachineName = Environment.MachineName,
                    OSVersion = Environment.OSVersion.ToString(),
                    ProcessorCount = Environment.ProcessorCount,
                    SystemDirectory = Environment.SystemDirectory,
                    UserDomainName = Environment.UserDomainName,
                    UserInteractive = Environment.UserInteractive,
                    UserName = Environment.UserName,
                    WorkingSet = Environment.WorkingSet,
                    TotalCPUTime = currentProcess.TotalProcessorTime,
                    UserCPUTime = currentProcess.UserProcessorTime,
                    ThreadCount = currentProcess.Threads.Count,
                };
                
                return Ok(result);
            }
            catch (Exception e)
            {
                if (HostingEnvironment.IsProduction())
                {
                    return StatusCode(500);
                }
                else
                {
                    return StatusCode(500, BusinessError.FromException(e, isFatalError: true));
                }
            }
        }
        
        /// <summary>
        ///     Retrieves public information about the microservice host.
        /// </summary>
        [HttpGet, Route("health/service/info")]
        public IActionResult GetServicePublicInfo()
        {
            return Ok(
                new Model.DTOPublicServiceInfo
                {
                    Uid = ServiceHost.Settings?.Uid?.ToString("n"),
                    Name = ServiceHost.Settings?.ServiceName,
                    Description = ServiceHost.Settings?.ServiceDescription,
                    EnvironmentName = HostingEnvironment.EnvironmentName,
                    HasBootstrapped = ServiceHost.HasBootstrapped,
                    NextIterationAt = ServiceHost.NextIterationAt,
                    IsRunning = ServiceHost.IsRunning,
                    Bindings = ServiceHost.GetHostBindings()
                }
            );
        }
    }
}