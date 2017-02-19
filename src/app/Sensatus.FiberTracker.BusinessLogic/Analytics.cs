using System;
using System.Data;
using System.Linq;
using Sensatus.FiberTracker.DataAccess;

namespace Sensatus.FiberTracker.BusinessLogic
{
    public class Analytics
    {
        private readonly Arch _arch = new Arch();
        private DBHelper _dbHelper = new DBHelper();
        private readonly ReportArchitecture _reportArch = new ReportArchitecture();
        private readonly CommonArch _commonReportArch = new CommonArch();
        private readonly ItemWiseAnalytics _itemAnalytics = new ItemWiseAnalytics();

        public DataTable AnalyticReport(string month, string year, AnalyticReportType reportType)
        {
            return reportType.ToString().Equals("Individual") ? MonthlyTrendReport(month, year) : _itemAnalytics.ItemWiseTrendReport(month, year);
        }

        public DataTable MonthlyTrendReport(string month, string year)
        {
            var dataTable = new DataTable();

            //Checks that Data exist for present month or not?
            var presentMmyyyy = _arch.DecodeMonthYear(month, year);
            var dataExistForPresntMonth = _commonReportArch.DataExistForMonth(month, year);

            //Checks that Data exist for prev month or not?            
            var previousMonth = _arch.GetPreviousMonth(month);
            var previousMonthYear = _arch.GetPrevMonthsYear(month, year);
            var prevMmyyyy = _arch.DecodeMonthYear(previousMonth, previousMonthYear);
            var dataExistForPrevMonth = _commonReportArch.DataExistForMonth(previousMonth, previousMonthYear);

            //Checks that Data exist for next month or not?
            var nextMonth = _arch.GetNextMonth(month);
            var nextMonthYear = _arch.GetNextMonthsYear(month, year);
            var nextMmyyyy = _arch.DecodeMonthYear(nextMonth, nextMonthYear);
            var dataExistForNextMonth = _commonReportArch.DataExistForMonth(nextMonth, nextMonthYear);

            string[,] reportData = null;
            string[] columnName = null ;

            if (dataExistForPresntMonth && dataExistForPrevMonth && dataExistForNextMonth)
            {
                columnName = new [] { "Expensed By", previousMonth + " (Prev.)", "Trend1", month + " (Pres.)", "Trend2", nextMonth + " (Nxt.)" };
                reportData = new string[_reportArch.GetAllUsers().Length, 6];
                reportData = GetReportData1(prevMmyyyy, presentMmyyyy, nextMmyyyy);
                dataTable = _arch.GetDataTableFrom2DArray(columnName, reportData);
            }
            else if (dataExistForPresntMonth && dataExistForPrevMonth && !dataExistForNextMonth)
            {
                columnName = new [] { "Expensed By", previousMonth + " (Prev.)", "Trend", month + " (Pres.)" };
                reportData = new string[_reportArch.GetAllUsers().Length, 4];
                reportData = GetReportData2(prevMmyyyy, presentMmyyyy);
                dataTable = _arch.GetDataTableFrom2DArray(columnName, reportData);
            }
            else if (dataExistForPresntMonth && !dataExistForPrevMonth && !dataExistForNextMonth)
            {
                columnName = new [] { "Expensed By", month + " (Pres.)" };
                dataTable = _reportArch.MonthlyReportData(month, year);
            }
            else if (dataExistForPresntMonth && !dataExistForPrevMonth && dataExistForNextMonth)
            {
                columnName = new [] { "Expensed By", month + " (Pres.)", "Trend", nextMonth + " (Nxt.)" };
                reportData = new string[_reportArch.GetAllUsers().Length, 4];
                reportData = GetReportData2(presentMmyyyy, nextMmyyyy);
                dataTable = _arch.GetDataTableFrom2DArray(columnName, reportData);
            }

            return dataTable;
        }

        private string[,] GetReportData1(string previousMonthYear, string presentMonthYear,  string nextMonthYear)
        {
            var numOfUsers = _reportArch.GetAllUsers().Length;
            var reportData = new string[numOfUsers + 1, 6];
            var expBy = _reportArch.GetAllUsers();
            var prevMontExpense = _reportArch.GetExpenseByUsers(previousMonthYear);
            var trndPreviousPresent = new string[prevMontExpense.Length];
            var presentMontExpense = _reportArch.GetExpenseByUsers(presentMonthYear);
            var trendPresentVsNextMonth = new string[presentMontExpense.Length];
            var nextMontExpense = _reportArch.GetExpenseByUsers(nextMonthYear);

            var sumPrevMonthExp = GetTotalExpense(prevMontExpense);
            var sumPresentMonthExp = GetTotalExpense(presentMontExpense);
            var sumNextMonthExp = GetTotalExpense(nextMontExpense);

            for (var row = 0; row <= numOfUsers; row++)
            {
                if (row == numOfUsers)
                {
                    reportData[row, 0] = "T O T A L :";
                    reportData[row, 1] = sumPrevMonthExp.ToString();
                    reportData[row, 2] = GetTrendSymbol(sumPrevMonthExp.ToString(), sumPresentMonthExp.ToString());
                    reportData[row, 3] = sumPresentMonthExp.ToString();
                    reportData[row, 4] = GetTrendSymbol(sumPresentMonthExp.ToString(), sumNextMonthExp.ToString());
                    reportData[row, 5] = sumNextMonthExp.ToString();
                }
                else
                {
                    reportData[row, 0] = expBy[row];
                    reportData[row, 1] = prevMontExpense[row];
                    reportData[row, 2] = GetTrendSymbol(prevMontExpense[row], presentMontExpense[row]);
                    reportData[row, 3] = presentMontExpense[row];
                    reportData[row, 4] = GetTrendSymbol(presentMontExpense[row], nextMontExpense[row]);
                    reportData[row, 5] = nextMontExpense[row];
                }
            }
            return reportData;
        }

        private double GetTotalExpense(string[] expenses)
        {
            return expenses.Aggregate(0.0, (current, expense) => current + Convert.ToDouble(expense));
        }

        private string[,] GetReportData2(string previousMonthYear, string presentMonthYear)
        {
            var numOfUsers = _reportArch.GetAllUsers().Length;
            var reportData = new string[numOfUsers + 1, 4];
            var expBy = _reportArch.GetAllUsers();
            var prevMontExpense = _reportArch.GetExpenseByUsers(previousMonthYear);
            var trndPreviousPresent = new string[prevMontExpense.Length];
            var presentMontExpense = _reportArch.GetExpenseByUsers(presentMonthYear);

            var sumPrevMonthExp = GetTotalExpense(prevMontExpense);
            var sumPresentMonthExp = GetTotalExpense(presentMontExpense);

            for (var row = 0; row <= _reportArch.GetAllUsers().Length; row++)
            {
                if (row == numOfUsers)
                {
                    reportData[row, 0] = "T O T A L :";
                    reportData[row, 1] = sumPrevMonthExp.ToString();
                    reportData[row, 2] = GetTrendSymbol(sumPrevMonthExp.ToString(), sumPresentMonthExp.ToString());
                    reportData[row, 3] = sumPresentMonthExp.ToString();
                }
                else
                {
                    reportData[row, 0] = expBy[row];
                    reportData[row, 1] = prevMontExpense[row];
                    reportData[row, 2] = GetTrendSymbol(prevMontExpense[row], presentMontExpense[row]);
                    reportData[row, 3] = presentMontExpense[row];
                }
            }
            return reportData;
        }

        private string GetTrendSymbol(string month1, string month2)
        {
            var firstMonth = Convert.ToDouble(month1);
            var secMonth = Convert.ToDouble(month2);
            if (firstMonth > secMonth)
                return " > ";
            return firstMonth < secMonth ? " < " : " - ";
        }      

        public enum AnalyticReportType
        {
            Individual,ItemWise,OverAll
        }
    }
}