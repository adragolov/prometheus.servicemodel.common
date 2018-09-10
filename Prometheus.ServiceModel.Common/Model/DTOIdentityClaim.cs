using System.Security.Claims;

namespace Prometheus.ServiceModel.Common.Model
{
    /// <summary>
    ///     Data transfer object model for a single identity claim.
    /// </summary>
    public class DTOIdentityClaim
    {
        /// <summary>
        ///     Gets the underlying identity claim type (e.g. address).
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     Gets the mapped claim value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        ///     Creates the DTO instance from a source claim object.
        /// </summary>
        /// <param name="c">The source claim, required.</param>
        public static DTOIdentityClaim FromClaim(Claim c)
        {
            return new DTOIdentityClaim
            {
                Type = c.Type,
                Value = c.Value
            };
        }
    }
}