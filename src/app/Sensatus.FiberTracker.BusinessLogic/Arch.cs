using Sensatus.FiberTracker.DataAccess;
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Sensatus.FiberTracker.BusinessLogic
{
    /// <summary>
    /// Class Arch.
    /// </summary>
    public class Arch
    {
        /// <summary>
        /// The database helper
        /// </summary>
        private readonly DBHelper _dbHelper = new DBHelper();

        /// <summary>
        /// Gets the data table from2 d array.
        /// </summary>
        /// <param name="columnNames">Name of the column.</param>
        /// <param name="reportData">The report data.</param>
        /// <returns>DataTable.</returns>
        public DataTable GetDataTableFrom2DArray(string[] columnNames, string[,] reportData)
        {
            var table = new DataTable();
            var numberOfColumns = columnNames.Length;
            var dataColumn = new DataColumn[numberOfColumns];

            for (var index = 0; index < numberOfColumns; index++)
            {
                dataColumn[index] = new DataColumn(columnNames[index], typeof(string));
                table.Columns.Add(dataColumn[index]);
            }

            var reportArray = reportData;
            for (var index = 0; index < reportArray.GetUpperBound(0) + 1; index++)
            {
                var row = table.NewRow();
                for (var columns = 0; columns < dataColumn.Length; columns++)
                    row[dataColumn[columns]] = reportArray[index, columns];
                table.Rows.Add(row);
            }
            return table;
        }

        /// <summary>
        /// Reades the text file.
        /// </summary>
        /// <returns>System.String.</returns>
        public string ReadeTxtFile()
        {
            var filePath = Environment.CurrentDirectory + "\\AboutAccountPlus.txt";
            var aboutData = string.Empty;

            if (File.Exists(filePath))
            {
                var reader = new StreamReader(filePath);
                try
                {
                    while (reader.Peek() != -1)
                        aboutData = aboutData + reader.ReadLine().Trim() + "\n";

                    reader.Close();
                    reader.Dispose();
                }
                catch (Exception err)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }
            else
            {
                return "Sorry could not get the information about Sensatus.FiberTracker.";
            }
            return aboutData;
        }

        /// <summary>
        /// Decodes the month year.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <returns>System.String.</returns>
        public string DecodeMonthYear(string month, string year)
        {
            year = year.Substring(2);
            var monthYear = string.Empty;

            switch (month)
            {
                case "Jan":
                    monthYear = "01" + year;
                    break;

                case "Feb":
                    monthYear = "02" + year;
                    break;

                case "Mar":
                    monthYear = "03" + year;
                    break;

                case "Apr":
                    monthYear = "04" + year;
                    break;

                case "May":
                    monthYear = "05" + year;
                    break;

                case "Jun":
                    monthYear = "06" + year;
                    break;

                case "Jul":
                    monthYear = "07" + year;
                    break;

                case "Aug":
                    monthYear = "08" + year;
                    break;

                case "Sep":
                    monthYear = "09" + year;
                    break;

                case "Oct":
                    monthYear = "10" + year;
                    break;

                case "Nov":
                    monthYear = "11" + year;
                    break;

                case "Dec":
                    monthYear = "12" + year;
                    break;
            }

            return monthYear;
        }

        /// <summary>
        /// Gets the previous month.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <returns>System.String.</returns>
        public string GetPreviousMonth(string month)
        {
            var previousMonth = string.Empty;

            switch (month)
            {
                case "Jan":
                    previousMonth = "Dec";
                    break;

                case "Feb":
                    previousMonth = "Jan";
                    break;

                case "Mar":
                    previousMonth = "Feb";
                    break;

                case "Apr":
                    previousMonth = "Mar";
                    break;

                case "May":
                    previousMonth = "Apr";
                    break;

                case "Jun":
                    previousMonth = "May";
                    break;

                case "Jul":
                    previousMonth = "Jun";
                    break;

                case "Aug":
                    previousMonth = "Jul";
                    break;

                case "Sep":
                    previousMonth = "Aug";
                    break;

                case "Oct":
                    previousMonth = "Sep";
                    break;

                case "Nov":
                    previousMonth = "Oct";
                    break;

                case "Dec":
                    previousMonth = "Nov";
                    break;
            }

            return previousMonth;
        }

        /// <summary>
        /// Gets the next month.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <returns>System.String.</returns>
        public string GetNextMonth(string month)
        {
            var nextMonth = string.Empty;

            switch (month)
            {
                case "Jan":
                    nextMonth = "Feb";
                    break;

                case "Feb":
                    nextMonth = "Mar";
                    break;

                case "Mar":
                    nextMonth = "Apr";
                    break;

                case "Apr":
                    nextMonth = "May";
                    break;

                case "May":
                    nextMonth = "Jun";
                    break;

                case "Jun":
                    nextMonth = "Jul";
                    break;

                case "Jul":
                    nextMonth = "Aug";
                    break;

                case "Aug":
                    nextMonth = "Sep";
                    break;

                case "Sep":
                    nextMonth = "Oct";
                    break;

                case "Oct":
                    nextMonth = "Nov";
                    break;

                case "Nov":
                    nextMonth = "Dec";
                    break;

                case "Dec":
                    nextMonth = "Jan";
                    break;
            }

            return nextMonth;
        }

        /// <summary>
        /// Gets the previous months year.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <returns>System.String.</returns>
        public string GetPrevMonthsYear(string month, string year)
        {
            var prevMonthsYear = 0;
            if (month.Equals("Jan"))
                prevMonthsYear = Convert.ToInt16(year) - 1;
            else
                prevMonthsYear = Convert.ToInt16(year);

            return prevMonthsYear.ToString();
        }

        /// <summary>
        /// Gets the next months year.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <returns>System.String.</returns>
        public string GetNextMonthsYear(string month, string year)
        {
            var nextMonthsYear = 0;
            if (month.Equals("Dec"))
                nextMonthsYear = Convert.ToInt16(year) + 1;
            else
                nextMonthsYear = Convert.ToInt16(year);

            return nextMonthsYear.ToString();
        }

        /// <summary>
        /// Enum ComboBoxItem
        /// </summary>
        public enum ComboBoxItem
        {
            Role,
            Item
        }

        /// <summary>
        /// Fills the data in combo.
        /// </summary>
        /// <param name="cmbItems">The CMB items.</param>
        /// <param name="item">The item.</param>
        public void FillDataInCombo(ComboBox cmbItems, ComboBoxItem item)
        {
            cmbItems.Items.Clear();
            var dt = new DataTable();
            cmbItems.Items.Insert(0, new DictionaryEntry("-1", "[ SELECT ]"));

            var Query = string.Empty;
            Query = item == ComboBoxItem.Item ? "SELECT Item_Id, Item_Name from Item_Details where IsActive=1" : "SELECT RoleId, Role from RoleDetails";
            dt = _dbHelper.ExecuteDataTable(Query);

            for (var i = 0; i < dt.Rows.Count; i++)
                cmbItems.Items.Add(new DictionaryEntry(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString()));

            cmbItems.ValueMember = "Key";
            cmbItems.DisplayMember = "Value";
            cmbItems.SelectedIndex = 0;
        }

        /// <summary>
        /// Fills the data in combo.
        /// </summary>
        /// <param name="cmbItems">The CMB items.</param>
        /// <param name="selectQuery">The select query.</param>
        /// <param name="index">The index.</param>
        public void FillDataInCombo(ComboBox cmbItems, string selectQuery, int index)
        {
            var dt = new DataTable();
            cmbItems.Items.Clear();
            dt = _dbHelper.ExecuteDataTable(selectQuery);

            for (var i = 0; i < dt.Rows.Count; i++)
                cmbItems.Items.Add(dt.Rows[i][index].ToString());
        }

        /// <summary>
        /// Gets the month.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <returns>System.Int32.</returns>
        public int GetMonth(string month)
        {
            var iMonth = 0;
            switch (month)
            {
                case "Jan":
                    iMonth = 1;
                    break;

                case "Feb":
                    iMonth = 2;
                    break;

                case "Mar":
                    iMonth = 3;
                    break;

                case "Apr":
                    iMonth = 4;
                    break;

                case "May":
                    iMonth = 5;
                    break;

                case "Jun":
                    iMonth = 6;
                    break;

                case "Jul":
                    iMonth = 7;
                    break;

                case "Aug":
                    iMonth = 8;
                    break;

                case "Sep":
                    iMonth = 9;
                    break;

                case "Oct":
                    iMonth = 10;
                    break;

                case "Nov":
                    iMonth = 11;
                    break;

                case "Dec":
                    iMonth = 12;
                    break;
            }

            return iMonth;
        }
    }
}