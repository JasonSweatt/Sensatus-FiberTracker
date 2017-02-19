using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Sensatus.FiberTracker.DataAccess;

namespace Sensatus.FiberTracker.BusinessLogic
{
    public class ItemAnalyticArch
    {
        private DBHelper _dbHelper = new DBHelper();
        private Arch arch = new Arch();

        public DataTable MonthlyReportData(string month, string Year)
        {
            DataTable dtReportData = new DataTable();
            string monthYear = arch.DecodeMonthYear(month, Year);        

            string Query = "SELECT Distinct(Expense_Details.Item_Id), Item_Details.Item_Name as Item, Sum(Exp_Amount) as TotalExpense from " +
                                "Expense_Details,Item_Details Where " +
                                "Expense_Details.Item_Id=Item_Details.Item_Id AND " +
                                "Expense_Details.MonthYear='" + monthYear + "' AND Expense_Details.IsDeleted=0 " +
                                "Group by Expense_Details.Item_Id, Item_Details.Item_Name";

            dtReportData = _dbHelper.ExecuteDataTable(Query);

            return dtReportData;
        }      

        public string[] GetAllItems()
        {
            string[] nameArray = null;

            DataTable dtItemName = new DataTable();
            string Query = string.Empty;
            Query = "SELECT Item_Name From Item_Details where IsActive=1";
            dtItemName = _dbHelper.ExecuteDataTable(Query);
            int iRowCount = dtItemName.Rows.Count;

            if (iRowCount > 0)
            {
                nameArray = new string[iRowCount];

                for (int i = 0; i < iRowCount; i++)
                {
                    nameArray[i] = dtItemName.Rows[i][0].ToString();
                }
            }

            return nameArray;
        }

        public string[] GetItemIds()
        {
            string[] itemIdArray = null;
            DataTable dtItemID = new DataTable();
            string Query = string.Empty;
            Query = "SELECT Item_Id  From Item_Details where IsActive=1";
            dtItemID = _dbHelper.ExecuteDataTable(Query);
            int iRowCount = dtItemID.Rows.Count;

            if (iRowCount > 0)
            {
                itemIdArray = new string[iRowCount];

                for (int i = 0; i < iRowCount; i++)
                {
                    itemIdArray[i] = dtItemID.Rows[i][0].ToString();
                }
            }

            return itemIdArray;
        }

        public string[] GetExpenseOnItems()
        {
            string[] itemIDs = GetItemIds();
            string Query = string.Empty;

            string[] expenseAmount = new string[itemIDs.Length];

            for (int i = 0; i < itemIDs.Length; i++)
            {                
                Query = "SELECT Sum(Exp_Amount) FROM Expense_Details WHERE IsDeleted=0 AND Item_Id=" + itemIDs[i];

                if (_dbHelper.ExecuteScalar(Query) != null)
                {
                    expenseAmount[i] = _dbHelper.ExecuteScalar(Query).ToString();
                    if (expenseAmount[i].Equals(""))
                        expenseAmount[i] = "0";
                }
            }

            return expenseAmount;
        }

        public string[] GetExpenseForItems(string monthYear)
        {
            string[] itemIDs = GetItemIds();
            string Query = string.Empty;

            string[] expenseAmount = new string[itemIDs.Length];

            for (int i = 0; i < itemIDs.Length; i++)
            {
                Query = "SELECT Sum(Exp_Amount) FROM Expense_Details WHERE IsDeleted=0 AND MonthYear='" + monthYear + "' AND Item_Id=" + itemIDs[i];

                if (_dbHelper.ExecuteScalar(Query) != null)
                {
                    expenseAmount[i] = _dbHelper.ExecuteScalar(Query).ToString();
                    if (expenseAmount[i].Equals(""))
                        expenseAmount[i] = "0";
                }
            }

            return expenseAmount;
        }

        public string GetAmount(string p)
        {
            double individualExpense = Convert.ToDouble(GetIndividualExpense());
            double amountPaid = Math.Round(Convert.ToDouble(p), 2); ;
            double amount = 0.0;

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
            double indExp = 0.0;
            string individualExpense = string.Empty;
            string noOfParticipents = GetExpenseParticipents();

            indExp = Math.Round(Convert.ToDouble(GetTotalExpenses()) / Convert.ToDouble(noOfParticipents), 2);

            return indExp.ToString();

        }

        public string GetExpenseParticipents()
        {
            string participents = string.Empty;
            participents = _dbHelper.ExecuteScalar("Select Count(*) from User_Info WHERE IsActive=1 AND User_Id<>1").ToString();
            return participents;
        }

        public string GetTotalExpenses()
        {
            string totalExpense = string.Empty;
            totalExpense = _dbHelper.ExecuteScalar("Select Sum(Exp_Amount) from Expense_Details WHERE Finalized=0").ToString();

            if (totalExpense.Equals(""))
                return "0";
            else
                return totalExpense;
        }     
    }
}
