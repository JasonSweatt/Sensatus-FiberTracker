using Sensatus.FiberTracker.DataAccess;
using System;
using System.Data;

namespace Sensatus.FiberTracker.BusinessLogic
{
    public class ItemAnalyticArch
    {
        private DBHelper _dbHelper = new DBHelper();
        private Arch arch = new Arch();

        public DataTable MonthlyReportData(string month, string Year)
        {
            var monthYear = arch.DecodeMonthYear(month, Year);
            var query = "SELECT DISTINCT(ExpenseDetails.ItemId), ItemDetails.ItemName AS Item, SUM(ExpenseAmount) AS TotalExpense FROM " +
                                "ExpenseDetails,ItemDetails WHERE " +
                                "ExpenseDetails.ItemId = ItemDetails.ItemId AND " +
                                $"ExpenseDetails.MonthYear='{monthYear}' AND ExpenseDetails.IsDeleted = 0 " +
                                "GROUP BY ExpenseDetails.ItemId, ItemDetails.ItemName";

            var dataTable = _dbHelper.ExecuteDataTable(query);
            return dataTable;
        }

        public string[] GetAllItems()
        {
            string[] nameArray = null;
            var query = "SELECT ItemName FROM ItemDetails WHERE IsActive = 1";
            var dataTable = _dbHelper.ExecuteDataTable(query);
            var iRowCount = dataTable.Rows.Count;
            if (iRowCount > 0)
            {
                nameArray = new string[iRowCount];
                for (var index = 0; index < iRowCount; index++)
                    nameArray[index] = dataTable.Rows[index][0].ToString();
            }
            return nameArray;
        }

        public string[] GetItemIds()
        {
            string[] itemIdArray = null;
            var query = "SELECT ItemId FROM ItemDetails WHERE IsActive = 1";
            var dataTable = _dbHelper.ExecuteDataTable(query);
            var rowCount = dataTable.Rows.Count;
            if (rowCount > 0)
            {
                itemIdArray = new string[rowCount];
                for (var index = 0; index < rowCount; index++)
                    itemIdArray[index] = dataTable.Rows[index][0].ToString();
            }
            return itemIdArray;
        }

        public string[] GetExpenseOnItems()
        {
            var itemIds = GetItemIds();
            var expenseAmount = new string[itemIds.Length];
            for (var index = 0; index < itemIds.Length; index++)
            {
                var query = $"SELECT SUM(ExpenseAmount) FROM ExpenseDetails WHERE IsDeleted = 0 AND ItemId = {itemIds[index]}";
                if (_dbHelper.ExecuteScalar(query) != null)
                {
                    expenseAmount[index] = _dbHelper.ExecuteScalar(query).ToString();
                    if (expenseAmount[index].Equals(string.Empty))
                        expenseAmount[index] = "0";
                }
            }
            return expenseAmount;
        }

        public string[] GetExpenseForItems(string monthYear)
        {
            var itemIds = GetItemIds();
            var expenseAmount = new string[itemIds.Length];
            for (var index = 0; index < itemIds.Length; index++)
            {
                var query = $"SELECT SUM(ExpenseAmount) FROM ExpenseDetails WHERE IsDeleted = 0 AND MonthYear = '{monthYear}' AND ItemId = {itemIds[index]}";
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
            var totalExpense = _dbHelper.ExecuteScalar("SELECT SUM(ExpenseAmount) FROM ExpenseDetails WHERE Finalized = 0").ToString();
            return totalExpense.Equals(string.Empty) ? "0" : totalExpense;
        }
    }
}