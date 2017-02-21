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
            var dtUserName = new DataTable();
            var Query = string.Empty;
            Query = "SELECT First_Name as Name  From User_Info where IsActive=1 and RoleId<>1";
            dtUserName = helper.ExecuteDataTable(Query);
            var iRowCount = dtUserName.Rows.Count;

            if (iRowCount > 0)
            {
                nameArray = new string[iRowCount];

                for (var i = 0; i < iRowCount; i++)
                    nameArray[i] = dtUserName.Rows[i][0].ToString();
            }

            return nameArray;
        }

        private string[] GetUsersIds()
        {
            string[] userIdArray = null;
            var dtUserID = new DataTable();
            var Query = string.Empty;
            Query = "SELECT User_Id  From User_Info where IsActive=1 and RoleId<>1";
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

        private string[] GetExpenseByUsers()
        {
            var userIDs = GetUsersIds();
            if (userIDs == null)
                return null;

            var Query = string.Empty;
            object result = null;

            var expenseAmount = new string[userIDs.Length];

            for (var i = 0; i < userIDs.Length; i++)
            {
                Query = "SELECT Sum(Exp_Amount) FROM Expense_Details WHERE Finalized=0 AND IsDeleted=0 AND Exp_By=" + userIDs[i];
                result = _dbHelper.ExecuteScalar(Query);
                if (result != null)
                {
                    expenseAmount[i] = result.ToString();
                    if (expenseAmount[i].Equals(""))
                        expenseAmount[i] = "0";
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
            var indExp = 0.0;
            var individualExpense = string.Empty;
            var noOfParticipents = GetExpenseParticipents();

            indExp = Math.Round(Convert.ToDouble(GetTotalExpenses()) / Convert.ToDouble(noOfParticipents), 2);

            return indExp.ToString();
        }

        public string GetExpenseParticipents()
        {
            var participents = string.Empty;
            participents = _dbHelper.ExecuteScalar("Select Count(*) from User_Info WHERE IsActive=1 AND RoleId<>1").ToString();
            return participents;
        }

        public string GetTotalExpenses()
        {
            var totalExpense = string.Empty;
            totalExpense = _dbHelper.ExecuteScalar("Select Sum(Exp_Amount) from Expense_Details WHERE Finalized=0 AND IsDeleted=0").ToString();

            if (totalExpense.Equals(""))
                return "0";
            else
                return totalExpense;
        }

        public string ReportForDays()
        {
            var days = string.Empty;
            var Query = "SELECT Max(Exp_Date) as FromDate, Min(Exp_Date) as ToDate  from Expense_Details where Finalized=0 AND IsDeleted=0";

            var dtDates = _dbHelper.ExecuteDataTable(Query);
            var fromDate = DataFormat.GetDateTime(dtDates.Rows[0]["FromDate"]);
            var ToDate = DataFormat.GetDateTime(dtDates.Rows[0]["ToDate"]);

            var timeDiff = fromDate.Subtract(ToDate);
            days = Math.Ceiling(timeDiff.TotalDays).ToString();

            if (days.Equals(""))
                return "0";
            else if (days.Equals("0"))
                return "1";
            else
                return days;
        }

        public string PerDayExpense()
        {
            var perDayExpense = string.Empty;

            var totalExpense = Convert.ToDouble(GetTotalExpenses());
            var perDayExp = 0.0;
            var days = Convert.ToDouble(ReportForDays());

            if (days > 0)
                perDayExp = Math.Round(totalExpense / days, 2);
            else
                perDayExp = 0.0;

            perDayExpense = perDayExp.ToString();

            return perDayExpense;
        }
    }
}