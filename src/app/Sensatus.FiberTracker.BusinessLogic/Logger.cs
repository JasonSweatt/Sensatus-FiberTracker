using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System.IO;
using System;
using Sensatus.FiberTracker.Configurations;


namespace Sensatus.FiberTracker.BusinessLogic
{
    public class Logger
    {
        private const string APPENDER_NAME = "ApplicationLogger";
        private static log4net.ILog log = null;

        /// <summary>
        /// Writes the stack trace into the Log file for the passed exception.
        /// </summary>
        /// <param name="ex">Exception to be written into Log file</param>
        public static void WriteLog(Exception ex)
        {
            if (ApplicationConfiguration.LoggingEnabled)
            {
                string fileName = GetFileName(FileType.Log);
                FileAppender fileAppender = CreateAppender(fileName);

                CreateLogger(fileAppender, APPENDER_NAME);
                log = LogManager.GetLogger(APPENDER_NAME);

                string stackTrace = GetStackTraceInfo();
                log.Info(stackTrace, ex);
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
                string fileName = GetFileName(FileType.Trace);
                FileAppender fileAppender = CreateAppender(fileName);

                CreateLogger(fileAppender, APPENDER_NAME);
                log = LogManager.GetLogger(APPENDER_NAME);
                log.Info(traceInfo + Environment.NewLine);
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
                string fileName = GetFileName(FileType.Trace);
                FileAppender fileAppender = CreateAppender(fileName);

                CreateLogger(fileAppender, APPENDER_NAME);
                log = LogManager.GetLogger(APPENDER_NAME);

                string stackTrace = Environment.NewLine + "Screen/Form Name : " + formName + Environment.NewLine;
                stackTrace += "Method/ Action/ Routine Invoked : " + traceInfo + Environment.NewLine;
                stackTrace += "Start Time : " + DateTime.Now.ToString() + Environment.NewLine;
                stackTrace += "Username : " + SessionParameters.UserName + Environment.NewLine;
                log.Info(stackTrace);
            }
        }


        private static string GetStackTraceInfo()
        {
            string stackTraceInfo = Environment.NewLine;

            stackTraceInfo += "************************************************************" + Environment.NewLine;
            stackTraceInfo = (stackTraceInfo + "Username : ") + SessionParameters.UserName + Environment.NewLine;
            stackTraceInfo = (stackTraceInfo + "Date & Time : ") + DateTime.Now.ToString() + Environment.NewLine;
            stackTraceInfo += "************************************************************" + Environment.NewLine;

            return stackTraceInfo;
        }


        private enum FileType
        {
            Log,
            Trace
        }

        private static string GetFileName(FileType fileType)
        {
            string dirName = (fileType == Logger.FileType.Log ? Environment.CurrentDirectory + @"\Diagnostics\Log" : Environment.CurrentDirectory + @"\Diagnostics\Trace");

            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            string fileName = ApplicationConfiguration.LogTraceSetting == ApplicationConfiguration.LogTraceType.DateWise ? DateTime.Now.ToString("dd-MMM-yyyy") + ".txt" : (fileType == FileType.Log ? "Log.txt" : "Trace.txt");                      
            return (dirName + "\\") + fileName;            
            
        }


        private static FileAppender CreateAppender(string fileName)
        {
            RollingFileAppender rollingFileAppender = new RollingFileAppender();
            FileAppender fileAppender = new FileAppender();
            PatternLayout patternLayOut = new PatternLayout();
            
            patternLayOut.ConversionPattern = "%d %m%n";
            patternLayOut.ActivateOptions();
            fileAppender.Layout = patternLayOut;
            fileAppender.AppendToFile = true;
            fileAppender.File = fileName;
            fileAppender.Name = APPENDER_NAME;
            fileAppender.ActivateOptions();

            return fileAppender;
        }

        private static void CreateLogger(FileAppender fileAppender, string loggerName)
        {
            log4net.Repository.Hierarchy.Hierarchy hierarchy = (log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetLoggerRepository();
            log4net.Repository.Hierarchy.Logger logger = (log4net.Repository.Hierarchy.Logger)hierarchy.GetLogger(APPENDER_NAME);
            logger.RemoveAllAppenders();
            logger.AddAppender(fileAppender);
            hierarchy.Configured = true;
        }

    }
}