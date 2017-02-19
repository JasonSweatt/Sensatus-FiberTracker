using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sensatus.FiberTracker.BusinessLogic;

namespace Sensatus.FiberTracker.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Add Exception Handler so that for every thread exception Application_ThreadException method would be invoked
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
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
            Error err = new Error();
            err.ExceptionMessage = stackTrace;
            err.ShowDialog();
        }                      


    }              
}