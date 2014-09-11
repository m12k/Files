using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BTSS.Logic;
using BTSS.Common; 
 
namespace BTSS.Presentation.UserControls
{
    public partial class UserControlProjects : UserControl 
    {

#region Constructor
        public UserControlProjects()
        {
            InitializeComponent();
        }
        #endregion
              
#region Declarations

        private Logic.Interfaces.IProject project; 
        private string id = "";
        private string projectName;
        private List<GetProjectListResult> data;
         
        
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

#region EventHandlers
          
        private void UserControlProject_Load(object sender, EventArgs e)
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

        private void userControlListing_OnTextSearchChanged(string textSearch)
        {
            try
            {
                var result = from s in data
                             where s.Name.ToLower().Contains(textSearch.ToLower()) || s.Desc.ToLower().Contains(textSearch.ToLower())
                             select s;

                userControlListing.DataSource = result.ToList();

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
                projectName = row.Cells["Name"].Value.ToString();
                userControlButtonsMain.ButtonsMode = BTSS.Common.UserControls.UserControlButtonsMain.Mode.Select;
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
           
#region Methods

        private bool New(Bitmap icon)
        {
            try
            {
                using (var frm = new BTSS.Presentation.FormProject())
                {
                    frm.Operation = Core.Operation.INSERT;
                    frm.Icon = Icon.FromHandle(icon.GetHicon());
                    frm.Text = "Project[NEW]";
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
                using (var frm = new BTSS.Presentation.FormProject())
                {
                    frm.Operation = Common.Core.Operation.UPDATE;
                    frm.Icon = Icon.FromHandle(icon.GetHicon());
                    frm.Text = "Project[EDIT]";
                    frm.ProjectId = id;
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
                using (var frm = new BTSS.Presentation.FormProject())
                {
                    frm.Operation = Common.Core.Operation.VIEW;
                    frm.Icon = Icon.FromHandle(icon.GetHicon());
                    frm.Text = "Project[VIEW]";
                    frm.ProjectId = id;
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
                if (!project.IsProjectAllowToDelete(id))
                {
                    Common.Core.ShowMessage("Project has reference user(s). Delete not allowed.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                if (Common.Core.ShowQuestion(projectName + " will be deleted. Do you want to continue?") == DialogResult.Cancel) { return false; }
                 
                project.Id = id;
                project.Operation = Common.Core.Operation.DELETE;
                project.Save();
                Common.Core.ShowMessage("Project " + projectName + " successfully deleted!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                project = new Project(Common.Core.ConnectionString);                 
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
                dt.Columns.Add("Name");
                dt.Columns.Add("Desc");
                dt.Columns.Add("Version");
                dt.Columns.Add("IsActive",typeof(bool));

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
                        case "Id": col.HeaderText = "Project Id"; break;
                        case "Name": col.HeaderText = "Name"; break;
                        case "Desc": col.HeaderText = "Description"; break;
                        case "Version": col.HeaderText = "Version"; break;
                        case "IsActive": col.HeaderText = "Is Active"; break;
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
                    data = project.GetProjectList();
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
                    var result = user.GetAccessRights(Common.Core.UserId, Core.Module.BTSSPROJECTS);

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
 