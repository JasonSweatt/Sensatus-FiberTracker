using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Sensatus.FiberTracker.Formatting;

namespace Sensatus.FiberTracker.Configurations
{
    public static class ApplicationConfiguration
    {
        const string DEFAULT_CONNECTION_KEY = "defaultConnection";
        const string LOGGING_ENABLED_KEY = "logging";
        const string TRACING_ENABLED_KEY = "tracing";
        const string DATE_WISE_LOG_TRACE_KEY = "dateWiseLogTrace";
        const string EXPENSE_CCY_KEY = "expenseCCY";
        const string SUPPORT_MAIL_KEY = "supportMail";
        const string SUPPORT_URL_KEY = "supportURL";            

        /// <summary>
        /// Gets default connection being used.
        /// </summary>
        public static string DefaultConnection
        {
            get
            {
                return ConfigurationManager.AppSettings[DEFAULT_CONNECTION_KEY];
            }
        }

        /// <summary>
        /// Gets DB provider from app.config file
        /// </summary>
        public static string DBProvider
        {
            get
            {
                return ConfigurationManager.ConnectionStrings[DefaultConnection].ProviderName ;
            }
        }

        /// <summary>
        /// Gets connection string from app.config file
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings[DefaultConnection].ConnectionString;
            }
        }

        /// <summary>
        /// Gets whether logging is enabled or not
        /// </summary>
        public static bool LoggingEnabled
        {
            get
            {
                return DataFormat.GetBoolean(ConfigurationManager.AppSettings[LOGGING_ENABLED_KEY]);
            }
        }

        /// <summary>
        /// Gets whether tracing is enabled or not
        /// </summary>
        public static bool TracingEnabled
        {
            get
            {
                return DataFormat.GetBoolean(ConfigurationManager.AppSettings[TRACING_ENABLED_KEY]);
            }
        }

        /// <summary>
        /// Type of Logging/ Tracing
        /// </summary>
        public enum LogTraceType
        {
            SingleFile,
            DateWise
        }

        /// <summary>
        /// Gets Type of Log/ Trace i.e. Separate file of one file for each date
        /// </summary>
        public static LogTraceType LogTraceSetting
        {
            get
            {
                return  DataFormat.GetBoolean(ConfigurationManager.AppSettings[DATE_WISE_LOG_TRACE_KEY]) ? LogTraceType.DateWise : LogTraceType.SingleFile;
            }
        }

        /// <summary>
        /// Gets Expense Currency from app.config
        /// </summary>
        public static string ExpenseCCY
        {
            get
            {
                return DataFormat.GetString(ConfigurationManager.AppSettings[EXPENSE_CCY_KEY]) ;
            }
        }

        /// <summary>
        /// Gets support mail from app.config
        /// </summary>
        public static string SupportMail
        {
            get
            {
                return DataFormat.GetString(ConfigurationManager.AppSettings[SUPPORT_MAIL_KEY]) ;
            }
        }


        /// <summary>
        /// Gets support URL
        /// </summary>
        public static string SupportURL
        {
            get
            {
                return DataFormat.GetString(ConfigurationManager.AppSettings[SUPPORT_URL_KEY]);
            }
        }


    }
}
