using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Biztech.Classes;

namespace Biztech.Forms.BTSS
{
    public partial class FormUsers : Form
    {

        #region "Constructor"
        
        public FormUsers()
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
        private ClassUsers classUser; 
        private Validation Validation;

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

        private void userControlButtonsSave1_OnButtonClick(EventArgs e, ref bool isOk, Biztech.UserControls.UserControlButtonsSave.ButtonClick button)
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
          
        #endregion

        #region "Methods"

   private void Initialize()
        {
            try
            {
                classUser = new ClassUsers(Global.ConnectionString);
                Validation = new Validation();
                Validation.Validate(this);

                if (Operation == Global.Operation.UPDATE || Operation == Global.Operation.VIEW)
                {
                    GetUserDetailsResult result = classUser.GetUserDetails(UserId);

                    textBoxID.Text = UserId;
                    textBoxLastName.Text = result.LastName;
                    textBoxFirstName.Text = result.FirstName;
                    textBoxMI.Text = result.MiddleName;
                    textBoxUserName.Text = result.UserName;
                    textBoxUserName.Tag = result.UserName;
                    textBoxPassword.Text = result.Password;
                    textBoxRetypePassword.Text = result.Password;

                    LoadProjects();
                    LoadGroups();


                    if (Operation == Global.Operation.VIEW)
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
                if (!Validation.IsTextBoxesValid())
                {
                    Global.ShowMessage("Please fill-in required field(s).", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                
                if (Global.IsCodeExist(textBoxUserName.Tag.ToString(), textBoxUserName.Text, "[User]", "UserName", Operation))
                {
                    Global.ShowMessage("User Name already exist.\n\nPlease Check.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                textBoxMI.ReadOnly = true;
                textBoxUserName.ReadOnly = true;
                textBoxPassword.ReadOnly = true;
                textBoxRetypePassword.ReadOnly = true;
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
                textBoxPassword.Clear();
                textBoxRetypePassword.Clear();
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
                classUser.Operation = Operation;
                classUser.Id = UserId;
                classUser.LastName = textBoxLastName.Text;
                classUser.FirstName = textBoxFirstName.Text;
                classUser.MI = textBoxMI.Text;
                classUser.userName = textBoxUserName.Text;
                classUser.Password = textBoxPassword.Text;
                classUser.DataUserProject = dataGridViewProject.DataSource as DataTable;
                classUser.DataUserGroup = dataGridViewGroup.DataSource as DataTable;
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
                DataTable dt = classUser.GetUsersProjectList(textBoxID.Text);
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
                DataTable dt = classUser.GetUserGroup(textBoxID.Text);
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
