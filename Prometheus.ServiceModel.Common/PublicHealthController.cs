using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prometheus.ServiceModel.Extensions;

namespace Prometheus.ServiceModel.Common
{
    /// <summary>
    ///     Public facing controller with anonymous access for end users and services.
    /// </summary>
    [AllowAnonymous]
    public class PublicHealthController : Controller
    {
        protected readonly IServiceHostStartup HostStartup;

        public PublicHealthController(IServiceHostStartup startup)
        {
            HostStartup = startup;
        }

        /// <summary>
        ///     Retrieves a simple health ping response.
        /// </summary>
        [HttpGet, Route("ping")]
        public IActionResult GetAnonymousPing()
        {
            return Ok(HostStartup.CreateConsulServiceRegistration());
        }
    }
}