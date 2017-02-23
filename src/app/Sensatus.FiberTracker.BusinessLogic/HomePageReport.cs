using Sensatus.FiberTracker.DataAccess;
using System.Data;

namespace Sensatus.FiberTracker.BusinessLogic
{
    public class HomePageReport
    {
        private DBHelper _dbHelper = new DBHelper();

        public DataTable HomePageDetails()
        {
            var query = "SELECT ExpenseDetails.ExpenseId,ItemDetails.ItemName AS Item, ExpenseDetails.ExpenseDescription AS Details, ExpenseDetails.ExpenseAmount AS Amount, UserInfo.FirstName AS ExpensedBy, ExpenseDetails.ExpenseDate AS ExpDate, ExpenseDetails.ExpenseBy AS UserId " +
                        "FROM ExpenseDetails, ItemDetails, UserInfo " +
                        "WHERE ExpenseDetails.ItemId = ItemDetails.ItemId AND ExpenseDetails.ExpenseBy=UserInfo.UserId AND ExpenseDetails.FInalized = 0 AND ExpenseDetails.IsDeleted = 0;";

            var dataTable = _dbHelper.ExecuteDataTable(query);
            return dataTable;
        }

        public DataTable HomePageReportData()
        {
            var query = "SELECT DISTINCT(ExpenseBy) AS UserId, UserInfo.FirstName AS ExpencedBy, SUM(ExpenseAmount) AS Amount FROM " +
            "ExpenseDetails, UserInfo WHERE " +
            "ExpenseDetails.ExpenseBy = UserInfo.UserId AND " +
            "ExpenseDetails.Finalized = 0 AND ExpenseDetails.IsDeleted = 0 " +
            "GROUP BY ExpenseBy, UserInfo.FirstName";
            var dataTable = _dbHelper.ExecuteDataTable(query);
            return dataTable;
        }
    }
}