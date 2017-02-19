using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Sensatus.FiberTracker.DataAccess;

namespace Sensatus.FiberTracker.BusinessLogic
{
    public static class  Common
    {
        /// <summary>
        /// Background color for the screen
        /// </summary>
        public static Color BGColor
        {
            get
            {
                return Color.FromArgb(122, 150, 223);
            }
        }

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
            UserRole userRole = new UserRole();
            string sqlQuery = "SELECT Role from RoleDetails Where RoleId=" + roleId.ToString();
            object role = (new DBHelper()).ExecuteScalar(sqlQuery);
            if (role != null)
            {
                switch (role.ToString().ToUpper())
                {
                    case "ADMIN":
                        userRole = UserRole.Admin;
                        break;
                    case "USER":
                        userRole= UserRole.GeneralUser;
                        break;
                    default :
                        userRole = UserRole.GeneralUser;
                        break;
                }
            }

            return userRole;
        }
    }
}
