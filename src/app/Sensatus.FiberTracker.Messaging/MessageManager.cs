using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.Globalization;
using System.Windows.Forms;
using Sensatus.FiberTracker.Configurations;

namespace Sensatus.FiberTracker.Messaging
{
    public static class MessageManager
    {
        private static global::System.Resources.ResourceManager resourceMan;
        private static global::System.Globalization.CultureInfo resourceCulture;

        private const string ACCOUNT_PLUS_CAPTION = "AccountPlus";

        public static DialogResult DisplayMessage(string message, string caption, MessageBoxIcon icon, MessageBoxButtons buttons)
        {
            return MessageBox.Show(message, caption, buttons, icon);
        }

        public static DialogResult DisplayMessage(string msgNo,  MessageBoxIcon icon, MessageBoxButtons buttons)
        {
            string message = GetMessage(msgNo);
            return DisplayMessage(message, ACCOUNT_PLUS_CAPTION + " [" + msgNo  + "]", icon, buttons);
        }

        public static DialogResult DisplayMessage(string msgNo, MessageBoxIcon icon)
        {
            string message = GetMessage(msgNo, false);
            return DisplayMessage(message, ACCOUNT_PLUS_CAPTION + " [" + msgNo  + "]", icon, MessageBoxButtons.OK);
        }

        public static DialogResult DisplayMessage(string msgNo, MessageBoxButtons buttons)
        {
            string message = GetMessage(msgNo, false);
            MessageBoxIcon icon = GetIcon(buttons);
            return DisplayMessage(message, ACCOUNT_PLUS_CAPTION + " [" + msgNo  + "]", icon, buttons);
        }


        public static DialogResult DisplayMessage(string msgNo, string[] arg, MessageBoxButtons buttons)
        {
            string message = GetMessage(msgNo, arg);
            MessageBoxIcon icon = GetIcon(buttons);
            return DisplayMessage(message, ACCOUNT_PLUS_CAPTION + " [" + msgNo  + "]", icon, buttons);
        }

        public static DialogResult DisplayMessage(string msgNo, string arg0, MessageBoxButtons buttons)
        {
            return DisplayMessage(msgNo, new string[] { arg0 }, buttons);
        }

        public static DialogResult DisplayMessage(string msgNo, string arg0, string arg1, MessageBoxButtons buttons)
        {
            return DisplayMessage(msgNo, new string[] { arg0, arg1 }, buttons);
        }

        public static DialogResult DisplayMessage(string msgNo, string arg0, string arg1,string arg2, MessageBoxButtons buttons)
        {
            return DisplayMessage(msgNo, new string[] { arg0, arg1, arg2 }, buttons);
        }

        public static DialogResult DisplayMessage(string msgNo)
        {
            string message = GetMessage(msgNo);            
            return DisplayMessage(message, ACCOUNT_PLUS_CAPTION + " [" + msgNo  + "]", MessageBoxIcon.Information , MessageBoxButtons.OK );
        }

        public static DialogResult DisplayMessage(string msgNo, string[] arg)
        {
            string message = GetMessage(msgNo, arg);
            return DisplayMessage(message, ACCOUNT_PLUS_CAPTION + " [" + msgNo  + "]", MessageBoxIcon.Information, MessageBoxButtons.OK);
        }

        public static DialogResult DisplayMessage(string msgNo, string arg)
        {
            return DisplayMessage(msgNo, new string[] { arg});
        }

        public static DialogResult DisplayMessage(string msgNo, string arg1, string arg2)
        {
            return DisplayMessage(msgNo, new string[] { arg1, arg2  });
        }

        public static DialogResult DisplayMessage(string msgNo, string arg1, string arg2, string arg3)
        {
            return DisplayMessage(msgNo, new string[] { arg1, arg2, arg3 });
        }

        public static DialogResult DisplayCustomMessage(string message)
        {            
            return DisplayMessage(message, ACCOUNT_PLUS_CAPTION, MessageBoxIcon.Information, MessageBoxButtons.OK);
        }

        public static DialogResult DisplayCustomMessage(string message, MessageBoxButtons buttons)
        {
            MessageBoxIcon icon = GetIcon(buttons);
            return DisplayMessage(message, ACCOUNT_PLUS_CAPTION, icon, buttons);
        }

        private static MessageBoxIcon GetIcon(MessageBoxButtons buttons)
        {
            switch (buttons)
            {
                case MessageBoxButtons.YesNo:
                case MessageBoxButtons.YesNoCancel:
                    return MessageBoxIcon.Question;
                default :
                    return MessageBoxIcon.Information;
            }
        }

        #region Public Methods To Read Message From Resource File
        public static string GetMessage(string messageNo, bool includeNo)
        {
            string message = string.Empty;
            message = ResourceManager.GetString("MSG" + messageNo, Culture);
            message = includeNo ? "[" + messageNo + "] " + message : message;
            return message;
        }

        public static string GetMessage(string messageNo, string[] arg,  bool includeNo)
        {
            string message = string.Empty;
            message = ResourceManager.GetString("MSG" + messageNo, Culture);
            message = string.Format(message, arg);
            message = includeNo ? "[" + messageNo + "]" + message : message;
            return message;
        }

        public static string GetMessage(string messageNo, string arg0, bool includeNo)
        {
            return GetMessage(messageNo, new string[] { arg0 }, includeNo);
        }

        public static string GetMessage(string messageNo, string arg0, string arg1, bool includeNo)
        {
            return GetMessage(messageNo, new string[] { arg0, arg1 }, includeNo);
        }

        public static string GetMessage(string messageNo, string arg0, string arg1, string arg2, bool includeNo)
        {
            return GetMessage(messageNo, new string[] { arg0, arg1, arg2 }, includeNo);
        }

        public static string GetMessage(string messageNo)
        {
            return GetMessage(messageNo, false);
        }

        public static string GetMessage(string messageNo, string[] arg)
        {
            return GetMessage(messageNo, arg, false);
        }

        public static string GetMessage(string messageNo, string arg0)
        {
            return GetMessage(messageNo, new string[] { arg0 });
        }

        public static string GetMessage(string messageNo, string arg0, string arg1)
        {
            return GetMessage(messageNo, new string[] { arg0, arg1 });
        }

        public static string GetMessage(string messageNo, string arg0, string arg1, string arg2)
        {
            return GetMessage(messageNo, new string[] { arg0, arg1, arg2 });
        }

        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Sensatus.FiberTracker.Messaging.MessageResource", typeof(MessageResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }

        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        #endregion
    }
}
