using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BTSS.Presentation.Projects
{
    public partial class FormUser : Form
    {

        #region "Contructor"

        public FormUser()
        {
            InitializeComponent();
        }

        #endregion
         
        #region "Properties"
         
        public string UserId { get; set; }
        public BTSS.Common.Core.Operation Operation { get; set; }
         
        #endregion
        
        #region "Declarations"

        public event EventHandler OnSave;  
        private Logic.Interfaces.Projects.IUser user; 
        private BTSS.Common.Validation validation;

        private string accountName;
        #endregion

        #region "Event Handlers"
        
        private void FormUser_Load(object sender, EventArgs e)
        {
            try
            {
                Initialize(); 
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void userControlButtonsSave_OnButtonClick(EventArgs e, ref bool isOk, BTSS.Common.UserControls.UserControlButtonsSave.ButtonClick button)
        {
            try
            {
                switch (button)
                {
                    case BTSS.Common.UserControls.UserControlButtonsSave.ButtonClick.Save:
                        if (Save())
                        {
                            if (Operation == BTSS.Common.Core.Operation.UPDATE)
                                DialogResult = DialogResult.OK;
                        }
                        break;
                    case BTSS.Common.UserControls.UserControlButtonsSave.ButtonClick.Cancel: DialogResult = DialogResult.Cancel; break;
                }

            }
            catch (Exception)
            {

                throw;
            }
        } 
        
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                using (var frm = new FormBrowseUser())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        textBoxLastName.Text = frm.User.LastName;
                        textBoxFirstName.Text = frm.User.FirstName;
                        textBoxUserName.Text = frm.User.UserName;
                        accountName = frm.User.AccountName;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region "Methods"
         
        private void Initialize()
        {
            try
            {
                user = new Logic.Projects.User(BTSS.Common.Core.ProjectConnectionString, BTSS.Common.Core.ProjectDataProvider);
                validation = new BTSS.Common.Validation(); 
                validation.Validate(this);

                InitializeGrid();

                if (Operation == BTSS.Common.Core.Operation.UPDATE || Operation == BTSS.Common.Core.Operation.VIEW)
                { 
                    DataRow dr;

                    dr = user.GetUserDetails(UserId);
                    textBoxID.Text = UserId;
                    textBoxLastName.Text = dr["user_last_name"].ToString();
                    textBoxFirstName.Text = dr["user_first_name"].ToString();                     
                    textBoxUserName.Text = dr["user_name"].ToString();
                    textBoxUserName.Tag = dr["user_name"].ToString();
                    accountName = dr["user_name"].ToString().Substring(4, dr["user_name"].ToString().Length - 4).ToString();

                    if (Operation == BTSS.Common.Core.Operation.VIEW)
                    {
                        userControlButtonsSave.Visible = false;
                        Lock();
                    } 
                }
                
                LoadGroupAccess();
                textBoxLastName.Focus();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void InitializeGrid()
        {
            try
            { 
                DataTable dtGroup = new DataTable();
                dtGroup.Columns.Add("grp_id");
                dtGroup.Columns.Add("is_selected", typeof(bool));
                dtGroup.Columns.Add("grp_name");
                dtGroup.Columns.Add("grp_desc");

                dataGridViewGroup.DataSource = dtGroup;

                InitializeGridLayout();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void InitializeGridLayout()
        {
            try
            { 
                foreach (DataGridViewColumn col in dataGridViewGroup.Columns)
                {
                    switch (col.HeaderText)
                    {
                        case "is_selected":
                            col.HeaderText = "";
                            col.Width = 10;
                            break;
                        case "grp_name": col.HeaderText = "Group Name"; break;
                        default:
                            col.Visible = false; break;
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool Save()
        {
            try
            {
                if (!validation.IsTextBoxesValid())
                {
                    BTSS.Common.Core.ShowMessage("Please fill-in required field(s).", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                if (!IsValid()) return false;

                if (Logic.Common.IsValueAlreadyExist(BTSS.Common.Core.ProjectConnectionString, BTSS.Common.Core.ProjectDataProvider, "set_user", "[user_name]", textBoxUserName.Tag.ToString(), textBoxUserName.Text, Operation))
                {
                    BTSS.Common.Core.ShowMessage("User Name already exist. Please check.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                //DataTable groups = new DataTable();
                //groups.Columns.Add("domain");
                //groups.Columns.Add("group");
                //groups.Rows.Add("prd.manulifeusa.com", "business technologies db users");
                //groups.Rows.Add("prd.manulifeusa.com", "SHR MNE1050 Dept Ops Business Tech - RWM");
                //groups.Rows.Add("MLIDDOMAIN1", "business technology db users");


                //if (Operation == BTSS.Common.Core.Operation.UPDATE) accountName = textBoxUserName.Text;

                //if (BTSS.Common.Core.IsUserExistInActiveDirectory(accountName, groups) == false)
                //{
                //    BTSS.Common.Core.ShowMessage("Save Failed!\n\nUser does not belong to group 'business technologies db users'".Replace("\n", Environment.NewLine), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    return false;
                //}
                
                ControlToClass();
                user.Save();

                if (Operation == BTSS.Common.Core.Operation.INSERT) { BTSS.Common.Core.ShowMessage("User " + textBoxUserName.Text + " successfully saved!", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                else { BTSS.Common.Core.ShowMessage("User " + textBoxUserName.Text + " successfully updated!", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                ClearControls();

                if (Operation == BTSS.Common.Core.Operation.INSERT)
                    OnSave(new object(), new EventArgs());

                textBoxLastName.Focus();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
      
        private void Lock()
        {
            try
            {
                textBoxLastName.ReadOnly = true;
                textBoxFirstName.ReadOnly = true;                 
                textBoxUserName.ReadOnly = true; 
                dataGridViewGroup.ReadOnly = true;
                buttonBrowse.Enabled = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ClearControls()
        {
            try
            { 
                textBoxID.Clear();
                textBoxLastName.Clear();
                textBoxFirstName.Clear();                
                textBoxUserName.Clear(); 

                LoadGroupAccess();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ControlToClass()
        {
            try
            {
                user.Operation = Operation;
                user.Id = UserId;
                user.LastName = textBoxLastName.Text;
                user.FirstName = textBoxFirstName.Text;                 
                user.UserName = textBoxUserName.Text;
                 
                user.Data = dataGridViewGroup.DataSource as DataTable;
            }
            catch (Exception)
            {
                throw;
            }
        }
         
        private void LoadGroupAccess()
        {
            try
            {
                dataGridViewGroup.DataSource = user.GetGroupAccess();

                DataRow drr;
                DataTable dt = user.GetUserGroup(UserId);
                foreach (DataRow row in dt.Rows)
                {
                    if (((dataGridViewGroup.DataSource) as DataTable).Select("grp_id = '" + row["grp_id"] + "'", "").Length > 0)
                    {
                        drr = ((dataGridViewGroup.DataSource) as DataTable).Select("grp_id = '" + row["grp_id"] + "'", "").FirstOrDefault();
                        drr["is_selected"] = true;
                    }
                }

                (dataGridViewGroup.DataSource as DataTable).AcceptChanges();

            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsValid()
        {
            try
            { 
                if ((dataGridViewGroup.DataSource as DataTable).Select("is_selected = 1", "", DataViewRowState.ModifiedCurrent | DataViewRowState.OriginalRows).Length == 0)
                {
                    BTSS.Common.Core.ShowMessage("User must have atleast one group.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
  
    }
}
