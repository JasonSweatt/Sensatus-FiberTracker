using Sensatus.FiberTracker.DataAccess;
using Sensatus.FiberTracker.Formatting;
using System;
using System.Data;
using System.Text;

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
            var count = _dbHelper.ExecuteScalar($"SELECT COUNT(*) FROM Item_Details WHERE Item_Name = '{itemName}' AND IsActive = 1").ToString();
            return Convert.ToInt16(count) > 0;
        }

        /// <summary>
        /// Gets Item Id for the specified Item name
        /// </summary>
        /// <param name="itemName">Item Name</param>
        /// <returns>Item Id</returns>
        public string GetItemId(string itemName)
        {
            var query = $"SELECT Item_Id from Item_Details where Item_Name='{itemName}'";
            return _dbHelper.ExecuteScalar(query).ToString();
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
            var paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@itemName", itemName));
            paramCollection.Add(new DBParameter("@itemDesc", itemDesc));
            paramCollection.Add(new DBParameter("@createdBy", createdBy));
            paramCollection.Add(new DBParameter("@createdDate", createdDate, DbType.DateTime));
            var query = "INSERT INTO Item_Details (Item_Name, Item_Desc, Created_By, Entry_Date,  IsActive) VALUES (@itemName  , @itemDesc , @createdBy, @createdDate , 1)";
            return _dbHelper.ExecuteNonQuery(query, paramCollection) > 0;
        }

        /// <summary>
        /// Updates item details for an item of specified itemId and Item details
        /// </summary>
        /// <param name="itemId">Item Id</param>
        /// <param name="itemDesc">Item Description</param>
        /// <param name="entryBy">Item modified by</param>
        /// <param name="entryDate">Item modified date</param>
        /// <returns>true if item is modified successfully otherwise false</returns>
        public bool UpdateItem(int itemId, string itemDesc, string entryBy, string entryDate)
        {
            var paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@itemDesc", itemDesc));
            paramCollection.Add(new DBParameter("@entryBy", entryBy));
            paramCollection.Add(new DBParameter("@entryDate", entryDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("@itemId", itemId));
            var query = "UPDATE Item_Details SET Item_Desc = @itemDesc , Created_By = @entryBy, Entry_Date = @entryDate WHERE Item_Id = @itemId";
            return _dbHelper.ExecuteNonQuery(query, paramCollection) > 0;
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
            var sqlCommand = new StringBuilder("SELECT Item_Id, Item_Name, Item_Desc, IsActive From Item_Details ");
            var paramCollection = new DBParameterCollection();

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
            var sqlCommand = $"SELECT Item_Desc From Item_Details Where Item_Id = {itemId}";
            return DataFormat.GetString(_dbHelper.ExecuteScalar(sqlCommand));
        }
    }
}