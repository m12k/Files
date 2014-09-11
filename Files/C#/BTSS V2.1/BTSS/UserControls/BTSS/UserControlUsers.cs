using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Biztech.Classes;

namespace Biztech.UserControls.BTSS
{
    public partial class UserControlUsers : UserControl
    {

#region "Constructor"
        public UserControlUsers()
        {
            InitializeComponent();
        }        
        #endregion
        
#region "Declarations"
        
        public event EventHandler OnClose; 
        private ClassUsers classUser;
        private List<GetUserListResult> data;
        private string id = "";
        private string userName;
        private Validation Validation;
         
        #endregion

#region "EventHandlers"

        private void UserControlUsers_Load(object sender, EventArgs e)
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
                    case UserControlButtonsMain.ButtonClick.Close: OnClose(this,e); break;
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
                id = row.Cells["Id"].Value.ToString();
                userName = row.Cells["UserName"].Value.ToString();
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
               var result = from s in data
                            where s.LastName.ToLower().Contains(textSearch.ToLower()) || s.FirstName.ToLower().Contains(textSearch.ToLower()) || s.UserName.ToLower().Contains(textSearch.ToLower())
                            select s;

                userControlListing.DataSource = result.ToList();
                
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
                View(Properties.Resources.View);
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
                 using (var frm = new Forms.BTSS.FormUsers())
                {
                    frm.Operation = Global.Operation.INSERT;
                    frm.Icon = Icon.FromHandle(icon.GetHicon());
                    frm.Text = "Users[NEW]";
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
                using (var frm = new Forms.BTSS.FormUsers())
                {
                    frm.Operation = Global.Operation.UPDATE;
                    frm.Icon = Icon.FromHandle(icon.GetHicon());
                    frm.Text = "Users[EDIT]";
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
        		using (var frm = new Forms.BTSS.FormUsers())
                {
                    frm.Operation = Global.Operation.VIEW;
                    frm.Icon = Icon.FromHandle(icon.GetHicon());
                    frm.Text = "Users[VIEW]";
                    frm.UserId = id;
                    frm.ShowDialog();
                }
                return true ; 
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
                classUser = new ClassUsers(Global.ConnectionString);
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
                dt.Columns.Add("Id");
                dt.Columns.Add("LastName");
                dt.Columns.Add("Firstname");
                dt.Columns.Add("MiddleName");
                dt.Columns.Add("UserName");
                dt.Columns.Add("Password");
                 
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
                        case "Id": col.HeaderText = "User Id"; break;
                        case "LastName": col.HeaderText = "Last Name"; break;
                        case "Firstname": col.HeaderText = "First Name"; break;
                        case "MiddleName": col.HeaderText = "MI"; break;
                        case "UserName": col.HeaderText = "User Name"; break;
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

        private void RefreshOnSave(object sender,EventArgs e)
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
