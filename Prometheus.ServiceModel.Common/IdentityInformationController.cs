using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prometheus.ServiceModel.Common.Model;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Prometheus.ServiceModel.Common
{
    /// <summary>
    ///     Controller with endpoints for simple and lightweight verification of the request identity.
    ///     Requires an authenticated request.
    /// </summary>
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class IdentityInformationController : Controller
    {
        /// <summary>
        ///     Retrieves a collection of all user claims in the request identity.
        /// </summary>
        /// <response code="200">
        ///     Returns a standard HTTP OK message with the collection of user claims (key-value pairs).
        /// </response>
        /// <response code="401">
        ///     Returns a 401 HTTP Unauthorized message for requests that lack authentication.
        /// </response>
        /// <response code="403">
        ///     Returns a 403 HTTP Forbidden message for requests that lack sufficient priviledges.
        /// </response>
        [HttpGet, Route("identity/whoami")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DTOIdentityClaim>))]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public IActionResult GetAllClaims()
        {
            return Ok(
                from c in User.Claims
                select DTOIdentityClaim.FromClaim(c)
            );
        }

        /// <summary>
        ///     Retrieves a collection of all user roles in the request identity.
        /// </summary>
        /// <response code="200">
        ///     Returns a standard HTTP OK message with the collection of user roles.
        ///     Each entry in the result is represented as the unique key of the role.
        /// </response>
        /// <response code="401">
        ///     Returns a 401 HTTP Unauthorized message for requests that lack authentication.
        /// </response>
        /// <response code="403">
        ///     Returns a 403 HTTP Forbidden message for requests that lack sufficient priviledges.
        /// </response>
        [HttpGet, Route("identity/whoami/roles")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<string>))]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public IActionResult GetRoles()
        {
            var roleClaimName = User.Identity is ClaimsIdentity claimsId ?
                claimsId.RoleClaimType :
                ClaimsIdentity.DefaultRoleClaimType;

            return Ok(
                from c in User.Claims.Where(c => c.Type.Equals(roleClaimName))
                select DTOIdentityClaim.FromClaim(c)
            );
        }
    }
}