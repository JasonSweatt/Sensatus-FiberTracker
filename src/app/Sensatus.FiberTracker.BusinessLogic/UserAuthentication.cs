using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Sensatus.FiberTracker.DataAccess;
using Sensatus.FiberTracker.Formatting;


namespace Sensatus.FiberTracker.BusinessLogic
{
    public class UserAuthentication
    {
        private DBHelper _dbHelper = new DBHelper();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <param name="password">Password</param>
        /// <param name="userId">An out parameter which returns the UserId if login is successful</param>
        /// <param name="role">An out parameter which returns the RoleId if login is successful</param>
        /// <returns>true - If UserName and Password are valid otherwise false</returns>
        public bool IsValidUser(string userName, string password,out int userId, out Common.UserRole role)
        {
            bool isValidUser = false;
            
            password = (new DataSecurity()).Encrypt(password);
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@userName", userName, DbType.String));
            paramCollection.Add(new DBParameter("@password", password, DbType.String));

            string sqlCommand = "SELECT * FROM USER_INFO WHERE User_Name=@userName AND Pwd=@password AND IsActive=1";
            role = new Common.UserRole();
            userId = 0;

            
            DataTable data = _dbHelper.ExecuteDataTable(sqlCommand, paramCollection);
            
            if (data.Rows.Count > 0)
            {
                isValidUser =true ;
                role = Common.GetUserRole(DataFormat.GetInteger(data.Rows[0]["RoleId"]));
                userId = DataFormat.GetInteger(data.Rows[0]["User_Id"]);
            }

            return isValidUser;
        }
    }
}
