using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms; 
using System.Deployment.Application;
using BTSS.Common;
using BTSS.Logic;

namespace BTSS.Presentation
{
    public partial class FormMain : Form
    {

    #region "Constructor"

    public FormMain()
    {
        InitializeComponent();
    }

    #endregion
     
    #region "Declarations"

    private Logic.Connection connection;
    private Logic.User user; 
    private Common.Validation Validation;
    private Core.ConnectionStatus connectionStatus = BTSS.Common.Core.ConnectionStatus.Disconnected;
    private Timer timerUpdateAvailable;
    private Timer timerConnected;
    private int ticksCount;
        
    private bool hasModuleAccess;
    private bool hasGroupAccess;
    private bool hasUserAccess;

    #endregion
     
    #region Events

    private void FormMain2_Load(object sender, EventArgs e)
    {
        try
        {             
            CheckUpdate();
            Initialize();
            LoadProject();
        }
        catch (Exception)
        {
            throw;
        } 
    }
 
    private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
    {
        if (buttonConnect.Text == "Connect")
        {
            panelHead.Size = new Size(panelHead.Width, 110);
        }
        else
        {
            if (BTSS.Common.Core.ProjectDataProvider == BTSS.Common.Core.DataProvider.OLEDB)
                panelHead.Size = new Size(panelHead.Width, 130);
            else if (BTSS.Common.Core.ProjectDataProvider == BTSS.Common.Core.DataProvider.SQL)
                panelHead.Size = new Size(panelHead.Width, 140);
        }
    }

    private void treeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
    {
        panelHead.Size = new Size(panelHead.Width, 80);
    }

    private void projectsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        try
        { 
            Show(new UserControls.UserControlProjects(), projectsToolStripMenuItem.Tag.ToString());
        }
        catch (Exception)
        { 
            throw;
        }  
    }

    private void groupsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        try
        {
           Show(new UserControls.UserControlGroups(), groupsToolStripMenuItem.Tag.ToString());
        }
        catch (Exception)
        {
            throw;
        } 
    }

    private void usersToolStripMenuItem_Click(object sender, EventArgs e)
    {
        try
        {
            Show(new UserControls.UserControlUsers(), usersToolStripMenuItem.Tag.ToString());
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void buttonConnect_Click(object sender, EventArgs e)
    {
        try
        {
            if (buttonConnect.Text == "Connect")
            {

                var result = ((comboBoxProject.Items[comboBoxProject.SelectedIndex]) as GetUserProjectsResult);
                string connectionString = "";

                //Start of: check connection
                if (result.Provider == "OLEDB")
                {
                    connectionString = Common.Core.GetConnectionString(result.File, result.UserID, result.Password, result.HasOtherDetails.Value, result.MDW);
                    connection = new Connection(connectionString, Core.DataProvider.OLEDB);
                }
                else if (result.Provider == "SQL")
                {
                    connectionString = Common.Core.GetConnectionString(result.Datasource, result.DatabaseName, result.UserID, result.Password);
                    connection = new Connection(connectionString, Core.DataProvider.SQL);
                }

                if (!connection.HasConnection())
                {
                    Common.Core.ShowMessage("Connection Failed.\n\nPlease check.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                //End of: check connection

                treeProject.Nodes["NodeHeader"].Nodes["NodeName"].Text = "Name: " + result.Name;
                treeProject.Nodes["NodeHeader"].Nodes["NodeDescription"].Text = "Description: " + result.Desc;


                if (result.Provider == "OLEDB")
                {
                    foreach (TreeNode n in treeProject.Nodes["NodeHeader"].Nodes)
                    {
                        if (n.Name == "NodeDatasource" || n.Name == "NodeDatabaseName")
                            n.Remove();
                    }

                    treeProject.Nodes["NodeHeader"].Nodes.Add("NodeDatabase", "Database: " + result.File);

                    Common.Core.ProjectDataProvider = Common.Core.DataProvider.OLEDB;
                }
                else if (result.Provider == "SQL")
                {
                    foreach (TreeNode n in treeProject.Nodes["NodeHeader"].Nodes)
                    {
                        if (n.Name == "NodeDatabase")
                            n.Remove();
                    }

                    treeProject.Nodes["NodeHeader"].Nodes.Add("NodeDatasource", "Datasource: " + result.Datasource);
                    treeProject.Nodes["NodeHeader"].Nodes.Add("NodeDatabaseName", "Database Name: " + result.DatabaseName);

                    Common.Core.ProjectDataProvider = Common.Core.DataProvider.SQL;
                }
                 
                Common.Core.ProjectConnectionString = connectionString;
                Common.Core.ProjectName = comboBoxProject.Text;
            }

            treeProject.CollapseAll();
            SetConnectionStatus();
            SetMenuStatus();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void userToolStripMenuItem_Click(object sender, EventArgs e)
    {
        try
        {
            Show(new BTSS.Presentation.UserControls.Projects.UserControlUser(), buttonUser.Tag.ToString());
        }
        catch (Exception)
        {                
            throw;
        }
    }

    private void moduleToolStripMenuItem_Click(object sender, EventArgs e)
    {
        try
        {
            Show(new BTSS.Presentation.UserControls.Projects.UserControlModule(), buttonModule.Tag.ToString());
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void groupToolStripMenuItem_Click(object sender, EventArgs e)
    {
        try
        {
            Show(new BTSS.Presentation.UserControls.Projects.UserControlGroup(), buttonGroup.Tag.ToString());
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        try
        {
            Application.Exit();
        }
        catch (Exception)
        {
            throw;
        } 
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
        try
        {
            using (var frm = new FormAbout())
            {
                frm.ShowDialog();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void projectListToolStripMenuItem_Click(object sender, EventArgs e)
    {
        try
        {
            Logic.Project classProject = new  Project(BTSS.Common.Core.ConnectionString);

            DataTable dt = new DataTable();
            dt = classProject.GetProjectListForReport();

            using (var reportViewer = new Common.ReportViewer())
            {
                reportViewer.ReportTitle = "Project List";
                reportViewer.XMLName = "ProjectList";
                reportViewer.ReportSource = (new Reports.ReportProjectList());
                reportViewer.SetDatasource(dt);
                reportViewer.ShowReport();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void getUpdateToolStripMenuItem_Click(object sender, EventArgs e)
    {
        try
        {
            GetUpdate();                 
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void toolStripLabelProject_Click(object sender, EventArgs e)
    {
        try
        {
            projectsToolStripMenuItem_Click(sender, e);
        }
        catch (Exception)
        {
            throw;
        }

    }

    private void toolStripButtonGroups_Click(object sender, EventArgs e)
    {
        try
        {
            groupsToolStripMenuItem_Click(sender, e);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void toolStripButtonUsers_Click(object sender, EventArgs e)
    {
        try
        {
            usersToolStripMenuItem_Click(sender, e);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void toolStripButtonModule_Click(object sender, EventArgs e)
    {
        try
        {
            moduleToolStripMenuItem_Click(sender, e);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void toolStripButtonGroup_Click(object sender, EventArgs e)
    {
        try
        {
            groupToolStripMenuItem_Click(sender, e);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void toolStripButtonUser_Click(object sender, EventArgs e)
    {
        try
        {
            userToolStripMenuItem_Click(sender, e);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void buttonRefreshProject_Click(object sender, EventArgs e)
    {
        try
        {
            LoadProject();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void auditTrailToolStripMenuItem_Click(object sender, EventArgs e)
    {
        try
        {
            using (FormAuditTrail frm = new FormAuditTrail())
            {
                frm.ShowDialog();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ActiveDirectoryUsersListToolStripMenuItem_Click(object sender, EventArgs e)
    {
        try
        {
            using (FormActiveDirectory frm = new FormActiveDirectory())
            {
                frm.ShowDialog();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void groupOfUsersToolStripMenuItem_Click(object sender, EventArgs e)
    {
        try
        {
            Logic.Interfaces.Projects.IUser user = new Logic.Projects.User(BTSS.Common.Core.ProjectConnectionString, Common.Core.ProjectDataProvider);
            
            DataTable dt = new DataTable();
            dt = user.GetUserGroupList();
            dt.DefaultView.Sort = "full_name ASC";
             

            using (var reportViewer = new Common.ReportViewer())
            {
                reportViewer.ReportTitle = "Group of Users";
                reportViewer.XMLName = "GroupOfUsers";
                reportViewer.ReportSource = (new Reports.ReportGroupOfUsers());
                reportViewer.SetDatasource(dt.DefaultView.ToTable());
                reportViewer.ShowReport();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void domainOfUsersToolStripMenuItem_Click(object sender, EventArgs e)
    {
        try
        {
            //TODO: Fix first - add domain field to set_user table from projects.
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void projectAuditTrailToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        try
        {
            using (Projects.FormAuditTrail frm = new Projects.FormAuditTrail())
            {
                frm.ShowDialog();
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
            user = new User(Core.ConnectionString);
            Validation = new Validation();
            timerConnected = new Timer();
            timerConnected.Tick += new EventHandler(TimerConnected_Ticks);
            
            Validation.Validate(this);
            toolStripStatusLabelUserName.Text = "User: " + Core.FullName;
            SetMenuStatus();
            
            toolTipMain.SetToolTip(buttonRefreshProject, "Refresh Database List");
            toolTipMain.SetToolTip(buttonConnect, "Connect to Database");

            SetAccessRights();

            StringBuilder sb = new StringBuilder();
            Text = sb.Append(Text).Append(" ").Append(Application.ProductVersion).ToString();          
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void Show(UserControl uc, string moduleDirectory)
    {
        try
        {

            foreach (TabPage t in tabControlMain.TabPages)
            {
                if (string.Compare(t.Text, uc.Tag.ToString(), true) == 0)
                {
                    tabControlMain.SelectedTab = t;
                    return;
                }
            }

            Common.UserControls.UserControlMain userControlMain = new BTSS.Common.UserControls.UserControlMain();
             
            userControlMain.ModuleDirectory = moduleDirectory;
            userControlMain.Dock = DockStyle.Fill;
            userControlMain.Control = uc;
             
            //add Main contriol to Panel
            panelRight.Controls.Add(userControlMain); 

            //initialize new tab page
            TabPage tabPage = new TabPage();
            tabPage.Text = uc.Tag.ToString();
            tabPage.Tag = uc.Name;
            tabPage.Font = new Font("Microsoft Sans Serif", (float)8.25, FontStyle.Regular); 
            tabPage.Controls.Add(userControlMain);
            tabControlMain.TabPages.Add(tabPage);
            
            switch (uc.Name)
            {
                case "UserControlProjects":
                    (uc as BTSS.Presentation.UserControls.UserControlProjects).Tab = tabPage;
                    (uc as BTSS.Presentation.UserControls.UserControlProjects).OnCloseControl += new BTSS.Presentation.UserControls.UserControlProjects.OnCloseControlEvent(ControlMainClose);
                    break;
                case "UserControlUsers":
                    (uc as BTSS.Presentation.UserControls.UserControlUsers).Tab = tabPage;
                    (uc as BTSS.Presentation.UserControls.UserControlUsers).OnCloseControl += new BTSS.Presentation.UserControls.UserControlUsers.OnCloseControlEvent(ControlMainClose);
                    break;
                case "UserControlGroups":
                    (uc as BTSS.Presentation.UserControls.UserControlGroups).Tab = tabPage;
                    (uc as BTSS.Presentation.UserControls.UserControlGroups).OnCloseControl +=  new BTSS.Presentation.UserControls.UserControlGroups.OnCloseControlEvent(ControlMainClose);
                    break; 
                case "UserControlModule":
                    (uc as BTSS.Presentation.UserControls.Projects.UserControlModule).Tab = tabPage;
                    (uc as BTSS.Presentation.UserControls.Projects.UserControlModule).OnCloseControl += new BTSS.Presentation.UserControls.Projects.UserControlModule.OnCloseControlEvent(ControlMainClose);
                    break;
                case "UserControlGroup":
                    (uc as BTSS.Presentation.UserControls.Projects.UserControlGroup).Tab = tabPage;
                    (uc as BTSS.Presentation.UserControls.Projects.UserControlGroup).OnCloseControl += new BTSS.Presentation.UserControls.Projects.UserControlGroup.OnCloseControlEvent(ControlMainClose);
                    break;
                case "UserControlUser":
                    (uc as BTSS.Presentation.UserControls.Projects.UserControlUser).Tab = tabPage;
                    (uc as BTSS.Presentation.UserControls.Projects.UserControlUser).OnCloseControl += new BTSS.Presentation.UserControls.Projects.UserControlUser.OnCloseControlEvent(ControlMainClose);
                    break;
            }

            tabControlMain.SelectedTab = tabPage; 
             
        }
        catch (Exception)
        {

            throw;
        } 
    }
            

    private void ControlMainClose(TabPage tabPage)
    {
        try
        {
            tabControlMain.TabPages.Remove(tabPage);
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void LoadProject()
    {
        try
        {
            Logic.User user = new User(BTSS.Common.Core.ConnectionString);

            var result = from s in user.GetUserProjects(BTSS.Common.Core.UserId)
                         orderby s.Name
                         select s;


            comboBoxProject.DataSource = result.ToList();
            comboBoxProject.DisplayMember = "Name";
            comboBoxProject.ValueMember = "Id";
            if (comboBoxProject.Items.Count > 0) comboBoxProject.SelectedIndex = 0;
            else buttonConnect.Enabled = false;

        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ClearDetails()
    {
        try
        {

            foreach (TreeNode n in treeProject.Nodes["NodeHeader"].Nodes)
            {
                if (n.Name == "NodeDatasource" || n.Name == "NodeDatabaseName" || n.Name == "NodeDatabase")
                    n.Remove();
                else if (n.Name == "NodeName")
                    n.Text = "Name: ";
                else if (n.Name == "NodeDescription")
                    n.Text = "Description: ";
            } 

             
            comboBoxProject.Enabled = true;

            foreach (TabPage tab in tabControlMain.TabPages)
            {
                tabControlMain.TabPages.Remove(tab);
            }

        }
        catch (Exception)
        {
            throw;
        }
    }

    private void SetConnectionStatus()
    {
        try
        {
            if (connectionStatus == Core.ConnectionStatus.Disconnected)
            {
                connectionStatus = Core.ConnectionStatus.Connected;
                buttonConnect.Text = "Disconnect";
                comboBoxProject.Enabled = false;

                ticksCount = 0;
                timerConnected.Start();  
            }
            else if (connectionStatus == Core.ConnectionStatus.Connected)
            {
                connectionStatus = Core.ConnectionStatus.Disconnected;
                buttonConnect.Text = "Connect";
                ClearDetails();
                labelConnected.Visible = false;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void SetMenuStatus()
    {
        try
        {
            if (connectionStatus == Core.ConnectionStatus.Disconnected)
            {
                buttonModule.Enabled = false;
                buttonGroup.Enabled = false;
                buttonUser.Enabled = false;
                 
                buttonRefreshProject.Enabled = true;
            }
            else if (connectionStatus == Core.ConnectionStatus.Connected)
            {                    
                buttonModule.Enabled = true && hasModuleAccess;
                buttonGroup.Enabled = true && hasGroupAccess;
                buttonUser.Enabled = true && hasUserAccess;
                 
                buttonRefreshProject.Enabled = false;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
           
    private void GetUpdate()
    {
        try
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {

                UpdateCheckInfo info = null;
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                try
                {
                    info = ad.CheckForDetailedUpdate();

                }
                catch (DeploymentDownloadException dde)
                {
                    MessageBox.Show("The new version of the application cannot be downloaded at this time. \n\nPlease check your network connection, or try again later. Error: " + dde.Message);
                    return;
                }
                catch (InvalidDeploymentException ide)
                {
                    MessageBox.Show("Cannot check for a new version of the application. The ClickOnce deployment is corrupt. Please redeploy the application and try again. Error: " + ide.Message);
                    return;
                }
                catch (InvalidOperationException ioe)
                {
                    MessageBox.Show("This application cannot be updated. It is likely not a ClickOnce application. Error: " + ioe.Message);
                    return;
                }

                if (info.UpdateAvailable)
                {
                    try
                    {
                        ad.Update();
                        MessageBox.Show("The application has been upgraded, and will now reload.");
                        Application.Restart();
                    }
                    catch (DeploymentDownloadException dde)
                    {
                        MessageBox.Show("Cannot install the latest version of the application. \n\nPlease check your network connection, or try again later. Error: " + dde);
                        return;
                    }

                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void CheckUpdate()
    {
        try
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
                UpdateCheckInfo info = null;

                timerUpdateAvailable = new Timer();

                timerUpdateAvailable.Tick += new EventHandler(TimerTick);

                info = ad.CheckForDetailedUpdate();

                try
                {
                    if (info.UpdateAvailable)
                    {
                        getUpdateToolStripMenuItem.Enabled = true;
                        timerUpdateAvailable.Interval = 500;
                        timerUpdateAvailable.Start();
                    }
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void TimerTick(object sender, EventArgs e)
    {
        try
        {
            if (toolStripStatusLabelUpdateAvailable.Text == "")
                toolStripStatusLabelUpdateAvailable.Text = "Update Available!";
            else
                toolStripStatusLabelUpdateAvailable.Text = "";

        }
        catch (Exception)
        {
            throw;
        }
    }

    private void SetAccessRights()
    {
        try
        {
            int counter = 1;
            bool hasBTSSView = false;

            Core.Module mod = Core.Module.BTSSPROJECTS;

            //BTSS Module checking
            while (counter <= 4)
            {
                if (counter == 1)
                    mod = Core.Module.BTSSPROJECTS;
                else if (counter == 2)
                    mod = Core.Module.BTSSGROUP;
                else if (counter == 3)
                    mod = Core.Module.BTSSUSERS;
                else if (counter == 4)
                    mod = Core.Module.PREFERENCES;

                var result = user.GetAccessRights(Common.Core.UserId, mod);

                if (counter == 1 && !result.CanView.Value)
                    projectsToolStripMenuItem.Visible = false;
                if (counter == 2 && !result.CanView.Value)
                    groupsToolStripMenuItem.Visible = false;
                if (counter == 3 && !result.CanView.Value)
                    usersToolStripMenuItem.Visible = false;
                if (counter == 4 && !result.CanView.Value)
                    settingsToolStripMenuItem.Visible = false;

                if (result.CanView.Value)
                    hasBTSSView = true;

                counter++;
            }

            if (!hasBTSSView) setupToolStripMenuItem.Visible = false;

            counter = 1;
            //Individual project checking
            while (counter <= 3)
            {
                if (counter == 1)
                    mod = Core.Module.MODULE; 
                else if (counter == 2)
                    mod = Core.Module.GROUP;
                else if (counter == 3)
                    mod = Core.Module.USER;

                var result = user.GetAccessRights(Common.Core.UserId, mod);

                if (counter == 1 && result.CanView.Value)
                    hasModuleAccess = true;
                if (counter == 2 && result.CanView.Value)
                    hasGroupAccess = true;
                if (counter == 3 && result.CanView.Value)
                    hasUserAccess = true;

                counter++;
            }

            if (!hasModuleAccess && !hasGroupAccess && !hasUserAccess)
            {
                comboBoxProject.Enabled = false;
                buttonRefreshProject.Enabled = false;
                buttonConnect.Enabled = false;
            }
                        
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void TimerConnected_Ticks(object sender, EventArgs e)
    {
        try
        {
            if (ticksCount == 6) timerConnected.Stop();
             
            labelConnected.Visible = labelConnected.Visible ? false : true;
            ticksCount++;
        }
        catch (Exception)
        {
            throw;
        }
    }
 
    #endregion 

    private void dummyToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using (FormDummy frm = new FormDummy())
        {
            frm.ShowDialog();
        }
    }

    private void auditTrailToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        try
        {
            using (var frm = new FormSettingsAuditTrail())
            {
                frm.ShowDialog();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void activeDirectoryToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        try
        {
            using (FormActiveDirectoryGroupNew frm = new FormActiveDirectoryGroupNew())
            {
                frm.Domain = "prd.manulifeusa.com,MLIDDOMAIN1";
                frm.ModuleReference = Core.ModuleReference.Setting;
                frm.ShowDialog();
            }      
        }
        catch (Exception)
        {
            throw;
        }
    }
   
    }
}
