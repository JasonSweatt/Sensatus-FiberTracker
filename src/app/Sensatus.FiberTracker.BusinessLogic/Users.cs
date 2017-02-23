using Sensatus.FiberTracker.DataAccess;
using Sensatus.FiberTracker.Formatting;
using System;
using System.Data;
using System.Text;

namespace Sensatus.FiberTracker.BusinessLogic
{
    /// <summary>
    /// Class Users.
    /// </summary>
    public class Users
    {
        /// <summary>
        /// The database helper
        /// </summary>
        private DBHelper _dbHelper = new DBHelper();
        /// <summary>
        /// The security provider
        /// </summary>
        private DataSecurity _securityProvider = new DataSecurity();
        /// <summary>
        /// The arch
        /// </summary>
        private Arch _arch = new Arch();

        /// <summary>
        /// Creates new user for the specified values.
        /// </summary>
        /// <param name="roleId">Role id</param>
        /// <param name="userName">User name</param>
        /// <param name="password">Password</param>
        /// <param name="firstName">First name</param>
        /// <param name="lastName">Last name</param>
        /// <param name="email">Email id</param>
        /// <param name="mobile">Mobile number</param>
        /// <param name="isActive">true if user is active otherwise false</param>
        /// <returns>true if success otherwise false</returns>
        public bool CreateNewUser(int roleId, string userName, string password, string firstName, string lastName, string email, string mobile, bool isActive)
        {
            var paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@RoleId", roleId));
            paramCollection.Add(new DBParameter("@UserName", userName));
            paramCollection.Add(new DBParameter("@Password", _securityProvider.Encrypt(password)));
            paramCollection.Add(new DBParameter("@FirstName", firstName));
            paramCollection.Add(new DBParameter("@LastName", lastName));
            paramCollection.Add(new DBParameter("@Email", email));
            paramCollection.Add(new DBParameter("@Mobile", mobile));
            var insert = new StringBuilder("INSERT INTO UserInfo (RoleId, UserName, Password, FirstName, LastName, LastLoginDate, PasswordChangeDate, Email, Mobile, IsActive)");
            insert.Append($" VALUES (@RoleId, @UserName, @Password, @FirstName, @LastName, '{DateTime.Now}', '{DateTime.Now}', @Email, @Mobile, {(isActive ? 1 : 0)})");
            return _dbHelper.ExecuteNonQuery(insert.ToString(), paramCollection) > 0;
        }

        /// <summary>
        /// Updates the user details with the specified values.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="roleId">Role id</param>
        /// <param name="password">Password</param>
        /// <param name="firstName">First name</param>
        /// <param name="lastName">Last name</param>
        /// <param name="email">Email address</param>
        /// <param name="mobile">Mobile number</param>
        /// <param name="isActive">True if user is active otherwise False</param>
        /// <returns>True if success otherwise False</returns>
        public bool UpdateUserDetails(int userId, int roleId, string password, string firstName, string lastName, string email, string mobile, bool isActive)
        {
            var paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@RoleId", roleId));
            paramCollection.Add(new DBParameter("@Password", _securityProvider.Encrypt(password)));
            paramCollection.Add(new DBParameter("@FirstName", firstName));
            paramCollection.Add(new DBParameter("@LastName", lastName));
            paramCollection.Add(new DBParameter("@Email", email));
            paramCollection.Add(new DBParameter("@Mobile", mobile));
            paramCollection.Add(new DBParameter("@IsActiveFlag", isActive ? "1" : "0"));
            paramCollection.Add(new DBParameter("@UserId", userId));
            var update = "UPDATE UserInfo SET RoleId = @RoleId, Password = @Password, FirstName = @FirstName, LastName = @LastName, Email = @Email, Mobile = @Mobile, IsActive = @IsActiveFlag WHERE UserId = @UerId";
            return _dbHelper.ExecuteNonQuery(update, paramCollection) > 0;
        }

        /// <summary>
        /// Checks whether user exists for the name given or not.
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns>True if user exists otherwise False</returns>
        public bool CheckUserExist(string userName)
        {
            var paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@UserName", userName));
            paramCollection.Add(new DBParameter("@IsActive", 1));
            var query = "SELECT COUNT(*) FROM UserInfo WHERE UserName = @UserName AND IsActive = @IsActive";
            return Convert.ToInt16(_dbHelper.ExecuteScalar(query, paramCollection).ToString()) > 0;
        }

        /// <summary>
        /// Deletes the user. (Soft delete only. i.e. IsActive falg is set to 0)
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>True if user is deleted successfully otherwise false</returns>
        public bool DeleteUser(int userId)
        {
            var update = $"UPDATE UserInfo SET IsActive = 0 WHERE UserId = {userId}";
            return _dbHelper.ExecuteNonQuery(update) > 0;
        }

        /// <summary>
        /// Gets UserId for the specified user name
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns>Username</returns>
        public string GetUserId(string userName)
        {
            var paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@UserName", userName));
            var query = "SELECT UserId FROM UserInfo WHERE UserName = @UserName";
            return _dbHelper.ExecuteScalar(query).ToString();
        }

        /// <summary>
        /// Gets User Id for the specified User name.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>User Id</returns>
        public string GetUserName(int userId)
        {
            var query = $"SELECT UserName FROM UserInfo WHERE UserId = {userId}";
            return _dbHelper.ExecuteScalar(query).ToString();
        }

        /// <summary>
        /// Checks whether User associated to the specified User Id is active or not.
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>True if user is active otherwise false.</returns>
        public bool IsActiveUser(int userId)
        {
            var query = $"SELECT IsActive FROM UserInfo WHERE UserId = {userId}";
            return _dbHelper.ExecuteScalar(query).ToString().Equals("1");
        }

        /// <summary>
        /// Gets profile for the specified user
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>User profile in the form of DataTable</returns>
        public DataTable GetUserProfile(int userId)
        {
            var query = $"SELECT UserName, FirstName, LastName, Email, Mobile FROM UserInfo WHERE UserId = {userId}";
            var dataTable = _dbHelper.ExecuteDataTable(query);
            return dataTable;
        }

        /// <summary>
        /// Concatnates and returns specified user's First and Last name with space.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>FirstName + ' ' + LastName</returns>
        public string GetUserFirstLastName(int userId)
        {
            var query = $"SELECT FirstName + ' ' + LastName FROM UserInfo WHERE UserId = {userId}";
            var userName = _dbHelper.ExecuteScalar(query).ToString();
            return userName;
        }

        /// <summary>
        /// Updates user details for the specified values.
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="firstName">First Name</param>
        /// <param name="lastName">Last Name</param>
        /// <param name="email">Email Id</param>
        /// <param name="mobile">Mobile number</param>
        /// <returns>true if profile updated successfully otherwise false.</returns>
        public bool UpdateUserProfile(int userId, string firstName, string lastName, string email, string mobile)
        {
            var paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@FirstName", firstName));
            paramCollection.Add(new DBParameter("@LastName", lastName));
            paramCollection.Add(new DBParameter("@Email", email));
            paramCollection.Add(new DBParameter("@Mobile", mobile));
            paramCollection.Add(new DBParameter("@UserId", userId));
            var update = "UPDATE UserInfo SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Mobile = @Mobile WHERE UserId = @UserId";
            return _dbHelper.ExecuteNonQuery(update, paramCollection) > 0;
        }

        /// <summary>
        /// Updates user details for the specified values.
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="firstName">First Name</param>
        /// <param name="lastName">Last Name</param>
        /// <param name="password">Password</param>
        /// <param name="email">Email</param>
        /// <param name="mobile">Mobile number</param>
        /// <returns>true if profile updated successfully otherwise false.</returns>
        public bool UpdateUserProfile(int userId, string firstName, string lastName, string password, string email, string mobile)
        {
            var paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@FirstName", firstName));
            paramCollection.Add(new DBParameter("@LastName", lastName));
            paramCollection.Add(new DBParameter("@Password", _securityProvider.Encrypt(password)));
            paramCollection.Add(new DBParameter("@Email", email));
            paramCollection.Add(new DBParameter("@Mobile", mobile));
            paramCollection.Add(new DBParameter("@UserId", userId));
            var update = $"UPDATE UserInfo SET FirstName = @FirstName, LastName = @LastName, Password = @Password, PasswordChangeDate = '{DateTime.Now}', Email = @Email, Mobile = @Mobile WHERE UserId = @UserId";
            return _dbHelper.ExecuteNonQuery(update, paramCollection) > 0;
        }

        /// <summary>
        /// CHecks whether password specified is valid for the user id or not.
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="password">Password</param>
        /// <returns>True if password is valid otherwise false.</returns>
        public bool IsValidPassword(int userId, string password)
        {
            password = _securityProvider.Encrypt(password);
            var param = new DBParameter("@UserId", userId);
            var query = "SELECT Password FROM UserInfo WHERE UserId = @UserId";
            var pwd = _dbHelper.ExecuteScalar(query, param).ToString();
            return pwd.Trim().Equals(password);
        }

        /// <summary>
        /// Updates last login date for the logged in user.
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>True if updated successfully otherwise false</returns>
        public bool UpdateLastLoginDate(int userId)
        {
            var param = new DBParameter("@LastLoginDate", DateTime.Now, DbType.Date);
            var query = $"UPDATE UserInfo SET LastLoginDate = @LastLoginDate WHERE UserId = {userId}";
            return _dbHelper.ExecuteNonQuery(query, param) > 0;
        }

        /// <summary>
        /// Gets last login date for the specified user id
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>Last login date as string.</returns>
        public string GetLastLastLoginDate(int userId)
        {
            var query = $"SELECT LastLoginDate FROM UserInfo WHERE UserId = {userId}";
            var date = string.Empty;
            if (_dbHelper.ExecuteScalar(query) != null)
            {
                date = _dbHelper.ExecuteScalar(query).ToString();
                if (date.Equals(string.Empty))
                    return "N\\A";
                var values = date.Split(Convert.ToChar(" "));
                return DataFormat.DateToDisp(values[0]) + " " + values[1] + " " + values[2];
            }
            return "N\\A";
        }
    }
}