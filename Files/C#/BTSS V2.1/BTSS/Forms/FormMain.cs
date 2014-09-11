using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Biztech.Classes;
using System.Deployment.Application;

namespace Biztech
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

    private ClassCheckConnection classCheckConnection;
    private ClassUsers classUser; 
    private Validation Validation;
    private Global.ConnectionStatus connectionStatus = Global.ConnectionStatus.Disconnected;
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

    private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        try
        {
            using (var frm = new FormPreferences())
            {
                frm.ShowDialog();
            }
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
            if (Global.ProjectDataProvider == Global.DataProvider.OLEDB)
                panelHead.Size = new Size(panelHead.Width, 130);
            else if (Global.ProjectDataProvider == Global.DataProvider.SQL)
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
            Show(new UserControls.BTSS.UserControlProjects(), projectsToolStripMenuItem.Tag.ToString());
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
            Show(new UserControls.BTSS.UserControlGroups(), groupsToolStripMenuItem.Tag.ToString());
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
                Show(new UserControls.BTSS.UserControlUsers(), usersToolStripMenuItem.Tag.ToString());
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
                    connectionString = Global.GetConnectionString(result.File, result.UserID, result.Password, result.HasOtherDetails.Value, result.MDW);
                    classCheckConnection = new ClassCheckConnection(connectionString, Global.DataProvider.OLEDB);
                }
                else if (result.Provider == "SQL")
                {
                    connectionString = Global.GetConnectionString(result.Datasource, result.DatabaseName, result.UserID, result.Password);
                    classCheckConnection = new ClassCheckConnection(connectionString, Global.DataProvider.SQL);
                }

                if (!classCheckConnection.CheckConnection())
                {
                    Global.ShowMessage("Connection Failed.\n\nPlease check.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

                    Global.ProjectDataProvider = Global.DataProvider.OLEDB;
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

                    Global.ProjectDataProvider = Global.DataProvider.SQL;
                }


                Global.ProjectConnectionString = connectionString;
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
            Show(new UserControls.Individual.UserControlUser(), buttonUser.Tag.ToString());
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
            Show(new UserControls.Individual.UserControlModule(), buttonModule.Tag.ToString());
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
            Show(new UserControls.Individual.UserControlGroup(), buttonGroup.Tag.ToString());
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
            ClassProject classProject = new ClassProject(Global.ConnectionString);

            DataTable dt = new DataTable();
            dt = classProject.GetProjectListForReport();

            using (var classReportViewer = new ClassReportViewer())
            {
                classReportViewer.ReportTitle = "Project List";
                classReportViewer.XMLName = "ProjectList";
                classReportViewer.ReportSource = (new Biztech.Reports.ReportProjectList());
                classReportViewer.SetDatasource(dt);
                classReportViewer.ShowReport();
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
            using (FormActiveDirectoryUsersList frm = new FormActiveDirectoryUsersList())
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
            classUser = new ClassUsers(Global.ConnectionString);
            Validation = new Validation();
            timerConnected = new Timer();
            timerConnected.Tick += new EventHandler(TimerConnected_Ticks);
            
            Validation.Validate(this);
            toolStripStatusLabelUserName.Text = "User: " + Global.FullName;
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

            UserControls.UserControlMain userControlMain = new UserControls.UserControlMain();

            userControlMain.OnCloseControl += new UserControls.UserControlMain.OnCloseControlEvent(ControlMainClose);

            userControlMain.ModuleDirectory = moduleDirectory;
            userControlMain.Dock = DockStyle.Fill;
            userControlMain.Control = uc;
            
             
            panelRight.Controls.Add(userControlMain);
            //splitContainer1.Panel2.Controls.Add(userControlMain);

            TabPage tabPage = new TabPage();
            tabPage.Text = uc.Tag.ToString();
            tabPage.Tag = uc.Name;
            tabPage.Font = new Font("Microsoft Sans Serif",(float)8.25, FontStyle.Regular);
            userControlMain.Tab = tabPage;
            tabPage.Controls.Add(userControlMain);
            tabControlMain.TabPages.Add(tabPage);
  
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
            ClassUsers classUser = new ClassUsers(Global.ConnectionString);

            var result = from s in classUser.GetUserProjects(Global.UserId)
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
            if (connectionStatus == Global.ConnectionStatus.Disconnected)
            {
                connectionStatus = Global.ConnectionStatus.Connected;
                buttonConnect.Text = "Disconnect";
                comboBoxProject.Enabled = false;

                ticksCount = 0;
                timerConnected.Start();  
            }
            else if (connectionStatus == Global.ConnectionStatus.Connected)
            {
                connectionStatus = Global.ConnectionStatus.Disconnected;
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
            if (connectionStatus == Global.ConnectionStatus.Disconnected)
            {
                buttonModule.Enabled = false;
                buttonGroup.Enabled = false;
                buttonUser.Enabled = false;
                 
                buttonRefreshProject.Enabled = true;
            }
            else if (connectionStatus == Global.ConnectionStatus.Connected)
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

            Global.Module mod = Global.Module.BTSSPROJECTS;
            
            //BTSS Module checking
            while (counter <=4)
            { 
                if (counter == 1)                        
                    mod = Global.Module.BTSSPROJECTS;
                else if (counter == 2)
                    mod = Global.Module.BTSSGROUP;
                else if (counter == 3)
                    mod = Global.Module.BTSSUSERS;
                else if (counter == 4)
                    mod = Global.Module.PREFERENCES;
 
                var result = classUser.GetAccessRights(Global.UserId,  mod);
 
                if (counter == 1 && !result.CanView.Value)
                    projectsToolStripMenuItem.Visible = false;
                if (counter == 2 && !result.CanView.Value)
                    groupsToolStripMenuItem.Visible = false;
                if (counter == 3 && !result.CanView.Value)
                    usersToolStripMenuItem.Visible = false;
                if (counter == 4 && !result.CanView.Value)
                    preferencesToolStripMenuItem.Visible = false;
 
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
                    mod = Global.Module.MODULE;
                else if (counter == 2)
                    mod = Global.Module.GROUP;
                else if (counter == 3)
                    mod = Global.Module.USER;

                var result = classUser.GetAccessRights(Global.UserId, mod);

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
    
    }
}
