using Sensatus.FiberTracker.DataAccess;
using System.Drawing;

namespace Sensatus.FiberTracker.BusinessLogic
{
    /// <summary>
    /// Class Common.
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// Background color for the screen
        /// </summary>
        /// <value>The color of the bg.</value>
        public static Color BGColor => Color.FromArgb(122, 150, 223);

        /// <summary>
        /// User roles supported by system
        /// </summary>
        public enum UserRole
        {
            Admin, GeneralUser
        }

        /// <summary>
        /// Returns the UserRole for the specified RoleId
        /// </summary>
        /// <param name="roleId">RoleId</param>
        /// <returns>UserRole</returns>
        public static UserRole GetUserRole(int roleId)
        {
            var userRole = new UserRole();
            var sqlQuery = $"SELECT Role from RoleDetails Where RoleId = {roleId}";
            var role = new DBHelper().ExecuteScalar(sqlQuery);
            if (role == null) return userRole;
            switch (role.ToString().ToUpper())
            {
                case "ADMIN":
                    userRole = UserRole.Admin;
                    break;

                case "USER":
                    userRole = UserRole.GeneralUser;
                    break;

                default:
                    userRole = UserRole.GeneralUser;
                    break;
            }

            return userRole;
        }
    }
}