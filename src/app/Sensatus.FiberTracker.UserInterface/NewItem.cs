using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sensatus.FiberTracker.BusinessLogic;
using Sensatus.FiberTracker.Messaging;
using Sensatus.FiberTracker.Formatting;




namespace Sensatus.FiberTracker.UI
{
    public partial class NewItem : AccountPlusBase
    {
        private Items _itemManagement = new Items();
        public NewItem()
        {
            InitializeComponent();
            LoadData(string.Empty, string.Empty);
        }

        private void LoadData(string itemName, string  itemDesc)
        {
            //StringBuilder sqlCommand = new StringBuilder("SELECT Item_Id, Item_Name, Item_Desc, IsActive From Item_Details ");
            
            //if (itemName != string.Empty)
            //    sqlCommand.Append(" WHERE Item_Name LIKE '" + itemName + "'");

            //if (itemName != string.Empty && itemDesc != string.Empty)            
            //    sqlCommand.Append(" AND Item_Name LIKE '" + itemDesc + "'");
            //else if (itemName == string.Empty && itemDesc != string.Empty)
            //    sqlCommand.Append(" WHERE Item_Name LIKE '" + itemDesc + "'");



            GRID_VIEW_ITEM_DETAILS.DataSource = _itemManagement.GetItems(itemName, itemDesc);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            AddUpdateItem();                        
        }

        private void AddUpdateItem()
        {
            errorProvider1.Clear();
            lblMessage.Clear();

            var itemName = txtItemName.Text.Trim();
            var itemDesc = txtaDesc.Text.Trim();
            var message = string.Empty;
            


            if (itemName == string.Empty)
            {
                message = MessageManager.GetMessage("33");
                errorProvider1.SetError(txtItemName, message);
                lblMessage.SetMessage(message);
                return;
            }

            if (itemDesc == string.Empty)
            {
                message = MessageManager.GetMessage("34");
                errorProvider1.SetError(txtaDesc, message);
                lblMessage.SetMessage(message);
                return;
            }

            if (_itemId != 0)
            {
                if (_itemManagement.UpdateItem(_itemId, itemDesc, SessionParameters.UserID.ToString(), DateTime.Now.ToShortDateString()))
                {
                    lblMessage.SetMessage(MessageManager.GetMessage("35"));
                    Clear();
                }
                else
                {
                    lblMessage.SetMessage(MessageManager.GetMessage("36"));
                }
            }
            else
            {
                if (!_itemManagement.ItemExist(itemName))
                    if (_itemManagement.AddNewItem(itemName, itemDesc, SessionParameters.UserID, DateTime.Now.ToShortDateString()))
                    {
                        lblMessage.SetMessage(MessageManager.GetMessage("38", itemName));
                        Clear();
                    }
                    else
                    {
                        lblMessage.SetMessage(MessageManager.GetMessage("36"));
                    }
                else
                    lblMessage.SetMessage(MessageManager.GetMessage("37", itemName));
            }


            LoadData(string.Empty, string.Empty);            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            LoadData(string.Empty, string.Empty);
            errorProvider1.Clear();
            txtItemName.Clear();
            txtaDesc.Clear();
            btnUpdate.Enabled = false;
            txtItemName.ReadOnly = false;
            btnAdd.Enabled = true;
            btnSearch.Enabled = true;
            _itemId = 0;
        }

        private int _itemId = 0;
        private void GRID_VIEW_ITEM_DETAILS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            errorProvider1.Clear();
            lblMessage.Clear();
            _itemId = DataFormat.GetInteger(GRID_VIEW_ITEM_DETAILS.Rows[e.RowIndex].Cells[0].Value);
            txtItemName.Text = DataFormat.GetString(GRID_VIEW_ITEM_DETAILS.Rows[e.RowIndex].Cells["Item_Name"].Value);
            txtaDesc.Text = DataFormat.GetString(GRID_VIEW_ITEM_DETAILS.Rows[e.RowIndex].Cells["Item_Desc"].Value);

            btnAdd.Enabled = false;
            btnUpdate.Enabled = true;
            btnSearch.Enabled = false;
            txtItemName.ReadOnly = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(txtItemName.Text.Trim(), txtaDesc.Text.Trim());
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            AddUpdateItem();
        }

        private void NewItem_Load(object sender, EventArgs e)
        {
            SetBGColor(this);
        }
    }
}
