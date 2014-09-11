using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BTSS.Presentation.UserControls.Projects
{
    public partial class UserControlGroup : UserControl
    {

#region "Constructor" 

        public UserControlGroup()
        {
            InitializeComponent();
        } 
       
#endregion
        
#region "Declarations" 

        private Logic.Interfaces.Projects.IGroup group;
        private DataTable data; 
        private string id = "";
        private string groupName;
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

        private void userControlButtonsMain_OnButtonClick(EventArgs e, ref bool isOk, BTSS.Common.UserControls.UserControlButtonsMain.ButtonClick button, Bitmap icon)
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
                id = row.Cells["grp_id"].Value.ToString();
                groupName = row.Cells["grp_name"].Value.ToString();
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
                var result = from s in data.AsEnumerable()                         
                        where s.Field<string>("grp_name").ToLower().Contains(textSearch.ToLower()) || s.Field<string>("grp_desc").ToLower().Contains(textSearch.ToLower())
                        select s;

                if (result.Any())
                    userControlListing.DataSource = result.CopyToDataTable();
                else
                {
                    userControlListing.DataSource = data.Clone();
                    userControlButtonsMain.ButtonsMode = BTSS.Common.UserControls.UserControlButtonsMain.Mode.Default; 
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
                View(BTSS.Common.Properties.Resources.View);
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
                using (var frm = new Presentation.Projects.FormGroup())
                {
                    frm.Operation = BTSS.Common.Core.Operation.INSERT; 
                    frm.Icon = Icon.FromHandle(icon.GetHicon());
                    frm.Text = "Group[NEW]";
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
                using (var frm = new Presentation.Projects.FormGroup())
                {
                    frm.Operation = BTSS.Common.Core.Operation.UPDATE; 
                    frm.Icon = Icon.FromHandle(icon.GetHicon());
                    frm.Text = "Group[EDIT]";
                    frm.GroupId = id;
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
                using (var frm = new Presentation.Projects.FormGroup())
                {
                    frm.Operation = BTSS.Common.Core.Operation.VIEW; 
                    frm.Icon = Icon.FromHandle(icon.GetHicon());
                    frm.Text = "Group[VIEW]";
                    frm.GroupId = id;
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
                if (!group.IsGroupAllowToDelete(id))
                {
                    Common.Core.ShowMessage("Group has reference user(s). Delete not allowed.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                if (Common.Core.ShowQuestion(groupName + " will be deleted. Do you want to continue?") == DialogResult.Cancel) { return false; }
 
                group.Id = id;
                group.Operation = BTSS.Common.Core.Operation.DELETE; 
                group.Save();
                Common.Core.ShowMessage("Group " + groupName + " successfully deleted!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
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
                group = new Logic.Projects.Group(Common.Core.ProjectConnectionString, Common.Core.ProjectDataProvider);
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
                dt.Columns.Add("grp_id");
                dt.Columns.Add("grp_name");
                dt.Columns.Add("grp_desc"); 
                 
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
                        case "grp_id": col.HeaderText = "Group Id"; break;
                        case "grp_name": col.HeaderText = "Group Name"; break;
                        case "grp_desc": col.HeaderText = "Description"; break;
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
                userControlButtonsMain.ButtonsMode = BTSS.Common.UserControls.UserControlButtonsMain.Mode.Default;

                using (var loader = new Common.Loader<string>(o =>
                { 
                    data = group.GetGroupList();
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
                using (var user = new Logic.User(Common.Core.ConnectionString))
                {
                    var result = user.GetAccessRights(Common.Core.UserId, BTSS.Common.Core.Module.MODULE);

                    userControlListing.CanEdit = result.CanEdit.Value;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        
#endregion

    }
}
