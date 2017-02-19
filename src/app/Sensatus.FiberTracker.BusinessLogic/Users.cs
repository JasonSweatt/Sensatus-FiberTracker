using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Sensatus.FiberTracker.DataAccess;
using Sensatus.FiberTracker.Formatting;

namespace Sensatus.FiberTracker.BusinessLogic
{
    public class Users
    {
        private DBHelper _dbHelper = new DBHelper();
        private DataSecurity _securityProvider = new DataSecurity();

        private Arch _arch = new Arch();

        /// <summary>
        /// Createates new user for the specified values.
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
            int isActiveFlag = isActive ? 1 : 0;
            string Query = string.Empty;            
      
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@roleId", roleId));
            paramCollection.Add(new DBParameter("@userName", userName));
            paramCollection.Add(new DBParameter("@password", _securityProvider.Encrypt(password)));
            paramCollection.Add(new DBParameter("@firstName", firstName));
            paramCollection.Add(new DBParameter("@lastName", lastName));            
            paramCollection.Add(new DBParameter("@email", email));
            paramCollection.Add(new DBParameter("@mobile", mobile));            
            
            Query = "INSERT INTO User_Info(RoleId, User_Name, Pwd, First_Name, Last_Name, " +
                "Last_Login_Date,Password_Change_Date, Email, Mobile ,IsActive) VALUES (" +
                "@roleId , @userName , @password, @firstName, @lastName, " +
                "'"+ DateTime.Now.ToString() + "', '" +  DateTime.Now.ToString() + "', @email, @mobile, 1)";
            
            return  _dbHelper.ExecuteNonQuery(Query, paramCollection) > 0;                
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
            string Query = string.Empty;
            string isActiveFlag = isActive ? "1" : "0";                        

            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@roleId", roleId));
            paramCollection.Add(new DBParameter("@password", _securityProvider.Encrypt(password)));
            paramCollection.Add(new DBParameter("@firstName", firstName));
            paramCollection.Add(new DBParameter("@lastName", lastName));
            paramCollection.Add(new DBParameter("@email", email));
            paramCollection.Add(new DBParameter("@mobile", mobile));
            paramCollection.Add(new DBParameter("@isActiveFlag", isActiveFlag));
            paramCollection.Add(new DBParameter("@userId", userId));

            Query = "UPDATE User_Info SET RoleId = @roleId, Pwd = @password , " + 
            "First_Name = @firstName , Last_Name =@lastName , " + 
            "Email = @email , Mobile = @mobile, IsActive = @isActiveFlag " + 
            "WHERE User_Id = @userId";
            return _dbHelper.ExecuteNonQuery(Query, paramCollection) > 0;
        }
      
        /// <summary>
        /// Checks whether user exists for the name given or not.
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns>True if user exists otherwise False</returns>
        public bool CheckUserExist(string userName)
        {            
            string Query = string.Empty;
            DBParameterCollection paramCollection  = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@userName", userName));
            paramCollection.Add(new DBParameter("@isActive", 1));

            Query = "SELECT COUNT(*) FROM User_Info where User_Name=@userName AND IsActive=@isActive";

            return Convert.ToInt16(_dbHelper.ExecuteScalar(Query, paramCollection).ToString()) > 0;                
        }

        /// <summary>
        /// Deletes the user. (Soft delete only. i.e. IsActive falg is set to 0)
        /// </summary>
        /// <param name="userID">User Id</param>
        /// <returns>True if user is deleted successfully otherwise false</returns>
        public bool DeleteUser(int userID)
        {
            string Query = "Update User_Info Set IsActive=0 where User_Id=" + userID.ToString();
            return (_dbHelper.ExecuteNonQuery(Query) > 0);
        }

        /// <summary>
        /// Gets UserId for the specified user name
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns>Username</returns>
        public string GetUserId(string userName)
        {
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@userName", userName));            
            string Query = "SELECT User_Id from User_Info where User_Name=@userName";

            return _dbHelper.ExecuteScalar(Query).ToString();
        }

        /// <summary>
        /// Gets User Id for the specified User name.
        /// </summary>
        /// <param name="userID">User Id</param>
        /// <returns>User Id</returns>
        public string GetUserName(int userID)
        {
            string Query = "SELECT User_Name from User_Info where User_Id=" + userID.ToString();

            return _dbHelper.ExecuteScalar(Query).ToString();
        }

        /// <summary>
        /// Checks whether User associated to the specified User Id is active or not.
        /// </summary>
        /// <param name="userID">User Id</param>
        /// <returns>True if user is active otherwise false.</returns>
        public bool IsActiveUser(int userID)
        {
            string Query = "SELECT IsActive from User_Info where User_Id=" + userID.ToString();

            if(_dbHelper.ExecuteScalar(Query).ToString().Equals("1"))
                return true;
            else
                return false;
            
        }

        /// <summary>
        /// Gets profile for the specified user 
        /// </summary>
        /// <param name="userID">User Id</param>
        /// <returns>User profile in the form of DataTable</returns>
        public DataTable GetUserProfile(int userID)
        {            
            DataTable dtProfile = new DataTable();
            string Query = "SELECT User_Name, First_Name, Last_Name, Email, Mobile from User_Info where User_Id=" + userID.ToString();
            dtProfile = _dbHelper.ExecuteDataTable(Query);
            return dtProfile;
        }

        /// <summary>
        /// Concatnates and returns specified user's First and Last name with space.
        /// </summary>
        /// <param name="userID">User Id</param>
        /// <returns>FirstName + ' ' + LastName </returns>
        public string GetUserFirstLastName(int userID)
        {
            string userName = string.Empty;
            string Query = "SELECT First_Name + ' ' + Last_Name from User_Info where User_Id=" + userID.ToString();
            userName = _dbHelper.ExecuteScalar(Query).ToString();

            return userName;
        }


        /// <summary>
        /// Updates user details for the specified values.
        /// </summary>
        /// <param name="userID">User Id</param>
        /// <param name="firstName">First Name</param>
        /// <param name="lastName">Last Name</param>
        /// <param name="email">Email Id</param>
        /// <param name="mobile">Mobile number</param>
        /// <returns>true if profile updated successfully otherwise false.</returns>
        public bool UpdateUserProfile(int userID, string firstName, string lastName, string email, string mobile)
        {
            DBParameterCollection paramCollection = new DBParameterCollection();                        
            paramCollection.Add(new DBParameter("@firstName", firstName));
            paramCollection.Add(new DBParameter("@lastName", lastName));
            paramCollection.Add(new DBParameter("@email", email));
            paramCollection.Add(new DBParameter("@mobile", mobile));            
            paramCollection.Add(new DBParameter("@userId", userID));

            string Query = "UPDATE User_Info SET First_Name=@firstName, " +
                "Last_Name=@lastName, Email = @email, Mobile = @mobile " +
                "WHERE User_Id=@userId";

            return _dbHelper.ExecuteNonQuery(Query, paramCollection) > 0;                
        }

        /// <summary>
        /// Updates user details for the specified values.
        /// </summary>
        /// <param name="userID">User Id</param>
        /// <param name="firstName">First Name</param>
        /// <param name="lastName">Last Name</param>
        /// <param name="password">Password</param>
        /// <param name="email">Email</param>
        /// <param name="mobile">Mobile number</param>
        /// <returns>true if profile updated successfully otherwise false.</returns>
        public bool UpdateUserProfile(int userID, string firstName, string lastName, string password, string email, string mobile)
        {            
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@firstName", firstName));
            paramCollection.Add(new DBParameter("@lastName", lastName));
            paramCollection.Add(new DBParameter("@password", _securityProvider.Encrypt(password)));            
            paramCollection.Add(new DBParameter("@email", email));
            paramCollection.Add(new DBParameter("@mobile", mobile));            
            paramCollection.Add(new DBParameter("@userId", userID));

            string Query = "UPDATE User_Info SET First_Name=@firstName, Last_Name=@lastName, " +
                "Pwd=@password , Password_Change_Date='" + DateTime.Now.ToString()+ "', " +
                "Email=@email , Mobile=@mobile"+
                " WHERE User_Id=@userId";

            return _dbHelper.ExecuteNonQuery(Query, paramCollection) > 0;                
        }

        /// <summary>
        /// CHecks whether password specified is valid for the user id or not.
        /// </summary>
        /// <param name="userID">User Id</param>
        /// <param name="password">Password</param>
        /// <returns>True if password is valid otherwise false.</returns>
        public bool IsValidPassword(int userID, string password)
        {
            password = _securityProvider.Encrypt(password);

            DBParameter param = new DBParameter("@userId", userID);
            string Query = "SELECT Pwd from User_Info where User_Id=@userId";
            string pwd = _dbHelper.ExecuteScalar(Query, param).ToString();

            if (pwd.Trim().Equals(password))
                return true;
            else
                return false ;
        }

        /// <summary>
        /// Updates last login date for the logged in user.
        /// </summary>
        /// <param name="userID">User Id</param>
        /// <returns>True if updated successfully otherwise false</returns>
        public bool UpdateLastLoginDate(int userID)
        {
            DBParameter param = new DBParameter("@lastLoginDate", System.DateTime.Now, DbType.Date);
            string Query = "UPDATE User_Info SET Last_Login_Date=@lastLoginDate WHERE User_Id=" + userID.ToString();

            return _dbHelper.ExecuteNonQuery(Query, param) > 0;                
        }

        /// <summary>
        /// Gets last login date for the specified user id
        /// </summary>
        /// <param name="userID">User Id</param>
        /// <returns>Last login date as string.</returns>
        public string GetLastLastLoginDate(int userID)
        {
            string Query = "SELECT Last_Login_Date from User_Info WHERE User_Id=" + userID.ToString();
            string date = string.Empty;

            if (_dbHelper.ExecuteScalar(Query) != null)
            {
                date = _dbHelper.ExecuteScalar(Query).ToString();

                if (date.Equals(string.Empty))
                    return "N\\A";
                else
                {
                    string[] strDate = date.Split(Convert.ToChar(" "));
                    return DataFormat.DateToDisp(strDate[0]) + " " + strDate[1] + " " + strDate[2];
                }
            }
            else 
                return "N\\A";
        }

    }
}
