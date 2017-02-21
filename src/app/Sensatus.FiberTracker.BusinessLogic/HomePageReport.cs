using Sensatus.FiberTracker.DataAccess;
using System.Data;

namespace Sensatus.FiberTracker.BusinessLogic
{
    public class HomePageReport
    {
        private DBHelper _dbHelper = new DBHelper();

        public DataTable HomePageDetails()
        {
            var data = new DataTable();
            var Query = "SELECT Expense_Details.Exp_Id,Item_Details.Item_Name AS Item, Expense_Details.Exp_Desc AS Details, Expense_Details.Exp_Amount AS Amount, User_Info.First_Name AS ExpensedBy, Expense_Details.Exp_Date AS ExpDate, Expense_Details.Exp_By as UserId " +
                           "FROM Expense_Details, Item_Details, User_Info " +
                           "WHERE Expense_Details.Item_Id=Item_Details.Item_Id And Expense_Details.Exp_By=User_Info.User_Id And Expense_Details.FInalized=0 AND Expense_Details.IsDeleted=0; ";

            data = _dbHelper.ExecuteDataTable(Query);
            return data;
        }

        public DataTable HomePageReportData()
        {
            var dt = new DataTable();
            var Query = "SELECT Distinct(Exp_By) as UserId,User_Info.First_Name as ExpencedBy, Sum(Exp_Amount) as Amount from " +
            "Expense_Details,User_Info Where " +
            "Expense_Details.Exp_By=User_Info.User_Id AND " +
            "Expense_Details.Finalized=0 AND Expense_Details.IsDeleted=0 " +
            "Group by Exp_By, User_Info.First_Name";

            dt = _dbHelper.ExecuteDataTable(Query);
            return dt;
        }
    }
}