using System;
using System.Collections.Generic;
using System.Text;
using Sensatus.FiberTracker.DataAccess;
using System.Data;
using Sensatus.FiberTracker.Formatting;


namespace Sensatus.FiberTracker.BusinessLogic
{
    public class Items
    {
        private DBHelper _dbHelper = new DBHelper();

        /// <summary>
        /// Checks whether item with the specified name exists or not
        /// </summary>
        /// <param name="itemName">Item name</param>
        /// <returns>True if item with the specified name exists otherwise false</returns>
        public bool ItemExist(string itemName)
        {            
            string strCount = _dbHelper.ExecuteScalar("SELECT COUNT(*) FROM Item_Details WHERE Item_Name='" + itemName + "' AND IsActive=1").ToString();
            return Convert.ToInt16(strCount) > 0;                
        }

        /// <summary>
        /// Gets Item Id for the specified Item name
        /// </summary>
        /// <param name="itemName">Item Name</param>
        /// <returns>Item Id</returns>
        public string GetItemId(string itemName)
        {            
            string Query = "SELECT Item_Id from Item_Details where Item_Name='" + itemName + "'";
            return _dbHelper.ExecuteScalar(Query).ToString();            
        }
       

        /// <summary>
        /// Adds a new row to the Item_Details table for the specified values
        /// </summary>
        /// <param name="itemName">Item Name</param>
        /// <param name="itemDesc">Item Description</param>
        /// <param name="createdBy">Created By user Id</param>
        /// <param name="createdDate">Created on date</param>
        /// <returns>true if item is created successfully otherwise false</returns>
        public bool AddNewItem(string itemName, string itemDesc, int createdBy, string createdDate)
        {            
            string Query = string.Empty;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@itemName", itemName));
            paramCollection.Add(new DBParameter("@itemDesc", itemDesc));
            paramCollection.Add(new DBParameter("@createdBy", createdBy));
            paramCollection.Add(new DBParameter("@createdDate", createdDate, System.Data.DbType.DateTime));
            Query = "INSERT INTO Item_Details (Item_Name, Item_Desc, Created_By, Entry_Date,  IsActive) VALUES (";
            Query = Query + "@itemName  , @itemDesc , @createdBy, @createdDate , 1)";
            return  _dbHelper.ExecuteNonQuery(Query, paramCollection) > 0;                        
        }


        /// <summary>
        /// Updates item details for an item of specified itemId and Item details
        /// </summary>
        /// <param name="itemID">Item Id</param>
        /// <param name="itemDesc">Item Description</param>
        /// <param name="entryBy">Item modified by</param>
        /// <param name="entryDate">Item modified date</param>
        /// <returns>true if item is modified successfully otherwise false</returns>
        public bool UpdateItem(int itemID, string itemDesc, string entryBy, string entryDate)
        {            
            string Query = string.Empty;
            DBParameterCollection paramCollection = new DBParameterCollection();            
            paramCollection.Add(new DBParameter("@itemDesc", itemDesc));
            paramCollection.Add(new DBParameter("@entryBy", entryBy));
            paramCollection.Add(new DBParameter("@entryDate", entryDate, System.Data.DbType.DateTime));
            paramCollection.Add(new DBParameter("@itemID", itemID));
            Query = "UPDATE Item_Details SET Item_Desc=@itemDesc , " + 
                "Created_By =@entryBy, Entry_Date = @entryDate " +
                "WHERE Item_Id=@itemID";
            return _dbHelper.ExecuteNonQuery(Query, paramCollection) > 0;                            
        }

        /// <summary>
        /// Gets list of items having specified item name and item description.
        /// Pass String.Empty, String.Empty for fetching all the records i.e. GetItems("", "")
        /// </summary>
        /// <param name="itemName">Item name</param>
        /// <param name="itemDesc">Item description</param>
        /// <returns>Item collection in the form of data table</returns>
        public DataTable GetItems(string itemName, string itemDesc)
        {
            StringBuilder sqlCommand = new StringBuilder("SELECT Item_Id, Item_Name, Item_Desc, IsActive From Item_Details ");
            DBParameterCollection paramCollection = new DBParameterCollection();

            if (itemName != string.Empty)
            {
                sqlCommand.Append(" WHERE Item_Name LIKE @itemName");
                paramCollection.Add(new DBParameter("@itemName", itemName));
            }

            if (itemName != string.Empty && itemDesc != string.Empty)
            {
                sqlCommand.Append(" AND Item_Name LIKE @itemDesc ");
                paramCollection.Add(new DBParameter("@itemDesc", itemDesc));
            }
            else if (itemName == string.Empty && itemDesc != string.Empty)
            {
                sqlCommand.Append(" WHERE Item_Name LIKE @itemDesc");
                paramCollection.Add(new DBParameter("@itemDesc", itemDesc));
            }

            return _dbHelper.ExecuteDataTable(sqlCommand.ToString(), paramCollection);
        }

        /// <summary>
        /// Gets Item description for the specified item id.
        /// </summary>
        /// <param name="itemId">Item id</param>
        /// <returns>Item description</returns>
        public string GetItemDescription(string itemId)
        {
            string sqlCommand = "SELECT Item_Desc From Item_Details Where Item_Id = " + itemId;
            return DataFormat.GetString( _dbHelper.ExecuteScalar(sqlCommand));
        }

    }
}
