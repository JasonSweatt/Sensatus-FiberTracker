using Sensatus.FiberTracker.Configurations;
using Sensatus.FiberTracker.DataAccess;
using System;
using System.Collections;
using System.Data;

namespace Sensatus.FiberTracker.BusinessLogic
{
    public class MonthlyReport
    {
        private Arch arch = new Arch();
        private DBHelper _dbHelper = new DBHelper();

        public DataSet MonthlyReportData(string month, string Year)
        {
            var monthYear = arch.DecodeMonthYear(month, Year);
            var query = "SELECT Distinct(ExpenseBy) AS ExpBy, UserInfo.FirstName + ' ' + UserInfo.LastName AS ExpenseBy, SUM(ExpenseAmount) AS TotalExpense FROM " +
            "ExpenseDetails,UserInfo WHERE " +
            "ExpenseDetails.ExpenseBy=UserInfo.UserId AND " +
            $"ExpenseDetails.MonthYear='{monthYear}' AND ExpenseDetails.IsDeleted = 0 " +
            "GROUP BY ExpenseBy, UserInfo.FirstName + ' ' + UserInfo.LastName";
            var dataSet = _dbHelper.ExecuteDataSet(query);
            return dataSet;
        }

        public DataSet MonthlyReportData(string month, string Year, ReportType reportType)
        {
            var monthYear = arch.DecodeMonthYear(month, Year);
            var query = string.Empty;
            switch (reportType)
            {
                case ReportType.Individual:
                    query = "SELECT Distinct(ExpenseBy) ,UserInfo.FirstName AS ExpenseBy, SUM(ExpenseAmount) AS TotalExpense FROM " +
                            "ExpenseDetails,UserInfo WHERE " +
                            "ExpenseDetails.ExpenseBy=UserInfo.UserId AND " +
                            $"ExpenseDetails.MonthYear='{monthYear}' AND ExpenseDetails.IsDeleted = 0 " +
                            "GROUP BY ExpenseBy, UserInfo.FirstName";
                    break;

                case ReportType.ItemWise:
                    query = "SELECT Item_Details.Item_Name AS ItemName, SUM(ExpenseAmount) AS Expense FROM " +
                            "ExpenseDetails,Item_Details WHERE " +
                            "ExpenseDetails.Item_Id=Item_Details.Item_Id AND " +
                            $"ExpenseDetails.MonthYear='{monthYear}' AND ExpenseDetails.IsDeleted = 0 " +
                            "GROUP BY Item_Details.Item_Name ";
                    break;
            }
            var dataSet = _dbHelper.ExecuteDataSet(query);
            return dataSet;
        }

        public string GeneralDetails(string month, string Year)
        {
            var totalExpense = GetTotalExpense(month, Year);
            var daysInMonth = DateTime.DaysInMonth(Convert.ToInt16(Year), arch.GetMonth(month));
            var participents = GetNumberOfParticipents().ToString();
            var perDayExp = Math.Round(Convert.ToDouble(totalExpense) / daysInMonth, 2);
            var individualExp = Math.Round(Convert.ToDouble(totalExpense) / Convert.ToDouble(participents), 2);
            var reportText = $"{Resources.Labels.TotalExpense} : {totalExpense} {ApplicationConfiguration.ExpenseCCY}{Environment.NewLine}";
            reportText = $"{reportText}{Resources.Labels.Participents} : {participents}{Environment.NewLine}";
            reportText = $"{reportText}{Resources.Labels.Days} : {daysInMonth} {Resources.Labels.Days}{Environment.NewLine}";
            reportText = $"{reportText}{Resources.Labels.IndividualExpense} : {individualExp} {ApplicationConfiguration.ExpenseCCY}{Environment.NewLine}";
            reportText = $"{reportText}{Resources.Labels.PerDayExpense} : {perDayExp} {ApplicationConfiguration.ExpenseCCY}";
            return reportText;
        }

        public string GetTotalExpense(string month, string year)
        {
            var query = $"SELECT SUM(ExpenseAmount) FROM ExpenseDetails WHERE MonthYear='{arch.DecodeMonthYear(month, year)}' AND IsDeleted = 0";
            return _dbHelper.ExecuteScalar(query).ToString();
        }

        public int GetNumberOfParticipents()
        {
            var query = "SELECT COUNT(*) FROM UserInfo WHERE UserId <> 1 AND IsActive = 1";
            return Convert.ToInt16(_dbHelper.ExecuteScalar(query).ToString());
        }

        public string[] ReportFinalizeDetails(string month, string year)
        {
            var query = $"SELECT DISTINCT(Finalized) FROM ExpenseDetails WHERE MonthYear='{arch.DecodeMonthYear(month, year)}' AND IsDeleted=0 AND Finalized <> 0";
            var finalizeDetails = new ArrayList();
            if (_dbHelper.GetDataInArrayList(query) != null)
                finalizeDetails = _dbHelper.GetDataInArrayList(query);
            else
                finalizeDetails.Add("N\\A");

            var details = new string[finalizeDetails.Count];
            for (var index = 0; index < finalizeDetails.Count; index++)
                details[index] = finalizeDetails[index].ToString();

            return details;
        }

        public enum ReportType
        {
            Individual, ItemWise
        }
    }
}