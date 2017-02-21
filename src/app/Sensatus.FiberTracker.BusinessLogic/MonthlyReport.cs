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
            var dsReportData = new DataSet();
            var monthYear = arch.DecodeMonthYear(month, Year);

            var Query = "SELECT Distinct(Exp_By) as ExpBy,User_Info.First_Name + ' ' +User_Info.Last_Name as ExpenseBy, Sum(Exp_Amount) as TotalExpense from " +
            "Expense_Details,User_Info Where " +
            "Expense_Details.Exp_By=User_Info.User_Id AND " +
            "Expense_Details.MonthYear='" + monthYear + "' AND Expense_Details.IsDeleted=0" +
            " Group by Exp_By, User_Info.First_Name + ' ' +User_Info.Last_Name ";

            dsReportData = _dbHelper.ExecuteDataSet(Query);

            return dsReportData;
        }

        public DataSet MonthlyReportData(string month, string Year, ReportType reportType)
        {
            var dsReportData = new DataSet();
            var monthYear = arch.DecodeMonthYear(month, Year);
            var Query = string.Empty;

            switch (reportType)
            {
                case ReportType.Individual:
                    Query = "SELECT Distinct(Exp_By) ,User_Info.First_Name as ExpenseBy, Sum(Exp_Amount) as TotalExpense from " +
                            "Expense_Details,User_Info Where " +
                            "Expense_Details.Exp_By=User_Info.User_Id AND " +
                            "Expense_Details.MonthYear='" + monthYear + "' AND Expense_Details.IsDeleted=0" +
                            " Group by Exp_By, User_Info.First_Name";
                    break;

                case ReportType.ItemWise:
                    Query = "SELECT Item_Details.Item_Name as ItemName, Sum(Exp_Amount) as Expense from " +
                            "Expense_Details,Item_Details Where " +
                            "Expense_Details.Item_Id=Item_Details.Item_Id AND " +
                            "Expense_Details.MonthYear='" + monthYear + "' AND Expense_Details.IsDeleted=0 " +
                            "Group by Item_Details.Item_Name ";
                    break;
            }

            dsReportData = _dbHelper.ExecuteDataSet(Query);
            return dsReportData;
        }

        public string GeneralDetails(string month, string Year)
        {
            var reportText = string.Empty;
            var totalExpense = GetTotalExpense(month, Year);
            var daysInMonth = DateTime.DaysInMonth(Convert.ToInt16(Year), arch.GetMonth(month));
            var participents = GetNumberOfParticipents().ToString();
            var perDayExp = Math.Round(Convert.ToDouble(totalExpense) / daysInMonth, 2);
            var individualExp = Math.Round(Convert.ToDouble(totalExpense) / Convert.ToDouble(participents), 2);

            reportText = "Total Expense : " + totalExpense + " " + ApplicationConfiguration.ExpenseCCY + Environment.NewLine;
            reportText = reportText + "Participents: " + participents + Environment.NewLine;
            reportText = reportText + "Days : " + daysInMonth.ToString() + " Days" + Environment.NewLine;
            reportText = reportText + "Individual Expense : " + individualExp.ToString() + " " + ApplicationConfiguration.ExpenseCCY + Environment.NewLine;
            reportText = reportText + "Per day expense : " + perDayExp.ToString() + " " + ApplicationConfiguration.ExpenseCCY;

            return reportText;
        }

        public string GetTotalExpense(string month, string year)
        {
            var Query = "SELECT Sum(Exp_Amount) from Expense_Details where MonthYear='" + arch.DecodeMonthYear(month, year) + "' And IsDeleted=0";
            return _dbHelper.ExecuteScalar(Query).ToString();
        }

        public int GetNumberOfParticipents()
        {
            var Query = "SELECT Count(*) from User_Info where User_Id<>1 and IsActive=1";
            return Convert.ToInt16(_dbHelper.ExecuteScalar(Query).ToString());
        }

        public string[] ReportFinalizeDetails(string month, string year)
        {
            var Query = "SELECT Distinct(Finalized) from Expense_Details where MonthYear='" + arch.DecodeMonthYear(month, year) + "' And IsDeleted=0 And Finalized <> 0";
            var finalizeDetails = new ArrayList();

            if (_dbHelper.GetDataInArrayList(Query) != null)
                finalizeDetails = _dbHelper.GetDataInArrayList(Query);
            else
                finalizeDetails.Add("N\\A");

            var strDetails = new string[finalizeDetails.Count];

            for (var i = 0; i < finalizeDetails.Count; i++)
                strDetails[i] = finalizeDetails[i].ToString();

            return strDetails;
        }

        public enum ReportType
        {
            Individual, ItemWise
        }
    }
}