using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Sensatus.FiberTracker.DataAccess;
using Sensatus.FiberTracker.Formatting;


namespace Sensatus.FiberTracker.BusinessLogic
{
    public class ExpenseReport
    {
        private DBHelper _dbHelper = new DBHelper();
        public DataTable GetDataTableForDisplay()
        {
            DataTable table = new DataTable();
            Arch arch = new Arch();
            string[] columnName = {"Sl","expby","HasPaid","PayGet","Amount"};
            string[,] reportData = GetReportArray();

            if(reportData != null)
                table = arch.GetDataTableFrom2DArray(columnName, reportData);

            return table;                                  
        }

        private string[,] GetReportArray()
        {
            string[,] arrReturn = null;
            
            DataSet ds = new DataSet();
            int serialNumber = 0;

            string totalExpense = GetTotalExpenses();
            string individualExpense = GetIndividualExpense();

            string[] allUserNames = GetAllUsers();
            if (allUserNames == null) return null;

            string[] expenseAmount = GetExpenseByUsers();
            arrReturn = new string[allUserNames.Length, 5];

            for (int i = 0; i < allUserNames.Length; i++)
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
            DBHelper helper = new DBHelper();
            DataTable dtUserName = new DataTable();
            string Query = string.Empty;
            Query = "SELECT First_Name as Name  From User_Info where IsActive=1 and RoleId<>1";
            dtUserName = helper.ExecuteDataTable(Query);
            int iRowCount = dtUserName.Rows.Count;

            if (iRowCount > 0)
            {
                nameArray = new string[iRowCount];

                for (int i = 0; i < iRowCount; i++)                
                    nameArray[i] = dtUserName.Rows[i][0].ToString();                
            }

            return nameArray;
        }

        private string[] GetUsersIds()
        {
            string[] userIdArray = null;            
            DataTable dtUserID = new DataTable();
            string Query = string.Empty;
            Query = "SELECT User_Id  From User_Info where IsActive=1 and RoleId<>1";
            dtUserID = _dbHelper.ExecuteDataTable(Query);
            int iRowCount = dtUserID.Rows.Count;

            if (iRowCount > 0)
            {
                userIdArray = new string[iRowCount];

                for (int i = 0; i < iRowCount; i++)                
                    userIdArray[i] = dtUserID.Rows[i][0].ToString();                
            }

            return userIdArray;
        }

        private string[] GetExpenseByUsers()
        {
            string[] userIDs = GetUsersIds();
            if (userIDs == null)
                return null;

            string Query = string.Empty;
            object result = null;

            string[] expenseAmount = new string[userIDs.Length];

            for (int i = 0; i < userIDs.Length; i++)
            {
                Query = "SELECT Sum(Exp_Amount) FROM Expense_Details WHERE Finalized=0 AND IsDeleted=0 AND Exp_By=" + userIDs[i];
                result = _dbHelper.ExecuteScalar(Query);
                if ( result != null)
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

        private string GetPayGetOption(string p)
        {
            double individualExpense = Convert.ToDouble(GetIndividualExpense());
            double amountPaid = Math.Round(Convert.ToDouble(p), 2); ;

            if (amountPaid > individualExpense)
                return "Has to Get";
            else if (amountPaid.Equals(individualExpense))
                return "-";
            else
                return "Has to Pay";
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
            participents = _dbHelper.ExecuteScalar("Select Count(*) from User_Info WHERE IsActive=1 AND RoleId<>1").ToString();
            return participents;
        }

        public string GetTotalExpenses()
        {
            string totalExpense = string.Empty;            
            totalExpense = _dbHelper.ExecuteScalar("Select Sum(Exp_Amount) from Expense_Details WHERE Finalized=0 AND IsDeleted=0").ToString();

            if (totalExpense.Equals(""))
                return "0";
            else 
                return totalExpense;
        }

        public string ReportForDays()
        {
            string days = string.Empty;            
            string Query = "SELECT Max(Exp_Date) as FromDate, Min(Exp_Date) as ToDate  from Expense_Details where Finalized=0 AND IsDeleted=0";

            DataTable dtDates = _dbHelper.ExecuteDataTable(Query);
            DateTime fromDate = DataFormat.GetDateTime(dtDates.Rows[0]["FromDate"]);
            DateTime ToDate = DataFormat.GetDateTime(dtDates.Rows[0]["ToDate"]);

            TimeSpan timeDiff = fromDate.Subtract(ToDate);
            days = Math.Ceiling(timeDiff.TotalDays).ToString();

            if (days.Equals(""))
                return "0";
            else if(days.Equals("0"))
                return "1";
            else 
                return days;
        }

        public string PerDayExpense()
        {
            string perDayExpense = string.Empty;

            double totalExpense = Convert.ToDouble(GetTotalExpenses());
            double perDayExp = 0.0;
            double days = Convert.ToDouble(ReportForDays());

            if (days > 0)
                perDayExp = Math.Round(totalExpense / days, 2);
            else
                perDayExp = 0.0;

            perDayExpense = perDayExp.ToString();

            return perDayExpense;
        }
        
    }
}
