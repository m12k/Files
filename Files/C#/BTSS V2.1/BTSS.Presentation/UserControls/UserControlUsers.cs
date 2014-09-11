using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BTSS.Presentation.UserControls
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
         
        private Logic.Interfaces.IUser user;
        private List<Logic.GetUserListResult> data;
        private string id = "";
        private string userName;
        private Common.Validation validation;

        public delegate void OnCloseControlEvent(TabPage tabPage);
        public event OnCloseControlEvent OnCloseControl;

        protected void OnClosed(TabPage tabPage)
        {
            if (OnCloseControl != null)
            {
                OnCloseControl(tabPage);
            }

        }
        private TabPage _tabPage = null;

        public TabPage Tab
        {
            get { return _tabPage; }
            set { _tabPage = value; }
        }
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
        
       private void userControlButtonsMain_OnButtonClick(EventArgs e, ref bool isOk, Common.UserControls.UserControlButtonsMain.ButtonClick button, Bitmap icon)
        {
            try
            {
                switch (button)
                {
                    case BTSS.Common.UserControls.UserControlButtonsMain.ButtonClick.New: isOk = New(icon); break;
                    case BTSS.Common.UserControls.UserControlButtonsMain.ButtonClick.Edit: isOk = Edit(icon); break;
                    case BTSS.Common.UserControls.UserControlButtonsMain.ButtonClick.View: isOk = View(icon); break;
                    case BTSS.Common.UserControls.UserControlButtonsMain.ButtonClick.Delete: isOk = Delete(); break;
                    case BTSS.Common.UserControls.UserControlButtonsMain.ButtonClick.Refresh: RefreshListing(); break;
                    case BTSS.Common.UserControls.UserControlButtonsMain.ButtonClick.Close: OnCloseControl(_tabPage); break;
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
                userControlButtonsMain.ButtonsMode = BTSS.Common.UserControls.UserControlButtonsMain.Mode.Select; 
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
                View(Common.Properties.Resources.View);
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
                 using (var frm = new BTSS.Presentation.FormUser())
                {
                    frm.Operation = Common.Core.Operation.INSERT;
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
                using (var frm = new BTSS.Presentation.FormUser())
                {
                    frm.Operation = Common.Core.Operation.UPDATE;
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
        		using (var frm = new BTSS.Presentation.FormUser())
                {
                    frm.Operation = Common.Core.Operation.VIEW;
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
                if (Common.Core.ShowQuestion(userName + " will be deleted. Do you want to continue?") == DialogResult.Cancel) { return false; }

                user.Id = id;
                user.Operation = Common.Core.Operation.DELETE;
                user.Save();
                Common.Core.ShowMessage("User " + userName + " successfully deleted!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                user = new BTSS.Logic.User(Common.Core.ConnectionString);
                validation = new BTSS.Common.Validation(); 
                validation.Validate(this);
                SetControlListingAccessRights();
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
                userControlButtonsMain.ButtonsMode = BTSS.Common.UserControls.UserControlButtonsMain.Mode.Default;

                using (var loader = new Common.Loader<string>(o =>
                {
                    data = user.GetUserList();                    
                }))
                {
                    loader.RunWorker();
                };

                userControlListing.DataSource = data; 
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private void SetControlListingAccessRights()
        {
            try
            {
                var result = user.GetAccessRights(Common.Core.UserId, BTSS.Common.Core.Module.BTSSUSERS);

                userControlListing.CanEdit = result.CanEdit.Value;                 
            }
            catch (Exception)
            {
                throw;
            }
        }


#endregion 
              
    }
}
