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
            var dtReportData = new DataTable();
            var monthYear = arch.DecodeMonthYear(month, Year);

            var Query = "SELECT Distinct(Exp_By) as ExpBy,User_Info.First_Name as ExpenseBy, Sum(Exp_Amount) as TotalExpense from " +
            "Expense_Details,User_Info Where " +
            "Expense_Details.Exp_By=User_Info.User_Id AND " +
            "Expense_Details.MonthYear='" + monthYear + "' AND Expense_Details.IsDeleted=0" +
            " Group by Exp_By, User_Info.First_Name";

            dtReportData = _dbHelper.ExecuteDataTable(Query);

            return dtReportData;
        }

        public string[] GetAllUsers()
        {
            string[] nameArray = null;

            var dtUserName = new DataTable();
            var Query = string.Empty;
            Query = "SELECT First_Name As Name  From User_Info where IsActive=1 and User_Id<>1";
            dtUserName = _dbHelper.ExecuteDataTable(Query);
            var iRowCount = dtUserName.Rows.Count;

            if (iRowCount > 0)
            {
                nameArray = new string[iRowCount];

                for (var i = 0; i < iRowCount; i++)
                    nameArray[i] = dtUserName.Rows[i][0].ToString();
            }

            return nameArray;
        }

        public string[] GetUsersIds()
        {
            string[] userIdArray = null;
            var dtUserID = new DataTable();
            var Query = string.Empty;
            Query = "SELECT User_Id  From User_Info where IsActive=1 and User_Id<>1";
            dtUserID = _dbHelper.ExecuteDataTable(Query);
            var iRowCount = dtUserID.Rows.Count;

            if (iRowCount > 0)
            {
                userIdArray = new string[iRowCount];

                for (var i = 0; i < iRowCount; i++)
                    userIdArray[i] = dtUserID.Rows[i][0].ToString();
            }

            return userIdArray;
        }

        public string[] GetExpenseByUsers()
        {
            var userIDs = GetUsersIds();
            var Query = string.Empty;

            var expenseAmount = new string[userIDs.Length];

            for (var i = 0; i < userIDs.Length; i++)
            {
                Query = "SELECT Sum(Exp_Amount) FROM Expense_Details WHERE IsDeleted=0 AND Exp_By=" + userIDs[i];

                if (_dbHelper.ExecuteScalar(Query) != null)
                {
                    expenseAmount[i] = _dbHelper.ExecuteScalar(Query).ToString();
                    if (expenseAmount[i].Equals(""))
                        expenseAmount[i] = "0";
                }
            }

            return expenseAmount;
        }

        public string[] GetExpenseByUsers(string monthYear)
        {
            var userIDs = GetUsersIds();
            var Query = string.Empty;

            var expenseAmount = new string[userIDs.Length];

            for (var i = 0; i < userIDs.Length; i++)
            {
                Query = "SELECT Sum(Exp_Amount) FROM Expense_Details WHERE IsDeleted=0 AND MonthYear='" + monthYear + "' AND Exp_By=" + userIDs[i];

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
            var indExp = 0.0;
            var individualExpense = string.Empty;
            var noOfParticipents = GetExpenseParticipents();

            indExp = Math.Round(Convert.ToDouble(GetTotalExpenses()) / Convert.ToDouble(noOfParticipents), 2);

            return indExp.ToString();
        }

        public string GetExpenseParticipents()
        {
            var participents = string.Empty;
            participents = _dbHelper.ExecuteScalar("Select Count(*) from User_Info WHERE IsActive=1 AND User_Id<>1").ToString();
            return participents;
        }

        public string GetTotalExpenses()
        {
            var totalExpense = string.Empty;
            totalExpense = _dbHelper.ExecuteScalar("Select Sum(Exp_Amount) from Expense_Details WHERE Finalized=0").ToString();

            if (totalExpense.Equals(""))
                return "0";
            else
                return totalExpense;
        }
    }
}