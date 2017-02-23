using Sensatus.FiberTracker.DataAccess;
using Sensatus.FiberTracker.Formatting;
using System.Data;

namespace Sensatus.FiberTracker.BusinessLogic
{
    public class Expense
    {
        private DBHelper _dbHelper = new DBHelper();
        private Arch arch = new Arch();

        public bool AddNewExpense(int itemID, string expenseDesc, string expenseAmount, int expenseBy, string expenseDate)
        {
            var monthYear = DataFormat.GetDateTime(expenseDate).ToString("ddMMyy").Substring(2);
            var paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@ItemId", itemID));
            paramCollection.Add(new DBParameter("@ExpenseDesc", expenseDesc));
            paramCollection.Add(new DBParameter("@ExpenseAmount", expenseAmount));
            paramCollection.Add(new DBParameter("@ExpenseBy", expenseBy));
            paramCollection.Add(new DBParameter("@ExpenseDate", expenseDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("@MonthYear", monthYear));

            var query = "INSERT INTO ExpenseDetails (ItemId,  ExpenseDescription, ExpenseAmount, ExpenseBy, ExpenseDate, MonthYear, Finalized, IsDeleted) " +
                "VALUES (@ItemId, @ExpenseDesc, @ExpenseAmount, @ExpenseBy, @ExpenseDate, @MonthYear, 0, 0)";
            return _dbHelper.ExecuteNonQuery(query, paramCollection) > 0;
        }

        public bool ModifyExpenses(int itemId, int expenseID, string expenseDesc, string expenseAmount, string expenseDate)
        {
            var monthYear = System.DateTime.Now.ToString("ddMMyy").Substring(2);
            var paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@ExpenseDesc", expenseDesc));
            paramCollection.Add(new DBParameter("@ExpenseAmount", expenseAmount));
            paramCollection.Add(new DBParameter("@ExpDate", expenseDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("@ItemId", itemId));
            paramCollection.Add(new DBParameter("@MonthYear", monthYear));
            paramCollection.Add(new DBParameter("@ExpenseId", expenseID));
            var query = "UPDATE ExpenseDetails SET ExpenseDescription = @ExpenseDesc, ExpenseAmount = @ExpenseAmount, ExpenseDate = @ExpDate, ItemId = @ItemId, MonthYear = @MonthYear WHERE ExpenseId = @ExpenseId";
            return _dbHelper.ExecuteNonQuery(query, paramCollection) > 0;
        }

        public bool DeleteExpenses(int expenseId)
        {
            var update = $"UPDATE ExpenseDetails SET IsDeleted = 1 WHERE ExpenseId = {expenseId}";
            return _dbHelper.ExecuteNonQuery(update) > 0;
        }

        public DataTable GetDisplayData(int expenseId)
        {
            var query = "SELECT ExpenseDetails.ExpenseId,ItemDetails.ItemName,ExpenseDetails.ExpenseDescription,ExpenseDetails.ExpenseAmount, " +
                        "ExpenseDetails.ExpenseDate FROM ExpenseDetails, ItemDetails " +
                       $"WHERE ExpenseDetails.ItemId=ItemDetails.ItemId AND ExpenseDetails.ExpenseId = {expenseId}";
            var dataTable = _dbHelper.ExecuteDataTable(query);
            return dataTable;
        }

        public string GetExpensedBy(int expenseId)
        {
            var query = $"SELECT FirstName + ' ' + LastName FROM UserInfo WHERE UserId IN (SELECT ExpenseBy FROM ExpenseDetails WHERE ExpenseId= {expenseId})";
            return _dbHelper.ExecuteScalar(query).ToString();
        }

        /// <summary>
        /// Returns UserId
        /// </summary>
        /// <param name="expenseId"></param>
        /// <returns></returns>
        public string GetExpBy(int expenseId)
        {
            var query = $"SELECT ExpenseBy FROM ExpenseDetails WHERE ExpenseId= " + expenseId.ToString();
            return _dbHelper.ExecuteScalar(query).ToString();
        }
    }
}