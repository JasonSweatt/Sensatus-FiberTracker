using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Sensatus.FiberTracker.DataAccess;
using Sensatus.FiberTracker.Formatting;


namespace Sensatus.FiberTracker.BusinessLogic
{
    public class Expense
    {
        private DBHelper _dbHelper = new DBHelper();
        private Arch arch = new Arch();
    

        public bool AddNewExpense(int itemID, string expenseDesc,string expenseAmount, int expenseBy, string expenseDate)
        {
            string monthYear = DataFormat.GetDateTime(expenseDate).ToString("ddMMyy").Substring(2);

            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@itemID", itemID));
            paramCollection.Add(new DBParameter("@expenseDesc", expenseDesc));
            paramCollection.Add(new DBParameter("@expenseAmount", expenseAmount));
            paramCollection.Add(new DBParameter("@expenseBy", expenseBy));
            paramCollection.Add(new DBParameter("@expenseDate", expenseDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("@monthYear", monthYear));

            string Query = "INSERT INTO Expense_Details (Item_Id,  Exp_Desc , " +
                " Exp_Amount,  Exp_By ,  Exp_Date , MonthYear ,  " +
                "Finalized, IsDeleted ) "  + 
                "VALUES (@itemID, @expenseDesc, @expenseAmount, " +
                "@expenseBy, @expenseDate, @monthYear, 0, 0)";
            
            return _dbHelper.ExecuteNonQuery(Query, paramCollection) > 0;                
        }

        public bool ModifyExpenses(int itemId, int expenseID, string expenseDesc, string expenseAmount, string expenseDate)
        {
            string monthYear = System.DateTime.Now.ToString("ddMMyy").Substring(2);
            
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@expenseDesc", expenseDesc));
            paramCollection.Add(new DBParameter("@expenseAmount", expenseAmount));
            paramCollection.Add(new DBParameter("@expDate", expenseDate, DbType.DateTime ));
            paramCollection.Add(new DBParameter("@itemId", itemId));
            paramCollection.Add(new DBParameter("@monthYear", monthYear));
            paramCollection.Add(new DBParameter("@expenseID", expenseID));
            
            string Query = "UPDATE Expense_Details SET Exp_Desc = @expenseDesc , " +
            "Exp_Amount = @expenseAmount, " +
            "Exp_Date =@expDate, Item_Id =@itemId, MonthYear=@monthYear WHERE Exp_Id=@expenseID";

            return _dbHelper.ExecuteNonQuery(Query, paramCollection) > 0;             
        }

        public bool DeleteExpenses(int expenseID)
        {           
            string Query = "UPDATE Expense_Details SET IsDeleted = 1 WHERE Exp_Id=" + expenseID.ToString();
            return _dbHelper.ExecuteNonQuery(Query) > 0;                
        }

        public DataTable GetDisplayData(int expenseID)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT Expense_Details.Exp_Id,Item_Details.Item_Name,Expense_Details.Exp_Desc,Expense_Details.Exp_Amount, " +                            
                            "Expense_Details.Exp_Date from Expense_Details,Item_Details " +                            
                            "where Expense_Details.Item_Id=Item_Details.Item_Id  AND  Expense_Details.Exp_Id=" + expenseID.ToString();

            dt = _dbHelper.ExecuteDataTable(Query);
            return dt;
        }

        public string GetExpensedBy(int expenseID)
        {
            string Query = "SELECT First_Name + ' ' + Last_Name from User_Info where User_Id IN (Select Exp_By from Expense_Details Where Exp_Id= " + expenseID.ToString()  + ")";
            return _dbHelper.ExecuteScalar(Query).ToString();
        }

        /// <summary>
        /// Returns User_Id
        /// </summary>
        /// <param name="expenseID"></param>
        /// <returns></returns>
        public string GetExpBy(int expenseID)
        {
            string Query = "Select Exp_By from Expense_Details Where Exp_Id= " + expenseID.ToString() ;
            return _dbHelper.ExecuteScalar(Query).ToString();
        }
    }
}
