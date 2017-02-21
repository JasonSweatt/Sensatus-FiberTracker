using log4net;
using log4net.Appender;
using log4net.Layout;
using Sensatus.FiberTracker.Configurations;
using System;
using System.IO;

namespace Sensatus.FiberTracker.BusinessLogic
{
    public class Logger
    {
        private const string APPENDER_NAME = "ApplicationLogger";
        private static ILog _log = null;

        /// <summary>
        /// Writes the stack trace into the Log file for the passed exception.
        /// </summary>
        /// <param name="ex">Exception to be written into Log file</param>
        public static void WriteLog(Exception ex)
        {
            if (ApplicationConfiguration.LoggingEnabled)
            {
                var fileName = GetFileName(FileType.Log);
                var fileAppender = CreateAppender(fileName);

                CreateLogger(fileAppender, APPENDER_NAME);
                _log = LogManager.GetLogger(APPENDER_NAME);

                var stackTrace = GetStackTraceInfo();
                _log.Info(stackTrace, ex);
            }
        }

        /// <summary>
        /// Write traceInfo to the trace file.
        /// </summary>
        /// <param name="traceInfo">Information needs to be traced</param>
        public static void WriteTrace(string traceInfo)
        {
            if (ApplicationConfiguration.TracingEnabled)
            {
                var fileName = GetFileName(FileType.Trace);
                var fileAppender = CreateAppender(fileName);

                CreateLogger(fileAppender, APPENDER_NAME);
                _log = LogManager.GetLogger(APPENDER_NAME);
                _log.Info(traceInfo + Environment.NewLine);
            }
        }

        /// <summary>
        /// Writes the information(s) to the trace file.
        /// </summary>
        /// <param name="formName">Screen name</param>
        /// <param name="traceInfo">Trace Information to be written into trace file</param>
        public static void WriteTrace(string formName, string traceInfo)
        {
            if (ApplicationConfiguration.TracingEnabled)
            {
                var fileName = GetFileName(FileType.Trace);
                var fileAppender = CreateAppender(fileName);

                CreateLogger(fileAppender, APPENDER_NAME);
                _log = LogManager.GetLogger(APPENDER_NAME);

                var stackTrace = Environment.NewLine + "Screen/Form Name : " + formName + Environment.NewLine;
                stackTrace += "Method/ Action/ Routine Invoked : " + traceInfo + Environment.NewLine;
                stackTrace += "Start Time : " + DateTime.Now.ToString() + Environment.NewLine;
                stackTrace += "Username : " + SessionParameters.UserName + Environment.NewLine;
                _log.Info(stackTrace);
            }
        }

        /// <summary>
        /// Gets the stack trace information.
        /// </summary>
        /// <returns>System.String.</returns>
        private static string GetStackTraceInfo()
        {
            var stackTraceInfo = Environment.NewLine;
            stackTraceInfo += "************************************************************" + Environment.NewLine;
            stackTraceInfo = stackTraceInfo + "Username : " + SessionParameters.UserName + Environment.NewLine;
            stackTraceInfo = stackTraceInfo + "Date & Time : " + DateTime.Now.ToString() + Environment.NewLine;
            stackTraceInfo += "************************************************************" + Environment.NewLine;
            return stackTraceInfo;
        }

        /// <summary>
        /// Enum FileType
        /// </summary>
        private enum FileType
        {
            Log,
            Trace
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <param name="fileType">Type of the file.</param>
        /// <returns>System.String.</returns>
        private static string GetFileName(FileType fileType)
        {
            var dirName = fileType == FileType.Log ? Environment.CurrentDirectory + @"\Diagnostics\Log" : Environment.CurrentDirectory + @"\Diagnostics\Trace";
            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);
            var fileName = ApplicationConfiguration.LogTraceSetting == ApplicationConfiguration.LogTraceType.DateWise ? DateTime.Now.ToString("dd-MMM-yyyy") + ".txt" : (fileType == FileType.Log ? "Log.txt" : "Trace.txt");
            return dirName + "\\" + fileName;
        }

        /// <summary>
        /// Creates the appender.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>FileAppender.</returns>
        private static FileAppender CreateAppender(string fileName)
        {
            var rollingFileAppender = new RollingFileAppender();
            var fileAppender = new FileAppender();
            var patternLayOut = new PatternLayout { ConversionPattern = "%d %m%n" };
            patternLayOut.ActivateOptions();
            fileAppender.Layout = patternLayOut;
            fileAppender.AppendToFile = true;
            fileAppender.File = fileName;
            fileAppender.Name = APPENDER_NAME;
            fileAppender.ActivateOptions();

            return fileAppender;
        }

        /// <summary>
        /// Creates the logger.
        /// </summary>
        /// <param name="fileAppender">The file appender.</param>
        /// <param name="loggerName">Name of the logger.</param>
        private static void CreateLogger(FileAppender fileAppender, string loggerName)
        {
            var hierarchy = (log4net.Repository.Hierarchy.Hierarchy)LogManager.GetLoggerRepository();
            var logger = (log4net.Repository.Hierarchy.Logger)hierarchy.GetLogger(APPENDER_NAME);
            logger.RemoveAllAppenders();
            logger.AddAppender(fileAppender);
            hierarchy.Configured = true;
        }
    }
}