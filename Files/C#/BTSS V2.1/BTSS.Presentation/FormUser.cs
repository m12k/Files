using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BTSS.Presentation
{
    public partial class FormUser : Form
    {

        #region "Constructor"
        
        public FormUser()
        {
            InitializeComponent();
        }

        #endregion

        #region "Properties"

        public string UserId { get; set; }
        public Common.Core.Operation Operation { get; set; }

        #endregion

        #region "Declarations"

        public event EventHandler OnSave;
        private Logic.Interfaces.IUser user; 
        private Common.Validation validation;

        #endregion 

        #region "Event Handlers"

        private void FormUsers_Load(object sender, EventArgs e)
        {
            try
            {
                Initialize();
                InitializeGrid();
                LoadProjects();
                LoadGroups(); 
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void userControlButtonsSave1_OnButtonClick(EventArgs e, ref bool isOk, Common.UserControls.UserControlButtonsSave.ButtonClick button)
        {
            try
            {
                switch (button)
                {
                    case Common.UserControls.UserControlButtonsSave.ButtonClick.Save:
                        if (Save())
                        {
                            if (Operation == Common.Core.Operation.UPDATE)
                                DialogResult = DialogResult.OK;
                        }
                        break;
                    case Common.UserControls.UserControlButtonsSave.ButtonClick.Cancel: DialogResult = DialogResult.Cancel; break;
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
                        textBoxUserName.Text = frm.User.Domain + "\\" + frm.User.UserName;
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
                user = new BTSS.Logic.User(Common.Core.ConnectionString);
                validation = new BTSS.Common.Validation(); 
                validation.Validate(this); 

                if (Operation == Common.Core.Operation.UPDATE || Operation == Common.Core.Operation.VIEW)
                {
                    Logic.GetUserDetailsResult result = user.GetUserDetails(UserId);

                    textBoxID.Text = UserId;
                    textBoxID.Tag = UserId;
                     
                    textBoxLastName.Text = result.LastName;
                    textBoxLastName.Tag = result.LastName;

                    textBoxFirstName.Text = result.FirstName;
                    textBoxFirstName.Tag = result.FirstName;

                    textBoxMI.Text = result.MiddleName;
                    textBoxMI.Tag = result.MiddleName;

                    textBoxUserName.Text = result.UserName;
                    textBoxUserName.Tag = result.UserName;
                     
                    LoadProjects();
                    LoadGroups();
                   
                    if (Operation == Common.Core.Operation.VIEW)
                    {
                        userControlButtonsSave.Visible = false;
                        Lock();
                    } 
                }
                 
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
                DataTable dtUserProject = new DataTable();
                dtUserProject.Columns.Add("Id");
                dtUserProject.Columns.Add("UserId");
                dtUserProject.Columns.Add("IsSelected", typeof(bool));
                dtUserProject.Columns.Add("ProjId");
                dtUserProject.Columns.Add("Name");
                dtUserProject.Columns.Add("Desc");

                dataGridViewProject.DataSource = dtUserProject;


                DataTable dtUserGroup = new DataTable();
                dtUserGroup.Columns.Add("Id");
                dtUserGroup.Columns.Add("UserId");
                dtUserGroup.Columns.Add("IsSelected", typeof(bool));
                dtUserGroup.Columns.Add("GroupId");
                dtUserGroup.Columns.Add("Name");
                dtUserGroup.Columns.Add("Desc");

                dataGridViewGroup.DataSource = dtUserGroup;

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
                        case "IsSelected": col.HeaderText = "";
                            col.Width = 20;                             
                            break;
                        case "Name": col.HeaderText = "Name";
                            col.ReadOnly = true;
                            break;
                        case "Desc": col.HeaderText = "Description";
                            col.ReadOnly = true;
                            break;
                        default:
                            col.Visible = false; break;
                    }
                }

                foreach (DataGridViewColumn col in dataGridViewProject.Columns)
                {
                    switch (col.HeaderText)
                    {
                        case "IsSelected": col.HeaderText = "";
                            col.Width = 20;
                            break;
                        case "Name": col.HeaderText = "Name";
                            col.ReadOnly = true;
                            break;
                        case "Desc": col.HeaderText = "Description";
                            col.ReadOnly = true;
                            break;
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
                    Common.Core.ShowMessage("Please fill-in required field(s).", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                    
                ControlToClass();
                user.Save();

                if (Operation == Common.Core.Operation.INSERT) { Common.Core.ShowMessage("User " + textBoxUserName.Text + " successfully saved!", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                else { Common.Core.ShowMessage("User " + textBoxUserName.Text + " successfully updated!", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                ClearControls();

                if (Operation == Common.Core.Operation.INSERT)
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
                buttonBrowse.Enabled = false;
                dataGridViewGroup.ReadOnly = true;
                dataGridViewProject.ReadOnly = true;
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
                textBoxMI.Clear(); ;
                textBoxUserName.Clear(); 
                LoadProjects();
                LoadGroups();
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
                user.MiddleName = textBoxMI.Text;
                user.UserName = textBoxUserName.Text; 
                user.DataUserProject = dataGridViewProject.DataSource as DataTable;
                user.DataUserGroup = dataGridViewGroup.DataSource as DataTable;
            }
            catch (Exception)
            {
                throw;
            }
        }
          
        private void LoadProjects()
        {
            try
            {
                DataTable dt = user.GetUsersProjectList(textBoxID.Text);
                dataGridViewProject.DataSource = dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void LoadGroups()
        {
            try
            {
                DataTable dt = user.GetUserGroup(textBoxID.Text);
                dataGridViewGroup.DataSource = dt;
            }
            catch (Exception)
            {
                throw;
            }
        }
         
        #endregion
 
    }
}
