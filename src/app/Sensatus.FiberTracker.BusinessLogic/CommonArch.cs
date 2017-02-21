using Sensatus.FiberTracker.DataAccess;
using System;

namespace Sensatus.FiberTracker.BusinessLogic
{
    /// <summary>
    /// Class CommonArch.
    /// </summary>
    public class CommonArch
    {
        /// <summary>
        /// The database helper
        /// </summary>
        private readonly DBHelper _dbHelper = new DBHelper();
        /// <summary>
        /// The arch
        /// </summary>
        private readonly Arch _arch = new Arch();

        /// <summary>
        /// Datas the exist for month.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool DataExistForMonth(string month, string year)
        {
            var monthYear = _arch.DecodeMonthYear(month, year);
            var query = $"SELECT COUNT(*) FROM Expense_Details WHERE MonthYear = '{monthYear}' AND IsDeleted = 0";
            return Convert.ToInt16(_dbHelper.ExecuteScalar(query).ToString()) > 0;
        }
    }
}