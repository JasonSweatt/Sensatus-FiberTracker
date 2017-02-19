using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using Sensatus.FiberTracker.DataAccess;
using System.Windows.Forms;
using System.Collections;


namespace Sensatus.FiberTracker.BusinessLogic
{
    public class Arch
    {
        private DBHelper _dbHelper = new DBHelper();

        public DataTable GetDataTableFrom2DArray(string[] columnName, string[,] reportData)
        {
            DataTable table = new DataTable();            
            int cols = columnName.Length ;
            DataColumn[] dataColumn = new DataColumn[cols];
                        
            for (int c=0; c<cols; c++)
            {
                dataColumn[c] = new DataColumn(columnName[c], typeof(string));
                table.Columns.Add(dataColumn[c]);
            }
                      
            string[,] reportArray = reportData;

            for (int i = 0; i < reportArray.GetUpperBound(0) + 1; i++)
            {
                DataRow row = table.NewRow();

                for (int columns = 0; columns < dataColumn.Length; columns++)
                {
                    row[dataColumn[columns]] = reportArray[i,columns];
                }               

                table.Rows.Add(row);
            }
            return table;
        }

        public string ReadeTxtFile()
        {
            string filePath = System.Environment.CurrentDirectory + "\\AboutAccountPlus.txt";
            string aboutData = string.Empty;

            if (File.Exists(filePath))
            {
                StreamReader reader= new StreamReader(filePath);
                try
                {                   
                    while (reader.Peek() != -1)
                    {
                        aboutData = aboutData + reader.ReadLine().Trim() + "\n";
                    }

                    reader.Close();
                    reader.Dispose();
                }
                catch(Exception err)
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

        public string DecodeMonthYear(string month, string year)
        {
            year = year.Substring(2);
            string monthYear = string.Empty;

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

        public string GetPreviousMonth(string month)
        {
            string previousMonth = string.Empty;

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

        public string GetNextMonth(string month)
        {
            string nextMonth = string.Empty;

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

        public string GetPrevMonthsYear(string month, string year)
        {
            int prevMonthsYear = 0;
            if (month.Equals("Jan"))
                prevMonthsYear = Convert.ToInt16(year) - 1;
            else
                prevMonthsYear = Convert.ToInt16(year);

            return prevMonthsYear.ToString();
        }

        public string GetNextMonthsYear(string month, string year)
        {
            int nextMonthsYear = 0;
            if (month.Equals("Dec"))
                nextMonthsYear = Convert.ToInt16(year) + 1;
            else
                nextMonthsYear = Convert.ToInt16(year);

            return nextMonthsYear.ToString();
        }

        public enum ComboBoxItem
        {
            Role,
            Item
        }

        public void FillDataInCombo(ComboBox cmbItems, ComboBoxItem item)
        {
            cmbItems.Items.Clear();
            DataTable dt = new DataTable();
            cmbItems.Items.Insert(0, new DictionaryEntry("-1", "[ SELECT ]"));

            string Query = string.Empty;
            Query = item == ComboBoxItem.Item ? "SELECT Item_Id, Item_Name from Item_Details where IsActive=1" : "SELECT RoleId, Role from RoleDetails";
            dt = _dbHelper.ExecuteDataTable(Query);

            for (int i = 0; i < dt.Rows.Count; i++)            
                cmbItems.Items.Add(new DictionaryEntry(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString()));

            cmbItems.ValueMember = "Key";
            cmbItems.DisplayMember = "Value";
            cmbItems.SelectedIndex = 0;
        }

        public void FillDataInCombo(ComboBox cmbItems, string selectQuery, int index)
        {            
            DataTable dt = new DataTable();
            cmbItems.Items.Clear();
            dt = _dbHelper.ExecuteDataTable(selectQuery);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbItems.Items.Add(dt.Rows[i][index].ToString());
            }
        }

        public int GetMonth(string month)
        {
            int iMonth=0;
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
