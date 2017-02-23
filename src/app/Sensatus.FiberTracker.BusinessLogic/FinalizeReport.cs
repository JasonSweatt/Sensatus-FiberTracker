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
            var finalizeDate = DataFormat.DateToDB(System.DateTime.Now.ToShortDateString());
            var query = $"UPDATE FinalizationDetails SET FinalizeDate = '{DataFormat.GetCurrentDate()}' WHERE IsDeleted = 0";
            return _dbHelper.ExecuteNonQuery(query) > 0;
        }

        public bool Finalize()
        {
            var finalizeDate = DataFormat.DateToDB(System.DateTime.Now.ToShortDateString());
            var query = $"UPDATE ExpenseDetails SET Finalized = {finalizeDate} WHERE Finalized = 0";
            return _dbHelper.ExecuteNonQuery(query) > 0;
        }
    }
}