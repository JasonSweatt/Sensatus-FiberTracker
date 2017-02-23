using Sensatus.FiberTracker.DataAccess;
using Sensatus.FiberTracker.Formatting;
using System.Data;

namespace Sensatus.FiberTracker.BusinessLogic
{
    public class UserAuthentication
    {
        private DBHelper _dbHelper = new DBHelper();

        /// <summary>
        /// Determines whether [is valid user] [the specified user name].
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <param name="password">Password</param>
        /// <param name="userId">An out parameter which returns the UserId if login is successful</param>
        /// <param name="role">An out parameter which returns the RoleId if login is successful</param>
        /// <returns>true - If UserName and Password are valid otherwise false</returns>
        public bool IsValidUser(string userName, string password, out int userId, out Common.UserRole role)
        {
            var isValidUser = false;
            password = new DataSecurity().Encrypt(password);
            var paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@UserName", userName, DbType.String));
            paramCollection.Add(new DBParameter("@Password", password, DbType.String));

            var sqlCommand = "SELECT * FROM UserInfo WHERE UserName = @UserName AND Password = @Password AND IsActive = 1";
            role = new Common.UserRole();
            userId = 0;
            var data = _dbHelper.ExecuteDataTable(sqlCommand, paramCollection);

            if (data.Rows.Count > 0)
            {
                isValidUser = true;
                role = Common.GetUserRole(DataFormat.GetInteger(data.Rows[0]["RoleId"]));
                userId = DataFormat.GetInteger(data.Rows[0]["UserId"]);
            }
            return isValidUser;
        }
    }
}