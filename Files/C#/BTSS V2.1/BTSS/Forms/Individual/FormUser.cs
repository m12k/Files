using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Biztech.Classes;

namespace Biztech.Forms.Individual
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
        public Global.Operation Operation { get; set; }
         
        #endregion
        
        #region "Declarations"

        public event EventHandler OnSave;  
        private ClassUser classUser; 
        private Validation Validation;

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

        private void userControlButtonsSave_OnButtonClick(EventArgs e, ref bool isOk, Biztech.UserControls.UserControlButtonsSave.ButtonClick button)
        {
            try
            {
                switch (button)
                {
                    case Biztech.UserControls.UserControlButtonsSave.ButtonClick.Save:
                        if (Save())
                        {
                            if (Operation == Global.Operation.UPDATE)
                                DialogResult = DialogResult.OK;
                        }
                        break;
                    case Biztech.UserControls.UserControlButtonsSave.ButtonClick.Cancel: DialogResult = DialogResult.Cancel; break;
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
                using (var frm  = new FormBrowseUser())
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
                classUser = new ClassUser(Global.ProjectConnectionString, Global.ProjectDataProvider);
                Validation = new Validation();
                Validation.Validate(this);

                InitializeGrid();
                 
                if (Operation == Global.Operation.UPDATE || Operation == Global.Operation.VIEW)
                { 
                    DataRow dr;

                    dr = classUser.GetUserDetails(UserId);
                    textBoxID.Text = UserId;
                    textBoxLastName.Text = dr["user_last_name"].ToString();
                    textBoxFirstName.Text = dr["user_first_name"].ToString();                     
                    textBoxUserName.Text = dr["user_name"].ToString();
                    textBoxUserName.Tag = dr["user_name"].ToString();
                    accountName = dr["user_name"].ToString().Substring(4, dr["user_name"].ToString().Length - 4).ToString();

                    if (Operation == Global.Operation.VIEW)
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
                if (!Validation.IsTextBoxesValid())
                {
                    Global.ShowMessage("Please fill-in required field(s).", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                if (!IsValid()) return false;
                 
                if (classUser.IsValueAlreadyExist("set_user", "[user_name]", textBoxUserName.Tag.ToString(), textBoxUserName.Text, Operation))
                {
                    Global.ShowMessage("User Name already exist. Please check.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                List<string> groups = new List<string>();
                groups.Add("business technologies db users");
                groups.Add("SHR MNE1050 Dept Ops Business Tech - RWM");

                if (Operation == Global.Operation.UPDATE) accountName = textBoxUserName.Text; 
                
                if (Global.IsUserExistInActiveDirectory(accountName, groups) == false)
                {
                    Global.ShowMessage("Save Failed!\n\nUser does not belong to group 'business technologies db users'".Replace("\n", Environment.NewLine), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                
                ControlToClass();
                classUser.Save(); 

                if (Operation == Global.Operation.INSERT) { Global.ShowMessage("User " + textBoxUserName.Text + " successfully saved!", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                else { Global.ShowMessage("User " + textBoxUserName.Text + " successfully updated!", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                ClearControls();

                if (Operation == Global.Operation.INSERT)
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
                classUser.Operation = Operation;
                classUser.Id = UserId;
                classUser.LastName = textBoxLastName.Text;
                classUser.FirstName = textBoxFirstName.Text;                 
                classUser.Username = textBoxUserName.Text;
                 
                classUser.Data = dataGridViewGroup.DataSource as DataTable;
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
                dataGridViewGroup.DataSource = classUser.GetGroupAccess();

                DataRow drr;
                DataTable dt = classUser.GetUserGroup(UserId);
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
                    Global.ShowMessage("User must have atleast one group.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
