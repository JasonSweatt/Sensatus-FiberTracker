using Sensatus.FiberTracker.DataAccess;
using System;
using System.Data;

namespace Sensatus.FiberTracker.BusinessLogic
{
    public class ReportArchitecture
    {
        private DBHelper _dbHelper = new DBHelper();
        private Arch arch = new Arch();

        public DataTable MonthlyReportData(string month, string Year)
        {
            var monthYear = arch.DecodeMonthYear(month, Year);
            var query = @"SELECT Distinct(ExpenseBy) AS ExpBy, UserInfo.FirstName AS ExpenseBy, SUM(ExpenseAmount) AS TotalExpense
                          FROM  ExpenseDetails, UserInfo " +
                        $"WHERE ExpenseDetails.ExpenseBy = UserInfo.UserId AND ExpenseDetails.MonthYear = '{monthYear}' AND ExpenseDetails.IsDeleted = 0 " +
                        " GROUP BY ExpenseBy, UserInfo.FirstName";

            var dataTable = _dbHelper.ExecuteDataTable(query);
            return dataTable;
        }

        public string[] GetAllUsers()
        {
            string[] nameArray = null;
            var dataTable = new DataTable();
            var query = "SELECT FirstName AS Name FROM UserInfo WHERE IsActive = 1 and UserId <> 1";
            dataTable = _dbHelper.ExecuteDataTable(query);
            var rowCount = dataTable.Rows.Count;
            if (rowCount > 0)
            {
                nameArray = new string[rowCount];
                for (var index = 0; index < rowCount; index++)
                    nameArray[index] = dataTable.Rows[index][0].ToString();
            }
            return nameArray;
        }

        public string[] GetUsersIds()
        {
            string[] userIdArray = null;
            var dataTable = new DataTable();
            var query = "SELECT UserId FROM UserInfo WHERE IsActive = 1 AND UserId <> 1";
            dataTable = _dbHelper.ExecuteDataTable(query);
            var rowCount = dataTable.Rows.Count;
            if (rowCount > 0)
            {
                userIdArray = new string[rowCount];
                for (var index = 0; index < rowCount; index++)
                    userIdArray[index] = dataTable.Rows[index][0].ToString();
            }
            return userIdArray;
        }

        public string[] GetExpenseByUsers()
        {
            var usersIds = GetUsersIds();
            var expenseAmount = new string[usersIds.Length];
            for (var index = 0; index < usersIds.Length; index++)
            {
                var query = $"SELECT SUM(ExpenseAmount) FROM ExpenseDetails WHERE IsDeleted = 0 AND ExpenseBy = {usersIds[index]}";
                if (_dbHelper.ExecuteScalar(query) != null)
                {
                    expenseAmount[index] = _dbHelper.ExecuteScalar(query).ToString();
                    if (expenseAmount[index].Equals(string.Empty))
                        expenseAmount[index] = "0";
                }
            }
            return expenseAmount;
        }

        public string[] GetExpenseByUsers(string monthYear)
        {
            var usersIds = GetUsersIds();
            var expenseAmount = new string[usersIds.Length];
            for (var index = 0; index < usersIds.Length; index++)
            {
                var query = $"SELECT SUM(ExpenseAmount) FROM ExpenseDetails WHERE IsDeleted = 0 AND MonthYear = '{monthYear}' AND ExpenseBy={usersIds[index]}";
                if (_dbHelper.ExecuteScalar(query) != null)
                {
                    expenseAmount[index] = _dbHelper.ExecuteScalar(query).ToString();
                    if (expenseAmount[index].Equals(string.Empty))
                        expenseAmount[index] = "0";
                }
            }
            return expenseAmount;
        }

        public string GetAmount(string p)
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

        public string GetIndividualExpense()
        {
            var value = 0.0;
            var noOfParticipents = GetExpenseParticipents();
            value = Math.Round(Convert.ToDouble(GetTotalExpenses()) / Convert.ToDouble(noOfParticipents), 2);
            return value.ToString();
        }

        public string GetExpenseParticipents()
        {
            var participents = _dbHelper.ExecuteScalar("SELECT COUNT(*) FROM UserInfo WHERE IsActive = 1 AND UserId <> 1").ToString();
            return participents;
        }

        public string GetTotalExpenses()
        {
            var totalExpense = string.Empty;
            totalExpense = _dbHelper.ExecuteScalar("SELECT SUM(ExpenseAmount) FROM ExpenseDetails WHERE Finalized = 0").ToString();
            return totalExpense.Equals(string.Empty) ? "0" : totalExpense;
        }
    }
}