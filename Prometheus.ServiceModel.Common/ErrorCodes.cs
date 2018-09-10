namespace Prometheus.ServiceModel.Common
{
    /// <summary>
    ///     Container for the commonly used error codes.
    /// </summary>
    public static partial class ErrorCodes
    {
        /// <summary>
        ///     A top level error has occured with no additional details.
        ///     This usually indicates, that a new specific error handling, unit testing and error code is needed.
        /// </summary>
        public const int GeneralError = 41000;
        /// <summary>
        ///     The request is invalid, its scheme is not fulfilling its model state.
        /// </summary>
        public const int ModelStateError = 41000;

        /// <summary>
        ///     A required argument in the busines operation is missing.
        /// </summary>
        public const int ArgumentMissingError = 42140;

        /// <summary>
        ///     An argument in the business operation is invalid.
        /// </summary>
        public const int ArgumentInvalidError = 42145;

        /// <summary>
        ///     The specified business object is not present in the system.
        /// </summary>
        public const int ObjectNotFoundError = 42310;

        /// <summary>
        ///     The specified business object already exists in the system.
        /// </summary>
        public const int ObjectAlreadyExistsError = 42410;

        /// <summary>
        ///     The specified business object is a system one and cannot be changed or deleted.
        /// </summary>
        public const int SystemObjectError = 42510;
    }
}