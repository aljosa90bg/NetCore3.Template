namespace NetCore3.Template.Api.Constants
{
    /// <summary>
    /// Route constants for the User Store API
    /// </summary>
    public static class Routes
    {
        private const string BaseUri = "api/v{version:apiVersion}";

        /// <summary>
        /// Roles endpoint
        /// </summary>
        public const string ApplicationUserBase = BaseUri + "/applicationuser";
    }
}
