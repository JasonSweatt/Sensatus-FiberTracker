using Sensatus.FiberTracker.DataAccess;
using System;
using System.Data;
using System.Linq;

namespace Sensatus.FiberTracker.BusinessLogic
{
    public class ItemWiseAnalytics
    {
        private Arch arch = new Arch();
        private DBHelper _dbHelper = new DBHelper();
        private CommonArch commonReportArch = new CommonArch();
        private ItemAnalyticArch itemAnalyticArch = new ItemAnalyticArch();
        private ReportArchitecture reportArch = new ReportArchitecture();

        public DataTable ItemWiseTrendReport(string month, string year)
        {
            var data = new DataTable();

            //Checks that Data exist for present month or not?
            var presentMMYYYY = arch.DecodeMonthYear(month, year);
            var dataExistForPresntMonth = commonReportArch.DataExistForMonth(month, year);

            //Checks that Data exist for prev month or not?
            var previousMonth = arch.GetPreviousMonth(month);
            var previousMonthYear = arch.GetPrevMonthsYear(month, year);
            var prevMMYYYY = arch.DecodeMonthYear(previousMonth, previousMonthYear);
            var dataExistForPrevMonth = commonReportArch.DataExistForMonth(previousMonth, previousMonthYear);

            //Checks that Data exist for next month or not?
            var nextMonth = arch.GetNextMonth(month);
            var nextMonthYear = arch.GetNextMonthsYear(month, year);
            var nextMMYYYY = arch.DecodeMonthYear(nextMonth, nextMonthYear);
            var dataExistForNextMonth = commonReportArch.DataExistForMonth(nextMonth, nextMonthYear);

            string[,] reportData = null;
            string[] columnName = null;

            if (dataExistForPresntMonth && dataExistForPrevMonth && dataExistForNextMonth)
            {
                columnName = new [] { "Item", previousMonth + " (Prev.)", "Trend1", month + " (Pres.)", "Trend2", nextMonth + " (Nxt.)" };
                reportData = new string[itemAnalyticArch.GetAllItems().Length, 6];
                reportData = GetReportData1(prevMMYYYY, presentMMYYYY, nextMMYYYY);
                data = arch.GetDataTableFrom2DArray(columnName, reportData);
            }
            else if (dataExistForPresntMonth && dataExistForPrevMonth && !dataExistForNextMonth)
            {
                columnName = new [] { "Item", previousMonth + " (Prev.)", "Trend", month + " (Pres.)" };
                reportData = new string[itemAnalyticArch.GetAllItems().Length, 4];
                reportData = GetReportData2(prevMMYYYY, presentMMYYYY);
                data = arch.GetDataTableFrom2DArray(columnName, reportData);
            }
            else if (dataExistForPresntMonth && !dataExistForPrevMonth && !dataExistForNextMonth)
            {
                columnName = new [] { "Item", month + " (Pres.)" };
                data = itemAnalyticArch.MonthlyReportData(month, year);
            }
            else if (dataExistForPresntMonth && !dataExistForPrevMonth && dataExistForNextMonth)
            {
                columnName = new [] { "Item", month + " (Pres.)", "Trend", nextMonth + " (Nxt.)" };
                reportData = new string[reportArch.GetAllUsers().Length, 4];
                reportData = GetReportData2(presentMMYYYY, nextMMYYYY);
                data = arch.GetDataTableFrom2DArray(columnName, reportData);
            }

            return data;
        }

        private string[,] GetReportData1(string previousMonthYear, string presentMonthYear, string nextMonthYear)
        {
            var numberOfItems = itemAnalyticArch.GetAllItems().Length;
            var reportData = new string[numberOfItems + 1, 6];
            var expForItem = itemAnalyticArch.GetAllItems();
            var prevMontExpense = itemAnalyticArch.GetExpenseForItems(previousMonthYear);
            var trndPreviousPresent = new string[prevMontExpense.Length];
            var presentMontExpense = itemAnalyticArch.GetExpenseForItems(presentMonthYear);
            var trendPresentVsNextMonth = new string[presentMontExpense.Length];
            var nextMontExpense = itemAnalyticArch.GetExpenseForItems(nextMonthYear);

            var sumPrevMonthExp = GetTotalExpense(prevMontExpense);
            var sumPresentMonthExp = GetTotalExpense(presentMontExpense);
            var sumNextMonthExp = GetTotalExpense(nextMontExpense);

            for (var row = 0; row <= numberOfItems; row++)
                if (row == numberOfItems)
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
                    reportData[row, 0] = expForItem[row];
                    reportData[row, 1] = prevMontExpense[row];
                    reportData[row, 2] = GetTrendSymbol(prevMontExpense[row], presentMontExpense[row]);
                    reportData[row, 3] = presentMontExpense[row];
                    reportData[row, 4] = GetTrendSymbol(presentMontExpense[row], nextMontExpense[row]);
                    reportData[row, 5] = nextMontExpense[row];
                }

            return reportData;
        }

        private double GetTotalExpense(string[] expenses)
        {
            return expenses.Aggregate(0.0, (current, value) => current + Convert.ToDouble(value));
        }

        private string GetTrendSymbol(string month1, string month2)
        {
            var firstMonth = Convert.ToDouble(month1);
            var secMonth = Convert.ToDouble(month2);
            if (firstMonth > secMonth)
                return " > ";
            return firstMonth < secMonth ? " < " : " = ";
        }

        private string[,] GetReportData2(string previousMonthYear, string presentMonthYear)
        {
            var numberOfItems = itemAnalyticArch.GetAllItems().Length;
            var reportData = new string[numberOfItems + 1, 4];
            var expBy = itemAnalyticArch.GetAllItems();
            var prevMontExpense = itemAnalyticArch.GetExpenseForItems(previousMonthYear);
            var trndPreviousPresent = new string[prevMontExpense.Length];
            var presentMontExpense = itemAnalyticArch.GetExpenseForItems(presentMonthYear);

            var sumPrevMonthExp = GetTotalExpense(prevMontExpense);
            var sumPresentMonthExp = GetTotalExpense(presentMontExpense);

            for (var row = 0; row <= itemAnalyticArch.GetAllItems().Length; row++)
                if (row == numberOfItems)
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
            return reportData;
        }
    }
}