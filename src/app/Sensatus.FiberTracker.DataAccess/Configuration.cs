using System.Configuration;

namespace Sensatus.FiberTracker.DataAccess
{
    /// <summary>
    /// Class Configuration.
    /// </summary>
    internal static class Configuration
    {
        /// <summary>
        /// The default connection key
        /// </summary>
        private const string DEFAULT_CONNECTION_KEY = "defaultConnection";

        /// <summary>
        /// Gets the default connection.
        /// </summary>
        /// <value>The default connection.</value>
        public static string DefaultConnection => ConfigurationManager.AppSettings[DEFAULT_CONNECTION_KEY];

        /// <summary>
        /// Gets the database provider.
        /// </summary>
        /// <value>The database provider.</value>
        public static string DBProvider => ConfigurationManager.ConnectionStrings[DefaultConnection].ProviderName;

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public static string ConnectionString => ConfigurationManager.ConnectionStrings[DefaultConnection].ConnectionString;
    }
}