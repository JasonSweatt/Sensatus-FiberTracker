
namespace Sensatus.FiberTracker.BusinessLogic
{
    public static class SessionParameters
    {
        /// <summary>
        /// Gets or Sets UserId
        /// </summary>
        public static int UserId { get; set; } = 0;

        /// <summary>
        /// Gets or Sets UserRole
        /// </summary>
        public static Common.UserRole UserRole { get; set; }

        /// <summary>
        /// Gets or Sets UserName
        /// </summary>
        public static string UserName { get; set; } = string.Empty;
    }
}