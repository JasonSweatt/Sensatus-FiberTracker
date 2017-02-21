using System.Globalization;
using System.Resources;
using System.Windows.Forms;

namespace Sensatus.FiberTracker.Messaging
{
    public static class MessageManager
    {
        private static ResourceManager _resourceMan;

        private const string Caption = "Sensatus Fiber Tracker";

        /// <summary>
        /// Displays the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="buttons">The buttons.</param>
        /// <returns>DialogResult.</returns>
        public static DialogResult DisplayMessage(string message, string caption, MessageBoxIcon icon, MessageBoxButtons buttons)
        {
            return MessageBox.Show(message, caption, buttons, icon);
        }

        /// <summary>
        /// Displays the message.
        /// </summary>
        /// <param name="msgNo">The MSG no.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="buttons">The buttons.</param>
        /// <returns>DialogResult.</returns>
        public static DialogResult DisplayMessage(string msgNo, MessageBoxIcon icon, MessageBoxButtons buttons)
        {
            var message = GetMessage(msgNo);
            return DisplayMessage(message, $"{Caption}[{msgNo}]", icon, buttons);
        }

        /// <summary>
        /// Displays the message.
        /// </summary>
        /// <param name="msgNo">The MSG no.</param>
        /// <param name="icon">The icon.</param>
        /// <returns>DialogResult.</returns>
        public static DialogResult DisplayMessage(string msgNo, MessageBoxIcon icon)
        {
            var message = GetMessage(msgNo, false);
            return DisplayMessage(message, $"{Caption}[{msgNo}]", icon, MessageBoxButtons.OK);
        }

        /// <summary>
        /// Displays the message.
        /// </summary>
        /// <param name="msgNo">The MSG no.</param>
        /// <param name="buttons">The buttons.</param>
        /// <returns>DialogResult.</returns>
        public static DialogResult DisplayMessage(string msgNo, MessageBoxButtons buttons)
        {
            var message = GetMessage(msgNo, false);
            var icon = GetIcon(buttons);
            return DisplayMessage(message, $"{Caption}[{msgNo}]", icon, buttons);
        }

        /// <summary>
        /// Displays the message.
        /// </summary>
        /// <param name="msgNo">The MSG no.</param>
        /// <param name="arg">The argument.</param>
        /// <param name="buttons">The buttons.</param>
        /// <returns>DialogResult.</returns>
        public static DialogResult DisplayMessage(string msgNo, string[] arg, MessageBoxButtons buttons)
        {
            var message = GetMessage(msgNo, arg);
            var icon = GetIcon(buttons);
            return DisplayMessage(message, $"{Caption}[{msgNo}]", icon, buttons);
        }

        /// <summary>
        /// Displays the message.
        /// </summary>
        /// <param name="msgNo">The MSG no.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="buttons">The buttons.</param>
        /// <returns>DialogResult.</returns>
        public static DialogResult DisplayMessage(string msgNo, string arg0, MessageBoxButtons buttons)
        {
            return DisplayMessage(msgNo, new[] { arg0 }, buttons);
        }

        /// <summary>
        /// Displays the message.
        /// </summary>
        /// <param name="msgNo">The MSG no.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="buttons">The buttons.</param>
        /// <returns>DialogResult.</returns>
        public static DialogResult DisplayMessage(string msgNo, string arg0, string arg1, MessageBoxButtons buttons)
        {
            return DisplayMessage(msgNo, new[] { arg0, arg1 }, buttons);
        }

        /// <summary>
        /// Displays the message.
        /// </summary>
        /// <param name="msgNo">The MSG no.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        /// <param name="buttons">The buttons.</param>
        /// <returns>DialogResult.</returns>
        public static DialogResult DisplayMessage(string msgNo, string arg0, string arg1, string arg2, MessageBoxButtons buttons)
        {
            return DisplayMessage(msgNo, new[] { arg0, arg1, arg2 }, buttons);
        }

        /// <summary>
        /// Displays the message.
        /// </summary>
        /// <param name="msgNo">The MSG no.</param>
        /// <returns>DialogResult.</returns>
        public static DialogResult DisplayMessage(string msgNo)
        {
            var message = GetMessage(msgNo);
            return DisplayMessage(message, $"{Caption}[{msgNo}]", MessageBoxIcon.Information, MessageBoxButtons.OK);
        }

        /// <summary>
        /// Displays the message.
        /// </summary>
        /// <param name="msgNo">The MSG no.</param>
        /// <param name="arg">The argument.</param>
        /// <returns>DialogResult.</returns>
        public static DialogResult DisplayMessage(string msgNo, string[] arg)
        {
            var message = GetMessage(msgNo, arg);
            return DisplayMessage(message, $"{Caption}[{msgNo}]", MessageBoxIcon.Information, MessageBoxButtons.OK);
        }

        /// <summary>
        /// Displays the message.
        /// </summary>
        /// <param name="msgNo">The MSG no.</param>
        /// <param name="arg">The argument.</param>
        /// <returns>DialogResult.</returns>
        public static DialogResult DisplayMessage(string msgNo, string arg)
        {
            return DisplayMessage(msgNo, new [] { arg });
        }

        /// <summary>
        /// Displays the message.
        /// </summary>
        /// <param name="msgNo">The MSG no.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        /// <returns>DialogResult.</returns>
        public static DialogResult DisplayMessage(string msgNo, string arg1, string arg2)
        {
            return DisplayMessage(msgNo, new [] { arg1, arg2 });
        }

        /// <summary>
        /// Displays the message.
        /// </summary>
        /// <param name="msgNo">The MSG no.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        /// <param name="arg3">The arg3.</param>
        /// <returns>DialogResult.</returns>
        public static DialogResult DisplayMessage(string msgNo, string arg1, string arg2, string arg3)
        {
            return DisplayMessage(msgNo, new [] { arg1, arg2, arg3 });
        }

        /// <summary>
        /// Displays the custom message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>DialogResult.</returns>
        public static DialogResult DisplayCustomMessage(string message)
        {
            return DisplayMessage(message, Caption, MessageBoxIcon.Information, MessageBoxButtons.OK);
        }

        /// <summary>
        /// Displays the custom message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="buttons">The buttons.</param>
        /// <returns>DialogResult.</returns>
        public static DialogResult DisplayCustomMessage(string message, MessageBoxButtons buttons)
        {
            var icon = GetIcon(buttons);
            return DisplayMessage(message, Caption, icon, buttons);
        }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <param name="buttons">The buttons.</param>
        /// <returns>MessageBoxIcon.</returns>
        private static MessageBoxIcon GetIcon(MessageBoxButtons buttons)
        {
            switch (buttons)
            {
                case MessageBoxButtons.YesNo:
                case MessageBoxButtons.YesNoCancel:
                    return MessageBoxIcon.Question;

                default:
                    return MessageBoxIcon.Information;
            }
        }

        #region Public Methods To Read Message From Resource File

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <param name="messageNo">The message no.</param>
        /// <param name="includeNo">if set to <c>true</c> [include no].</param>
        /// <returns>System.String.</returns>
        public static string GetMessage(string messageNo, bool includeNo)
        {
            var message = ResourceManager.GetString($"MSG{messageNo}" , Culture);
            message = includeNo ? $"[{messageNo}]{message}" : message;
            return message;
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <param name="messageNo">The message no.</param>
        /// <param name="arg">The argument.</param>
        /// <param name="includeNo">if set to <c>true</c> [include no].</param>
        /// <returns>System.String.</returns>
        public static string GetMessage(string messageNo, string[] arg, bool includeNo)
        {
            var message = ResourceManager.GetString($"MSG{messageNo}", Culture);
            message = string.Format(message, arg);
            message = includeNo ? $"[{messageNo}]{message}" : message;
            return message;
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <param name="messageNo">The message no.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="includeNo">if set to <c>true</c> [include no].</param>
        /// <returns>System.String.</returns>
        public static string GetMessage(string messageNo, string arg0, bool includeNo)
        {
            return GetMessage(messageNo, new [] { arg0 }, includeNo);
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <param name="messageNo">The message no.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="includeNo">if set to <c>true</c> [include no].</param>
        /// <returns>System.String.</returns>
        public static string GetMessage(string messageNo, string arg0, string arg1, bool includeNo)
        {
            return GetMessage(messageNo, new [] { arg0, arg1 }, includeNo);
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <param name="messageNo">The message no.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        /// <param name="includeNo">if set to <c>true</c> [include no].</param>
        /// <returns>System.String.</returns>
        public static string GetMessage(string messageNo, string arg0, string arg1, string arg2, bool includeNo)
        {
            return GetMessage(messageNo, new [] { arg0, arg1, arg2 }, includeNo);
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <param name="messageNo">The message no.</param>
        /// <returns>System.String.</returns>
        public static string GetMessage(string messageNo)
        {
            return GetMessage(messageNo, false);
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <param name="messageNo">The message no.</param>
        /// <param name="arg">The argument.</param>
        /// <returns>System.String.</returns>
        public static string GetMessage(string messageNo, string[] arg)
        {
            return GetMessage(messageNo, arg, false);
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <param name="messageNo">The message no.</param>
        /// <param name="arg0">The arg0.</param>
        /// <returns>System.String.</returns>
        public static string GetMessage(string messageNo, string arg0)
        {
            return GetMessage(messageNo, new [] { arg0 });
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <param name="messageNo">The message no.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        /// <returns>System.String.</returns>
        public static string GetMessage(string messageNo, string arg0, string arg1)
        {
            return GetMessage(messageNo, new [] { arg0, arg1 });
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <param name="messageNo">The message no.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        /// <returns>System.String.</returns>
        public static string GetMessage(string messageNo, string arg0, string arg1, string arg2)
        {
            return GetMessage(messageNo, new [] { arg0, arg1, arg2 });
        }

        /// <summary>
        /// Returns the cached ResourceManager instance used by this class.
        /// </summary>
        /// <value>The resource manager.</value>
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (!ReferenceEquals(_resourceMan, null)) return _resourceMan;
                var temp = new ResourceManager("Sensatus.FiberTracker.Messaging.MessageResource", typeof(MessageResource).Assembly);
                _resourceMan = temp;
                return _resourceMan;
            }
        }

        /// <summary>
        /// Overrides the current thread's CurrentUICulture property for all
        /// resource lookups using this strongly typed resource class.
        /// </summary>
        /// <value>The culture.</value>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture { get; set; }

        #endregion Public Methods To Read Message From Resource File
    }
}