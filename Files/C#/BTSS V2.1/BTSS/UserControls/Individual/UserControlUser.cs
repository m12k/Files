using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Biztech.Classes;

namespace Biztech.UserControls.Individual
{
    public partial class UserControlUser : UserControl
    {
        public UserControlUser()
        {
            InitializeComponent();
        }

        #region "Declarations"

        public event EventHandler OnClose;
        private ClassUser classUser;     
        private DataTable data;

        private string id = "";
        private string userName;
        private Validation Validation;
        #endregion

        #region "EventHandlers"

        private void UserControlUser_Load(object sender, EventArgs e)
        {
            try
            {
                Initialize();
                InitializeGrid(); 
                RefreshListing();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void userControlButtonsMain_OnButtonClick(EventArgs e, ref bool isOk, UserControlButtonsMain.ButtonClick button, Bitmap icon)
        {
            try
            {
                switch (button)
                {
                    case UserControlButtonsMain.ButtonClick.New: isOk = New(icon); break;
                    case UserControlButtonsMain.ButtonClick.Edit: isOk = Edit(icon); break;
                    case UserControlButtonsMain.ButtonClick.View: isOk = View(icon); break;
                    case UserControlButtonsMain.ButtonClick.Delete: isOk = Delete(); break;
                    case UserControlButtonsMain.ButtonClick.Refresh: RefreshListing(); break;
                    case UserControlButtonsMain.ButtonClick.Close: OnClose(this, e); break;
                }
            }
            catch (Exception)
            {

                throw;
            }
        } 

        private void userControlListing_OnSelectionChanged(DataGridViewRow row)
        {
            try
            { 
                id = row.Cells["user_id"].Value.ToString();
                userName = row.Cells["user_name"].Value.ToString();
                userControlButtonsMain.ButtonsMode = UserControlButtonsMain.Mode.Select; 
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void userControlListing_OnTextSearchChanged(string textSearch)
        {
            try
            {
                var result = from s in data.AsEnumerable()
                             where s.Field<string>("user_last_name").ToLower().Contains(textSearch.ToLower()) || s.Field<string>("user_first_name").ToLower().Contains(textSearch.ToLower()) || s.Field<string>("user_name").ToLower().Contains(textSearch.ToLower())
                             select s;

                if (result.Any())
                    userControlListing.DataSource = result.CopyToDataTable();
                else
                {
                    userControlListing.DataSource = data.Clone(); 
                    userControlButtonsMain.ButtonsMode = UserControlButtonsMain.Mode.Default;
                } 
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void userControlListing_OnRefresh(object sender, EventArgs e)
        {
            try
            { 
                RefreshListing();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void userControlListing_OnRowDoubleClicked(DataGridViewRow row)
        {
            try
            { 
                View( Properties.Resources.View);
            }
            catch (Exception)
            {
                throw;
            }
        }
  
        #endregion

        #region "Methods"

        private bool New(Bitmap icon)
        {
            try
            {
                using (var frm = new Forms.Individual.FormUser())
                {
                    frm.Operation = Global.Operation.INSERT;
                    frm.Icon = Icon.FromHandle(icon.GetHicon());
                    frm.OnSave += new EventHandler(RefreshOnSave);
                    if (frm.ShowDialog() == DialogResult.OK)
                        RefreshListing();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool Edit(Bitmap icon)
        {
            try
            {
                using (var frm = new Forms.Individual.FormUser())
                {
                    frm.Operation = Global.Operation.UPDATE;
                    frm.Icon = Icon.FromHandle(icon.GetHicon());
                    frm.Text = "User[EDIT]";
                    frm.UserId = id;
                    if (frm.ShowDialog() == DialogResult.OK)
                        RefreshListing();
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool View(Bitmap icon)
        {
            try
            {
                using (var frm = new Forms.Individual.FormUser())
                {
                    frm.Operation = Global.Operation.VIEW;
                    frm.Icon = Icon.FromHandle(icon.GetHicon());
                    frm.Text = "User[VIEW]";
                    frm.UserId = id;
                    frm.ShowDialog();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }

        private bool Delete()
        {
            try
            {
                if (Global.ShowQuestion(userName + " will be deleted. Do you want to continue?") == DialogResult.Cancel) { return false; }
                 
                classUser.Id = id;
                classUser.Operation = Global.Operation.DELETE;
                classUser.Save();
                Global.ShowMessage("User " + userName + " successfully deleted!", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                RefreshListing();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
          
        private void Initialize()
        {
            try
            {
                classUser = new ClassUser(Global.ProjectConnectionString, Global.ProjectDataProvider); 
                Validation = new Validation();
                Validation.Validate(this);
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
                DataTable dt = new DataTable();
                dt.Columns.Add("user_id");
                dt.Columns.Add("user_last_name");
                dt.Columns.Add("user_first_name");
                dt.Columns.Add("user_middle_name");
                dt.Columns.Add("user_name");
                dt.Columns.Add("user_password"); 
                
                userControlListing.DataSource = dt;
                 
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
                foreach (DataGridViewColumn col in userControlListing.dataGridViewList.Columns)
                {
                    switch (col.HeaderText)
                    {
                        case "user_id": col.HeaderText = "User Id"; break;
                        case "user_last_name": col.HeaderText = "Last Name"; break;
                        case "user_first_name": col.HeaderText = "First Name"; break;
                        case "user_name": col.HeaderText = "User Name"; break;
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

        private void RefreshOnSave(object sender, EventArgs e)
        {
            try
            {
                RefreshListing();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void RefreshListing()
        {
            try
            {
                id = "";
                userControlButtonsMain.ButtonsMode = UserControlButtonsMain.Mode.Default;

                data = classUser.GetUserList();
                userControlListing.DataSource = data;
            }
            catch (Exception)
            {
                throw;
            }
        }
           
        #endregion
          
    }
}
