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
    public partial class UserControlUser : UserControl
    {

        #region Constructor

        public UserControlUser()
        {
            InitializeComponent();
        }

        #endregion

        #region Declarations
         
        private Logic.Interfaces.Projects.IUser user;
        private DataTable data;

        private ContextMenuStrip menuStrip;
        private string id = "";
        private string userName;
        private BTSS.Common.Validation validation;

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
                id = row.Cells["user_id"].Value.ToString();
                userName = row.Cells["user_name"].Value.ToString();
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
                             where s.Field<string>("user_last_name").ToLower().Contains(textSearch.ToLower()) || s.Field<string>("user_first_name").ToLower().Contains(textSearch.ToLower()) || s.Field<string>("user_name").ToLower().Contains(textSearch.ToLower())
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

        private void displayProjectsMenuStrip_ItemClicked(object sender, EventArgs e)
        {
            try
            {
                //Initialize datatable for reporting
                DataTable dtUserProjects = new DataTable();
                dtUserProjects.Columns.Add("ProjectName");
                dtUserProjects.Columns.Add("Description");
                dtUserProjects.Columns.Add("BusinessOwner");
                dtUserProjects.Columns.Add("IsActive", typeof(bool)); 
                dtUserProjects.Columns.Add("Provider");
                dtUserProjects.Columns.Add("File");
                dtUserProjects.Columns.Add("Datasource");
                dtUserProjects.Columns.Add("DatabaseName");
                                
                //Connection Variables
                Logic.Project project = new BTSS.Logic.Project(Common.Core.ConnectionString);
                Logic.Connection connection = null;
                Common.Core.DataProvider provider = BTSS.Common.Core.DataProvider.OLEDB;
                string connectionString = "";
                string selectedUserName;

                selectedUserName = userControlListing.dataGridViewList.SelectedRows[0].Cells["user_name"].Value.ToString();

                //User's project object
                Logic.Interfaces.Projects.IUser user;

                //Iterate ALL projects
                foreach (Logic.Project p in project.GetAllProjects())
                { 
                    if (p.Provider == "OLEDB")
                    {
                        connectionString = Common.Core.GetConnectionString(p.File, p.UserID, p.Password, p.HasOtherDetails, p.MDW);
                        connection = new BTSS.Logic.Connection(connectionString, BTSS.Common.Core.DataProvider.OLEDB);
                        provider= BTSS.Common.Core.DataProvider.OLEDB;
                    }
                    else if (p.Provider == "SQL")
                    {
                        connectionString = Common.Core.GetConnectionString(p.Datasource, p.DatabaseName, p.UserID, p.Password);
                        connection = new BTSS.Logic.Connection(connectionString, BTSS.Common.Core.DataProvider.SQL);
                        provider= BTSS.Common.Core.DataProvider.SQL;
                    }


                    //check if database has connection
                    if (connection.HasConnection())
                    { 
                        user = new Logic.Projects.User(connectionString, provider);

                        if (user.IsExist(selectedUserName))
                            dtUserProjects.Rows.Add(p.Name, p.Desc, p.BusinessOwner, p.IsActive, p.Provider, p.File, p.Datasource, p.DatabaseName);                          
                    } 
                }
                 
                using (var reportViewer = new Common.ReportViewer())
                {
                    reportViewer.ReportTitle = selectedUserName + " Projects";
                    reportViewer.XMLName = "UsersProjectList";
                    reportViewer.ReportSource = (new Reports.ReportUsersProjectList());
                    reportViewer.SetDatasource(dtUserProjects);
                    reportViewer.ShowReport();
                } 

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void dataGridViewList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right && userControlListing.dataGridViewList.SelectedRows.Count > 0)
                {
                    menuStrip.Items[0].Text = "Display " + userControlListing.dataGridViewList.SelectedRows[0].Cells["user_name"].Value.ToString() + " Projects";
                    menuStrip.Show(MousePosition);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            try
            {
                user = new Logic.Projects.User(BTSS.Common.Core.ProjectConnectionString, BTSS.Common.Core.ProjectDataProvider);
                validation = new BTSS.Common.Validation();
                validation.Validate(this);
                SetControlListingAccessRights();
                 
                menuStrip = new ContextMenuStrip();
                menuStrip.Items.Add("Display Projects");
                menuStrip.Items[0].Click += new EventHandler(displayProjectsMenuStrip_ItemClicked); 
                userControlListing.dataGridViewList.CellMouseDown +=new DataGridViewCellMouseEventHandler(dataGridViewList_CellMouseDown); 
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
  
        private bool New(Bitmap icon)
        {
            try
            {
                using (var frm = new BTSS.Presentation.Projects.FormUser())
                {
                    frm.Operation = BTSS.Common.Core.Operation.INSERT; 
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
                using (var frm = new BTSS.Presentation.Projects.FormUser())
                {
                    frm.Operation = BTSS.Common.Core.Operation.UPDATE; 
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
                using (var frm = new BTSS.Presentation.Projects.FormUser())
                {
                    frm.Operation = BTSS.Common.Core.Operation.VIEW; 
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
                if (BTSS.Common.Core.ShowQuestion(userName + " will be deleted. Do you want to continue?") == DialogResult.Cancel) { return false; }
                 
                user.Id = id;
                user.Operation = BTSS.Common.Core.Operation.DELETE;
                user.Save();
                BTSS.Common.Core.ShowMessage("User " + userName + " successfully deleted!", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                RefreshListing();

                return true;
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
                using (var user = new Logic.User(Common.Core.ConnectionString))
                {
                    var result = user.GetAccessRights(Common.Core.UserId, BTSS.Common.Core.Module.USER);

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
