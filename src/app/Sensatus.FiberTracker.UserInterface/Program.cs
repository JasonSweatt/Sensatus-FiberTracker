using Sensatus.FiberTracker.BusinessLogic;
using System;
using System.Windows.Forms;

namespace Sensatus.FiberTracker.UI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // Add Exception Handler so that for every thread exception Application_ThreadException method would be invoked
            Application.ThreadException += Application_ThreadException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            try
            {
                Logger.WriteLog(e.Exception);
                DisplayErrorBox(e.Exception.Message + Environment.NewLine + e.Exception.StackTrace);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Brings error screen to display the encountered Unhandled exception information
        /// </summary>
        /// <param name="stackTrace">Stacktrace Information to be displayed</param>
        private static void DisplayErrorBox(string stackTrace)
        {
            var err = new Error { ExceptionMessage = stackTrace };
            err.ShowDialog();
        }
    }
}