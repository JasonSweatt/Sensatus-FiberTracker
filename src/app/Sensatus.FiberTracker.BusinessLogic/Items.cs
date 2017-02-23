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
            var count = _dbHelper.ExecuteScalar($"SELECT COUNT(*) FROM ItemDetails WHERE ItemName = '{itemName}' AND IsActive = 1").ToString();
            return Convert.ToInt16(count) > 0;
        }

        /// <summary>
        /// Gets Item Id for the specified Item name
        /// </summary>
        /// <param name="itemName">Item Name</param>
        /// <returns>Item Id</returns>
        public string GetItemId(string itemName)
        {
            var query = $"SELECT ItemId from ItemDetails where ItemName='{itemName}'";
            return _dbHelper.ExecuteScalar(query).ToString();
        }

        /// <summary>
        /// Adds a new row to the ItemDetails table for the specified values
        /// </summary>
        /// <param name="itemName">Item Name</param>
        /// <param name="itemDesc">Item Description</param>
        /// <param name="createdBy">Created By user Id</param>
        /// <param name="createdDate">Created on date</param>
        /// <returns>true if item is created successfully otherwise false</returns>
        public bool AddNewItem(string itemName, string itemDesc, int createdBy, string createdDate)
        {
            var paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@ItemName", itemName));
            paramCollection.Add(new DBParameter("@ItemDescription", itemDesc));
            paramCollection.Add(new DBParameter("@CreatedBy", createdBy));
            paramCollection.Add(new DBParameter("@CreatedDate", createdDate, DbType.DateTime));
            var insert = "INSERT INTO ItemDetails (ItemName, ItemDescription, CreatedBy, EntryDate,  IsActive) VALUES (@ItemName, @ItemDescription, @CreatedBy, @CreatedDate, 1)";
            return _dbHelper.ExecuteNonQuery(insert, paramCollection) > 0;
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
            paramCollection.Add(new DBParameter("@ItemDescription", itemDesc));
            paramCollection.Add(new DBParameter("@EntryBy", entryBy));
            paramCollection.Add(new DBParameter("@EntryDate", entryDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("@ItemId", itemId));
            var update = "UPDATE ItemDetails SET ItemDescription = @ItemDescription, CreatedBy = @EntryBy, EntryDate = @EntryDate WHERE ItemId = @ItemId";
            return _dbHelper.ExecuteNonQuery(update, paramCollection) > 0;
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
            var sqlCommand = new StringBuilder("SELECT ItemId, ItemName, ItemDescription, IsActive FROM ItemDetails ");
            var paramCollection = new DBParameterCollection();

            if (itemName != string.Empty)
            {
                sqlCommand.Append(" WHERE ItemName LIKE @ItemName");
                paramCollection.Add(new DBParameter("@ItemName", itemName));
            }
            if (itemName != string.Empty && itemDesc != string.Empty)
            {
                sqlCommand.Append(" AND ItemName LIKE @ItemDescription ");
                paramCollection.Add(new DBParameter("@ItemDescription", itemDesc));
            }
            else if (itemName == string.Empty && itemDesc != string.Empty)
            {
                sqlCommand.Append(" WHERE ItemName LIKE @ItemDescription");
                paramCollection.Add(new DBParameter("@ItemDescription", itemDesc));
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
            var sqlCommand = $"SELECT ItemDescription FROM ItemDetails WHERE ItemId = {itemId}";
            return DataFormat.GetString(_dbHelper.ExecuteScalar(sqlCommand));
        }
    }
}