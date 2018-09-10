using Microsoft.AspNetCore.Mvc;
using Prometheus.Core.Model;
using System;
using System.Linq;

namespace Prometheus.ServiceModel.Common
{
    /// <summary>
    ///     Class that contains commonly used extensions for the Controller class.
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        ///     Returns status code 400 (bad request) and <seealso cref="ErrorCodes.ModelStateError"/>
        /// </summary>
        /// <param name="controller">The controller object.</param>
        /// <param name="e">The source exception object. Required.</param>
        public static IActionResult ModelStateError(this Controller controller)
        {
            var modelState = controller.ModelState;
            var modelStateErrorMessages = modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(x => $"{key}: {x.ErrorMessage}"))
                .ToArray();
            var result = BusinessError.FromMessage(
                "Request model state is invalid",
                errorCode: ErrorCodes.ModelStateError
            );
            result.ErrorInfo.TryAdd("errorMessages", modelStateErrorMessages);

            return controller.BadRequest(result);
        }
        /// <summary>
        ///     Returns status code 400 (bad request) and <seealso cref="ErrorCodes.GeneralError"/>
        /// </summary>
        /// <param name="controller">The controller object.</param>
        /// <param name="e">The source exception object. Required.</param>
        public static IActionResult GeneralError(this Controller controller, Exception e)
        {
            return controller.BadRequest(
                BusinessError.FromException(
                    e,
                    errorCode: ErrorCodes.GeneralError
                )
            );
        }
        /// <summary>
        ///     Returns status code 400 (bad request) and <seealso cref="ErrorCodes.GeneralError"/>
        /// </summary>
        /// <param name="controller">The controller object.</param>
        /// <param name="message">The error message to be returned.</param>
        public static IActionResult GeneralError(this Controller controller, string message = "General error.")
        {
            return controller.BadRequest(
                BusinessError.FromMessage(
                    message,
                    errorCode: ErrorCodes.GeneralError
                )
            );
        }
        /// <summary>
        ///     Returns status code 400 (bad request) and <seealso cref="ErrorCodes.ArgumentInvalidError"/>
        /// </summary>
        /// <param name="controller">The controller object.</param>
        /// <param name="parameterName">The name of the parameter that is required but missing.</param>
        public static IActionResult InvalidParameterError(this Controller controller, string parameterName = null)
        {
            return controller.BadRequest(
                BusinessError.FromMessage(
                    string.IsNullOrEmpty(parameterName) ?
                        "Invalid request parameter." :
                        $"Request parameter '{parameterName}' is invalid.",
                    errorCode: ErrorCodes.ArgumentInvalidError
                )
            );
        }
        /// <summary>
        ///     Returns status code 400 (bad request) and <seealso cref="ErrorCodes.ArgumentMissingError"/>
        /// </summary>
        /// <param name="controller">The controller object.</param>
        /// <param name="parameterName">The name of the parameter that is required but missing.</param>
        public static IActionResult MissingParameterError(this Controller controller, string parameterName = null)
        {
            return controller.BadRequest(
                BusinessError.FromMessage(
                    string.IsNullOrEmpty(parameterName) ? 
                        "A required request parameter is missing." :
                        $"Request parameter '{parameterName}' is missing.",
                    errorCode: ErrorCodes.ArgumentMissingError
                )
            );
        }
        /// <summary>
        ///     Returns status code 400 (bad request) and <seealso cref="ErrorCodes.ObjectAlreadyExistsError"/>
        /// </summary>
        /// <param name="controller">The controller object.</param>
        /// <param name="objectKey">The object identifier key.</param>
        /// <param name="objectType">The type of the object.</param>
        public static IActionResult ObjectAlreadyExistsError(this Controller controller,
            string objectType = null,
            string objectKey = null)
        {
            var objectPrefix = string.IsNullOrEmpty(objectType) ? "Object" : objectType;

            if (string.IsNullOrEmpty(objectKey))
            {
                return controller.BadRequest(
                    BusinessError.FromMessage(
                        $"{objectPrefix} already exists.",
                        errorCode: ErrorCodes.ObjectAlreadyExistsError
                    )
                );
            }
            else
            {
                return controller.BadRequest(
                    BusinessError.FromMessage(
                        $"{objectPrefix} with key '{objectKey}' already exists.",
                        errorCode: ErrorCodes.ObjectAlreadyExistsError
                    )
                );
            }
        }
        /// <summary>
        ///     Returns status code 400 (bad request) and <seealso cref="ErrorCodes.ObjectAlreadyExistsError"/>
        /// </summary>
        /// <param name="controller">The controller object.</param>
        /// <param name="objectKey">The object identifier key.</param>
        /// <param name="objectType">The type of the object.</param>
        public static IActionResult ObjectAlreadyExistsError(this Controller controller,
            Type objectType = null,
            string objectKey = null)
        {
            objectType = objectType ?? typeof(object);

            return controller.ObjectAlreadyExistsError(nameof(objectType), objectKey);
        }
        /// <summary>
        ///     Returns status code 400 (bad request) and <seealso cref="ErrorCodes.ObjectAlreadyExistsError"/>
        /// </summary>
        /// <param name="controller">The controller object.</param>
        /// <param name="objectKey">The object identifier key.</param>
        /// <typeparam name="T">The type of the object.</typeparam>
        public static IActionResult ObjectAlreadyExistsError<T>(this Controller controller,
            string objectKey = null)
        {
            return controller.ObjectAlreadyExistsError(typeof(T).Name, objectKey);
        }
        /// <summary>
        ///     Returns status code 400 (bad request) and <seealso cref="ErrorCodes.ObjectNotFoundError"/>
        /// </summary>
        /// <param name="controller">The controller object.</param>
        /// <param name="objectKey">The object identifier key.</param>
        /// <param name="objectType">The type of the object.</param>
        public static IActionResult ObjectNotFoundError(this Controller controller,
            string objectType = null,
            string objectKey = null)
        {
            var objectPrefix = string.IsNullOrEmpty(objectType) ? "Object" : objectType;

            if (string.IsNullOrEmpty(objectKey))
            {
                return controller.BadRequest(
                    BusinessError.FromMessage(
                        $"{objectPrefix} not found.",
                        errorCode: ErrorCodes.ObjectNotFoundError
                    )
                );
            }
            else
            {
                return controller.BadRequest(
                    BusinessError.FromMessage(
                        $"{objectPrefix} with key '{objectKey}' not found.",
                        errorCode: ErrorCodes.ObjectNotFoundError
                    )
                );
            }
        }
        /// <summary>
        ///     Returns status code 400 (bad request) and <seealso cref="ErrorCodes.ObjectNotFoundError"/>
        /// </summary>
        /// <param name="controller">The controller object.</param>
        /// <param name="objectKey">The object identifier key.</param>
        /// <param name="objectType">The type of the object.</param>
        public static IActionResult ObjectNotFoundError(this Controller controller,
            Type objectType = null,
            string objectKey = null)
        {
            objectType = objectType ?? typeof(object);

            return controller.ObjectNotFoundError(nameof(objectType), objectKey);
        }
        /// <summary>
        ///     Returns status code 400 (bad request) and <seealso cref="ErrorCodes.ObjectNotFoundError"/>
        /// </summary>
        /// <param name="controller">The controller object.</param>
        /// <param name="objectKey">The object identifier key.</param>
        /// <typeparam name="T">The type of the object.</typeparam>
        public static IActionResult ObjectNotFoundError<T>(this Controller controller,
            string objectKey = null)
        {
            return controller.ObjectNotFoundError(typeof(T).Name, objectKey);
        }
        /// <summary>
        ///     Returns status code 500 (internal server error) and exception payload.
        /// </summary>
        /// <param name="controller">The controller object.</param>
        /// <param name="e">The source exception object. Required.</param>
        public static IActionResult FatalServerError(this Controller controller, Exception e)
        {
            return controller.StatusCode(500,
                BusinessError.FromException(e, isFatalError: true)
            );
        }
    }
}