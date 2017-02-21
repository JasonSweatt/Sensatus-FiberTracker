using System;

namespace Sensatus.FiberTracker.Format
{
    public static class Formatting
    {
        /// <summary>
        /// To the date to database.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>System.String.</returns>
        public static string ToDateToDB(this string date)
        {
            var dt = Convert.ToDateTime(date);
            return dt.ToString("MMddyyyy");
        }

        /// <summary>
        /// Gets the current date.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string GetCurrentDate()
        {
            return DateTime.Now.ToString("MMddyyyy");
        }

        /// <summary>
        /// To the date to disp.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>System.String.</returns>
        public static string ToDateToDisp(this string date)
        {
            var dateString = new string[3];
            var dt = Convert.ToDateTime(date);
            dateString = dt.ToString("dd MMM yyyy").Split(Convert.ToChar(" "));
            return  $"{dateString[0]} {dateString[1]}, {dateString[2]}";
        }

        /// <summary>
        /// Gets the date from database date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>System.String.</returns>
        public static string GetDateFromDBDate(this string date)
        {
            var dateReturn = string.Empty;
            if (date.Trim().Length < 8)
                date = $"0{date.Trim()}";
            var month = date.Substring(0, 2);
            var date1 = date.Substring(2, 2);
            var year = date.Substring(4);
            dateReturn =  $"{month}/{date1}/{year}";
            dateReturn = dateReturn.ToDateToDisp();
            return dateReturn;
        }

        /// <summary>
        /// Gets the month.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>System.String.</returns>
        public static string GetMonth(this string date)
        {
            return date.ToDateToDB().Substring(0, 2);
        }

        /// <summary>
        /// Gets the year.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>System.String.</returns>
        public static string GetYear(this string date)
        {
            return date.ToDateToDB().Substring(4, 4);
        }

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>System.String.</returns>
        public static string GetDate(this string date)
        {
            return date.ToDateToDB().Substring(2, 2);
        }

        #region Core Formatting Metods

        /// <summary>
        /// Determines whether the specified value is numeric.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is numeric; otherwise, <c>false</c>.</returns>
        public static bool IsNumeric(this object value)
        {
            var retValue = false;
            if (value != null)
                retValue = value.ToString().IsNumeric();
            return retValue;
        }

        /// <summary>
        /// Determines whether the specified value is numeric.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is numeric; otherwise, <c>false</c>.</returns>
        public static bool IsNumeric(this string value)
        {
            var retValue = false;
            double result = 0;
            if (value != null)
                retValue = double.TryParse(value, out result);
            return retValue;
        }

        /// <summary>
        /// Determines whether the specified value is integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is integer; otherwise, <c>false</c>.</returns>
        public static bool IsInteger(this object value)
        {
            var retValue = false;
            if (value != null)
                retValue = value.ToString().IsInteger();
            return retValue;
        }

        /// <summary>
        /// Determines whether the specified value is integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is integer; otherwise, <c>false</c>.</returns>
        public static bool IsInteger(this string value)
        {
            var retValue = false;
            var result = 0;
            if (value != null)
                retValue = int.TryParse(value, out result);
            return retValue;
        }

        /// <summary>
        /// Determines whether the specified value is boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is boolean; otherwise, <c>false</c>.</returns>
        public static bool IsBoolean(this string value)
        {
            var retValue = false;
            var result = false;
            if (value != null)
                retValue = bool.TryParse(value, out result);
            return retValue;
        }

        /// <summary>
        /// Determines whether the specified value is boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is boolean; otherwise, <c>false</c>.</returns>
        public static bool IsBoolean(this object value)
        {
            var retValue = false;
            if (value != null)
                retValue = value.ToString().IsBoolean();
            return retValue;
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string GetString(this object value)
        {
            var retValue = string.Empty;
            if (value != null)
                retValue = value.ToString();
            return retValue;
        }

        /// <summary>
        /// Gets the integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        public static int GetInteger(this object value)
        {
            var retValue = 0;
            if (IsInteger(value))
                retValue = Convert.ToInt16(value);
            return retValue;
        }

        /// <summary>
        /// Gets the integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        public static int GetInteger(this string value)
        {
            return value.GetInteger();
        }

        /// <summary>
        /// Gets the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Double.</returns>
        public static double GetDouble(this object value)
        {
            double retValue = 0;
            if (IsNumeric(value))
                retValue = Convert.ToDouble(value);
            return retValue;
        }

        /// <summary>
        /// Gets the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Double.</returns>
        public static double GetDouble(this string value)
        {
            return value.GetDouble();
        }

        /// <summary>
        /// Gets the boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool GetBoolean(this object value)
        {
            var retValue = false;
            if (IsBoolean(value))
                retValue = Convert.ToBoolean(value);
            return retValue;
        }

        /// <summary>
        /// Gets the boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool GetBoolean(this string value)
        {
            return value.GetBoolean();
        }

        #endregion Core Formatting Metods
    }
}