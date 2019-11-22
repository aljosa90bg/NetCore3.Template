using Microsoft.AspNetCore.Identity;

namespace NetCore3.Template.Api.Constants
{
    /// <summary>
    /// Descriptions of default responses
    /// </summary>
    public static class DefaultRouteDescriptions
    {
        /// <summary>
        /// Default POST response for status code 200
        /// </summary>
        public const string PostSuccessfulResponse = "Request successful, result described in return type.";

        /// <summary>
        /// Default GET response for status code 200
        /// </summary>
        public const string GetSuccessfulResponse = "Successfully retrieved entity(ies).";

        /// <summary>
        /// Default successful response
        /// </summary>
        public const string SuccessfulResponse = "Sucessful response.";

        /// <summary>
        /// Default response for status code 200 when response is <see cref="IdentityResult"/>
        /// </summary>
        public const string SuccessfulIdentityResult = "Request successful, result described in return type.";

        /// <summary>
        /// Default no content response
        /// </summary>
        public const string NoContent = "Request successful, no content.";

        /// <summary>
        /// Default response for status code 400
        /// </summary>
        public const string BadRequest = "Bad request.";

        /// <summary>
        /// Default DELETE response for status code 200
        /// </summary>
        public const string DeleteSuccessful = "Successfully deleted entity(ies)";

        /// <summary>
        /// Default not found response
        /// </summary>
        public const string NotFound = "Not found";
    }
}
