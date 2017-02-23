using Sensatus.FiberTracker.DataAccess;
using Sensatus.FiberTracker.Formatting;
using System;
using System.Data;

namespace Sensatus.FiberTracker.BusinessLogic
{
    public class ExpenseReport
    {
        private DBHelper _dbHelper = new DBHelper();

        public DataTable GetDataTableForDisplay()
        {
            var table = new DataTable();
            var arch = new Arch();
            string[] columnName = { "Sl", "expby", "HasPaid", "PayGet", "Amount" };
            var reportData = GetReportArray();
            if (reportData != null)
                table = arch.GetDataTableFrom2DArray(columnName, reportData);
            return table;
        }

        private string[,] GetReportArray()
        {
            string[,] arrReturn = null;

            var ds = new DataSet();
            var serialNumber = 0;

            var totalExpense = GetTotalExpenses();
            var individualExpense = GetIndividualExpense();

            var allUserNames = GetAllUsers();
            if (allUserNames == null) return null;

            var expenseAmount = GetExpenseByUsers();
            arrReturn = new string[allUserNames.Length, 5];

            for (var i = 0; i < allUserNames.Length; i++)
            {
                serialNumber = i + 1;
                arrReturn[i, 0] = serialNumber.ToString();               //Serial Number
                arrReturn[i, 1] = allUserNames[i];                       //Name
                arrReturn[i, 2] = expenseAmount[i];                      //Amount Paid
                arrReturn[i, 3] = GetPayGetOption(expenseAmount[i]);     //Pay Get Option
                arrReturn[i, 4] = GetAmount(expenseAmount[i]);           //Amount Due
            }

            return arrReturn;
        }

        public string[] GetAllUsers()
        {
            string[] nameArray = null;
            var helper = new DBHelper();
            var query = "SELECT FirstName AS Name FROM UserInfo WHERE IsActive = 1 AND RoleId <> 1";
            var dataTable = helper.ExecuteDataTable(query);
            var rowCount = dataTable.Rows.Count;
            if (rowCount > 0)
            {
                nameArray = new string[rowCount];
                for (var index = 0; index < rowCount; index++)
                    nameArray[index] = dataTable.Rows[index][0].ToString();
            }
            return nameArray;
        }

        private string[] GetUsersIds()
        {
            string[] userIdArray = null;
            var query = "SELECT UserId FROM UserInfo WHERE IsActive = 1 AND RoleId <> 1";
            var dataTable = _dbHelper.ExecuteDataTable(query);
            var rowCount = dataTable.Rows.Count;
            if (rowCount > 0)
            {
                userIdArray = new string[rowCount];
                for (var index = 0; index < rowCount; index++)
                    userIdArray[index] = dataTable.Rows[index][0].ToString();
            }
            return userIdArray;
        }

        private string[] GetExpenseByUsers()
        {
            var usersIds = GetUsersIds();
            if (usersIds == null)
                return null;

            object result = null;
            var expenseAmount = new string[usersIds.Length];
            for (var index = 0; index < usersIds.Length; index++)
            {
                var query = $"SELECT SUM(ExpenseAmount) FROM ExpenseDetails WHERE Finalized = 0 AND IsDeleted = 0 AND ExpenseBy = {usersIds[index]}";
                result = _dbHelper.ExecuteScalar(query);
                if (result != null)
                {
                    expenseAmount[index] = result.ToString();
                    if (expenseAmount[index].Equals(string.Empty))
                        expenseAmount[index] = "0";
                }
            }
            return expenseAmount;
        }

        private string GetAmount(string p)
        {
            var individualExpense = Convert.ToDouble(GetIndividualExpense());
            var amountPaid = Math.Round(Convert.ToDouble(p), 2); ;
            var amount = 0.0;
            if (amountPaid > individualExpense)
                amount = amountPaid - individualExpense;
            else if (amountPaid.Equals(individualExpense))
                amount = 0.0;
            else
                amount = individualExpense - amountPaid;

            return amount.ToString();
        }

        private string GetPayGetOption(string p)
        {
            var individualExpense = Convert.ToDouble(GetIndividualExpense());
            var amountPaid = Math.Round(Convert.ToDouble(p), 2); ;
            if (amountPaid > individualExpense)
                return "Has to Get";
            else if (amountPaid.Equals(individualExpense))
                return "-";
            else
                return "Has to Pay";
        }

        public string GetIndividualExpense()
        {
            var value = 0.0;
            var noOfParticipents = GetExpenseParticipents();
            value = Math.Round(Convert.ToDouble(GetTotalExpenses()) / Convert.ToDouble(noOfParticipents), 2);
            return value.ToString();
        }

        public string GetExpenseParticipents()
        {
            var participents = _dbHelper.ExecuteScalar("SELECT COUNT(*) FROM UserInfo WHERE IsActive = 1 AND RoleId <> 1").ToString();
            return participents;
        }

        public string GetTotalExpenses()
        {
            var totalExpense = _dbHelper.ExecuteScalar("SELECT SUM(ExpenseAmount) FROM ExpenseDetails WHERE Finalized = 0 AND IsDeleted = 0").ToString();
            return totalExpense.Equals(string.Empty) ? "0" : totalExpense;
        }

        public string ReportForDays()
        {
            var query = "SELECT MAX(ExpenseDate) AS FromDate, Min(ExpenseDate) AS ToDate FROM ExpenseDetails WHERE Finalized = 0 AND IsDeleted = 0";
            var dataTable = _dbHelper.ExecuteDataTable(query);
            var fromDate = DataFormat.GetDateTime(dataTable.Rows[0]["FromDate"]);
            var toDate = DataFormat.GetDateTime(dataTable.Rows[0]["ToDate"]);
            var timeDiff = fromDate.Subtract(toDate);
            var days = Math.Ceiling(timeDiff.TotalDays).ToString();
            if (days.Equals(string.Empty))
                return "0";
            return days.Equals("0") ? "1" : days;
        }

        public string PerDayExpense()
        {
            var perDayExpense = string.Empty;
            var totalExpense = Convert.ToDouble(GetTotalExpenses());
            var perDayExp = 0.0;
            var days = Convert.ToDouble(ReportForDays());
            perDayExp = days > 0 ? Math.Round(totalExpense / days, 2) : 0.0;
            perDayExpense = perDayExp.ToString();
            return perDayExpense;
        }
    }
}