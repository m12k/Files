namespace BTSS.Presentation
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Name:");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Description:");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Database Details", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.auditTrailToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.activeDirectoryToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dummyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.activeDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.auditTrailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupOfUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.domainOfUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectAuditTrailToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.comboBoxProject = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.treeProject = new System.Windows.Forms.TreeView();
            this.panelHead = new System.Windows.Forms.Panel();
            this.labelConnected = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonRefreshProject = new System.Windows.Forms.Button();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelUserName = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelUpdateAvailable = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTipMain = new System.Windows.Forms.ToolTip(this.components);
            this.panelLeft = new System.Windows.Forms.Panel();
            this.buttonModule = new System.Windows.Forms.Button();
            this.buttonGroup = new System.Windows.Forms.Button();
            this.buttonUser = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelRight = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.panelHead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStripMain.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.setupToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.reportsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1000, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.dummyToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.auditTrailToolStripMenuItem1,
            this.activeDirectoryToolStripMenuItem1});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // auditTrailToolStripMenuItem1
            // 
            this.auditTrailToolStripMenuItem1.Name = "auditTrailToolStripMenuItem1";
            this.auditTrailToolStripMenuItem1.Size = new System.Drawing.Size(162, 22);
            this.auditTrailToolStripMenuItem1.Text = "Audit Trail";
            this.auditTrailToolStripMenuItem1.Click += new System.EventHandler(this.auditTrailToolStripMenuItem1_Click);
            // 
            // activeDirectoryToolStripMenuItem1
            // 
            this.activeDirectoryToolStripMenuItem1.Name = "activeDirectoryToolStripMenuItem1";
            this.activeDirectoryToolStripMenuItem1.Size = new System.Drawing.Size(162, 22);
            this.activeDirectoryToolStripMenuItem1.Text = "Active Directory";
            this.activeDirectoryToolStripMenuItem1.Click += new System.EventHandler(this.activeDirectoryToolStripMenuItem1_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // dummyToolStripMenuItem
            // 
            this.dummyToolStripMenuItem.Name = "dummyToolStripMenuItem";
            this.dummyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dummyToolStripMenuItem.Text = "Dummy";
            this.dummyToolStripMenuItem.Click += new System.EventHandler(this.dummyToolStripMenuItem_Click);
            // 
            // setupToolStripMenuItem
            // 
            this.setupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectsToolStripMenuItem,
            this.groupsToolStripMenuItem,
            this.usersToolStripMenuItem});
            this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
            this.setupToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.setupToolStripMenuItem.Text = "Setup";
            // 
            // projectsToolStripMenuItem
            // 
            this.projectsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("projectsToolStripMenuItem.Image")));
            this.projectsToolStripMenuItem.Name = "projectsToolStripMenuItem";
            this.projectsToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.projectsToolStripMenuItem.Tag = "Setup -> Projects";
            this.projectsToolStripMenuItem.Text = "Projects";
            this.projectsToolStripMenuItem.Click += new System.EventHandler(this.projectsToolStripMenuItem_Click);
            // 
            // groupsToolStripMenuItem
            // 
            this.groupsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("groupsToolStripMenuItem.Image")));
            this.groupsToolStripMenuItem.Name = "groupsToolStripMenuItem";
            this.groupsToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.groupsToolStripMenuItem.Tag = "Setup -> Groups";
            this.groupsToolStripMenuItem.Text = "Groups";
            this.groupsToolStripMenuItem.Click += new System.EventHandler(this.groupsToolStripMenuItem_Click);
            // 
            // usersToolStripMenuItem
            // 
            this.usersToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("usersToolStripMenuItem.Image")));
            this.usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            this.usersToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.usersToolStripMenuItem.Tag = "Setup -> Users";
            this.usersToolStripMenuItem.Text = "Users";
            this.usersToolStripMenuItem.Click += new System.EventHandler(this.usersToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activeDirectoryToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // activeDirectoryToolStripMenuItem
            // 
            this.activeDirectoryToolStripMenuItem.Name = "activeDirectoryToolStripMenuItem";
            this.activeDirectoryToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.activeDirectoryToolStripMenuItem.Text = "Active Directory";
            this.activeDirectoryToolStripMenuItem.Click += new System.EventHandler(this.ActiveDirectoryUsersListToolStripMenuItem_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectListToolStripMenuItem,
            this.auditTrailToolStripMenuItem,
            this.projectsToolStripMenuItem1});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.reportsToolStripMenuItem.Text = "Reports";
            // 
            // projectListToolStripMenuItem
            // 
            this.projectListToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("projectListToolStripMenuItem.Image")));
            this.projectListToolStripMenuItem.Name = "projectListToolStripMenuItem";
            this.projectListToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.projectListToolStripMenuItem.Text = "Project List";
            this.projectListToolStripMenuItem.Click += new System.EventHandler(this.projectListToolStripMenuItem_Click);
            // 
            // auditTrailToolStripMenuItem
            // 
            this.auditTrailToolStripMenuItem.Name = "auditTrailToolStripMenuItem";
            this.auditTrailToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.auditTrailToolStripMenuItem.Text = "Audit Trail";
            this.auditTrailToolStripMenuItem.Click += new System.EventHandler(this.auditTrailToolStripMenuItem_Click);
            // 
            // projectsToolStripMenuItem1
            // 
            this.projectsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.groupOfUsersToolStripMenuItem,
            this.domainOfUsersToolStripMenuItem,
            this.projectAuditTrailToolStripMenuItem1});
            this.projectsToolStripMenuItem1.Name = "projectsToolStripMenuItem1";
            this.projectsToolStripMenuItem1.Size = new System.Drawing.Size(138, 22);
            this.projectsToolStripMenuItem1.Text = "Projects";
            // 
            // groupOfUsersToolStripMenuItem
            // 
            this.groupOfUsersToolStripMenuItem.Name = "groupOfUsersToolStripMenuItem";
            this.groupOfUsersToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.groupOfUsersToolStripMenuItem.Text = "Group of Users";
            this.groupOfUsersToolStripMenuItem.Click += new System.EventHandler(this.groupOfUsersToolStripMenuItem_Click);
            // 
            // domainOfUsersToolStripMenuItem
            // 
            this.domainOfUsersToolStripMenuItem.Name = "domainOfUsersToolStripMenuItem";
            this.domainOfUsersToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.domainOfUsersToolStripMenuItem.Text = "Domain of Users";
            this.domainOfUsersToolStripMenuItem.Click += new System.EventHandler(this.domainOfUsersToolStripMenuItem_Click);
            // 
            // projectAuditTrailToolStripMenuItem1
            // 
            this.projectAuditTrailToolStripMenuItem1.Name = "projectAuditTrailToolStripMenuItem1";
            this.projectAuditTrailToolStripMenuItem1.Size = new System.Drawing.Size(163, 22);
            this.projectAuditTrailToolStripMenuItem1.Text = "Audit Trail";
            this.projectAuditTrailToolStripMenuItem1.Click += new System.EventHandler(this.projectAuditTrailToolStripMenuItem1_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getUpdateToolStripMenuItem,
            this.toolStripMenuItem1});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // getUpdateToolStripMenuItem
            // 
            this.getUpdateToolStripMenuItem.Enabled = false;
            this.getUpdateToolStripMenuItem.Name = "getUpdateToolStripMenuItem";
            this.getUpdateToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.getUpdateToolStripMenuItem.Text = "Get Update";
            this.getUpdateToolStripMenuItem.Click += new System.EventHandler(this.getUpdateToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.toolStripMenuItem1.Text = "About";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(882, 563);
            this.tabControlMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlMain.TabIndex = 0;
            // 
            // comboBoxProject
            // 
            this.comboBoxProject.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBoxProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxProject.FormattingEnabled = true;
            this.comboBoxProject.Location = new System.Drawing.Point(17, 28);
            this.comboBoxProject.Name = "comboBoxProject";
            this.comboBoxProject.Size = new System.Drawing.Size(245, 21);
            this.comboBoxProject.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Connect to Database";
            // 
            // buttonConnect
            // 
            this.buttonConnect.BackColor = System.Drawing.SystemColors.Control;
            this.buttonConnect.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.buttonConnect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonConnect.Location = new System.Drawing.Point(290, 28);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 21);
            this.buttonConnect.TabIndex = 3;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = false;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // treeProject
            // 
            this.treeProject.BackColor = System.Drawing.SystemColors.Control;
            this.treeProject.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeProject.FullRowSelect = true;
            this.treeProject.Location = new System.Drawing.Point(11, 53);
            this.treeProject.Name = "treeProject";
            treeNode1.Name = "NodeName";
            treeNode1.Text = "Name:";
            treeNode2.Name = "NodeDescription";
            treeNode2.Text = "Description:";
            treeNode3.Name = "NodeHeader";
            treeNode3.Text = "Database Details";
            this.treeProject.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.treeProject.Size = new System.Drawing.Size(829, 120);
            this.treeProject.TabIndex = 4;
            this.treeProject.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            this.treeProject.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeCollapse);
            // 
            // panelHead
            // 
            this.panelHead.BackColor = System.Drawing.SystemColors.Control;
            this.panelHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHead.Controls.Add(this.labelConnected);
            this.panelHead.Controls.Add(this.pictureBox1);
            this.panelHead.Controls.Add(this.buttonRefreshProject);
            this.panelHead.Controls.Add(this.buttonConnect);
            this.panelHead.Controls.Add(this.treeProject);
            this.panelHead.Controls.Add(this.comboBoxProject);
            this.panelHead.Controls.Add(this.label3);
            this.panelHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHead.Location = new System.Drawing.Point(0, 24);
            this.panelHead.Name = "panelHead";
            this.panelHead.Size = new System.Drawing.Size(1000, 81);
            this.panelHead.TabIndex = 1;
            // 
            // labelConnected
            // 
            this.labelConnected.AutoSize = true;
            this.labelConnected.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConnected.ForeColor = System.Drawing.Color.Blue;
            this.labelConnected.Location = new System.Drawing.Point(371, 29);
            this.labelConnected.Name = "labelConnected";
            this.labelConnected.Size = new System.Drawing.Size(82, 16);
            this.labelConnected.TabIndex = 9;
            this.labelConnected.Text = "Connected!";
            this.labelConnected.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(16, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 19);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // buttonRefreshProject
            // 
            this.buttonRefreshProject.BackColor = System.Drawing.SystemColors.Control;
            this.buttonRefreshProject.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.buttonRefreshProject.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonRefreshProject.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefreshProject.Image")));
            this.buttonRefreshProject.Location = new System.Drawing.Point(263, 28);
            this.buttonRefreshProject.Name = "buttonRefreshProject";
            this.buttonRefreshProject.Size = new System.Drawing.Size(23, 21);
            this.buttonRefreshProject.TabIndex = 2;
            this.buttonRefreshProject.UseVisualStyleBackColor = false;
            this.buttonRefreshProject.Click += new System.EventHandler(this.buttonRefreshProject_Click);
            // 
            // statusStripMain
            // 
            this.statusStripMain.BackColor = System.Drawing.SystemColors.Control;
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelUserName,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabelUpdateAvailable});
            this.statusStripMain.Location = new System.Drawing.Point(0, 672);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(1000, 22);
            this.statusStripMain.TabIndex = 2;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // toolStripStatusLabelUserName
            // 
            this.toolStripStatusLabelUserName.Name = "toolStripStatusLabelUserName";
            this.toolStripStatusLabelUserName.Size = new System.Drawing.Size(33, 17);
            this.toolStripStatusLabelUserName.Text = "User:";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(952, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // toolStripStatusLabelUpdateAvailable
            // 
            this.toolStripStatusLabelUpdateAvailable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.toolStripStatusLabelUpdateAvailable.Name = "toolStripStatusLabelUpdateAvailable";
            this.toolStripStatusLabelUpdateAvailable.Size = new System.Drawing.Size(0, 17);
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelLeft.Controls.Add(this.buttonModule);
            this.panelLeft.Controls.Add(this.buttonGroup);
            this.panelLeft.Controls.Add(this.buttonUser);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(114, 567);
            this.panelLeft.TabIndex = 0;
            // 
            // buttonModule
            // 
            this.buttonModule.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonModule.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonModule.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonModule.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonModule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonModule.Image = ((System.Drawing.Image)(resources.GetObject("buttonModule.Image")));
            this.buttonModule.Location = new System.Drawing.Point(16, 22);
            this.buttonModule.Name = "buttonModule";
            this.buttonModule.Size = new System.Drawing.Size(81, 75);
            this.buttonModule.TabIndex = 0;
            this.buttonModule.Tag = "Manage -> Module";
            this.buttonModule.Text = "Module";
            this.buttonModule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonModule.UseVisualStyleBackColor = false;
            this.buttonModule.Click += new System.EventHandler(this.moduleToolStripMenuItem_Click);
            // 
            // buttonGroup
            // 
            this.buttonGroup.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonGroup.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonGroup.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonGroup.Image = ((System.Drawing.Image)(resources.GetObject("buttonGroup.Image")));
            this.buttonGroup.Location = new System.Drawing.Point(15, 118);
            this.buttonGroup.Name = "buttonGroup";
            this.buttonGroup.Size = new System.Drawing.Size(81, 75);
            this.buttonGroup.TabIndex = 1;
            this.buttonGroup.Tag = "Manage -> Group";
            this.buttonGroup.Text = "Group";
            this.buttonGroup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonGroup.UseVisualStyleBackColor = false;
            this.buttonGroup.Click += new System.EventHandler(this.groupToolStripMenuItem_Click);
            // 
            // buttonUser
            // 
            this.buttonUser.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonUser.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonUser.Image = ((System.Drawing.Image)(resources.GetObject("buttonUser.Image")));
            this.buttonUser.Location = new System.Drawing.Point(15, 209);
            this.buttonUser.Name = "buttonUser";
            this.buttonUser.Size = new System.Drawing.Size(81, 75);
            this.buttonUser.TabIndex = 2;
            this.buttonUser.Tag = "Manage -> User";
            this.buttonUser.Text = "User";
            this.buttonUser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonUser.UseVisualStyleBackColor = false;
            this.buttonUser.Click += new System.EventHandler(this.userToolStripMenuItem_Click);
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.Gainsboro;
            this.panelMain.Controls.Add(this.panelRight);
            this.panelMain.Controls.Add(this.panelLeft);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 105);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1000, 567);
            this.panelMain.TabIndex = 10;
            // 
            // panelRight
            // 
            this.panelRight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelRight.Controls.Add(this.tabControlMain);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(114, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(886, 567);
            this.panelRight.TabIndex = 1;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1000, 694);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.panelHead);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Business Technology Security System(BTSS)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormMain2_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelHead.ResumeLayout(false);
            this.panelHead.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem; 
        private System.Windows.Forms.Button buttonUser;
        private System.Windows.Forms.Button buttonGroup;
        private System.Windows.Forms.Button buttonModule;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxProject;
        private System.Windows.Forms.TreeView treeProject;
        private System.Windows.Forms.Panel panelHead;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.ToolStripMenuItem setupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem groupsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelUserName;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelUpdateAvailable;
        private System.Windows.Forms.Button buttonRefreshProject;
        private System.Windows.Forms.ToolTip toolTipMain;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem auditTrailToolStripMenuItem;
        private System.Windows.Forms.Label labelConnected;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem activeDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem groupOfUsersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem domainOfUsersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dummyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectAuditTrailToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem auditTrailToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem activeDirectoryToolStripMenuItem1;
    }
}