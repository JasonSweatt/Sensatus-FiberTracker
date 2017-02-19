using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Sensatus.FiberTracker.DataAccess;
using Sensatus.FiberTracker.Formatting;


namespace Sensatus.FiberTracker.BusinessLogic
{
    public class FinalizeReport
    {
        private Arch _arch = new Arch();        
        private DBHelper _dbHelper = new DBHelper();

        public bool UpdateFinalizationDetails()
        {            
            string Query = string.Empty;
            string finalizeDate = DataFormat.DateToDB(System.DateTime.Now.ToShortDateString());            
            Query = "UPDATE Finalization_Details SET Finalize_Date = '" + DataFormat.GetCurrentDate() + "' WHERE IsDeleted=0";

            if (_dbHelper.ExecuteNonQuery(Query) > 0)
                return true;
            else
                return false;
        }

        public bool Finalize()
        {            
            string Query = string.Empty;
            string finalizeDate = DataFormat.DateToDB(System.DateTime.Now.ToShortDateString());

            Query = "UPDATE Expense_Details SET Finalized = " + finalizeDate + " WHERE Finalized=0";

            if (_dbHelper.ExecuteNonQuery(Query) > 0)
                return true;
            else
                return false;
                        
        }
    }
}
