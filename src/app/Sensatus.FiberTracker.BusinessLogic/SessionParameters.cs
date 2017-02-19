using System;
using System.Collections.Generic;
using System.Text;


namespace Sensatus.FiberTracker.BusinessLogic
{    
    public static class SessionParameters
    {        
            
        private static int _userId = 0;
        /// <summary>
        /// Gets or Sets UserId
        /// </summary>
        public static int UserID
        {
            get 
            {
                return _userId;
            }
            set 
            {
                _userId = value;
            }
        }

        private static Common.UserRole _userRole;

        /// <summary>
        /// Gets or Sets UserRole
        /// </summary>
        public static Common.UserRole UserRole
        {
            get
            {
                return _userRole;
            }
            set
            {
                _userRole = value;
            }
        }


        private static string _userName = String.Empty;

        /// <summary>
        /// Gets or Sets UserName
        /// </summary>
        public static string UserName
        {
            get 
            {
                return _userName;
            }
            set 
            {
                _userName = value;                
            }
        }
    }
}
