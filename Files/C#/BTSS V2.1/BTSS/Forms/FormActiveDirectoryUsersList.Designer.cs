namespace Biztech
{
    partial class FormActiveDirectoryUsersList
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
            this.treeViewGroup = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonRemoveGroup = new System.Windows.Forms.Button();
            this.buttonModify = new System.Windows.Forms.Button();
            this.buttonAddGroup = new System.Windows.Forms.Button();
            this.userControlListing = new Biztech.UserControls.Generic.UserControlListing();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewGroup
            // 
            this.treeViewGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewGroup.Location = new System.Drawing.Point(0, 0);
            this.treeViewGroup.Name = "treeViewGroup";
            this.treeViewGroup.Size = new System.Drawing.Size(288, 539);
            this.treeViewGroup.TabIndex = 1;
            this.treeViewGroup.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewGroup_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.treeViewGroup);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(288, 586);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonRemoveGroup);
            this.panel2.Controls.Add(this.buttonModify);
            this.panel2.Controls.Add(this.buttonAddGroup);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 539);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(288, 47);
            this.panel2.TabIndex = 3;
            // 
            // buttonRemoveGroup
            // 
            this.buttonRemoveGroup.Location = new System.Drawing.Point(185, 12);
            this.buttonRemoveGroup.Name = "buttonRemoveGroup";
            this.buttonRemoveGroup.Size = new System.Drawing.Size(88, 23);
            this.buttonRemoveGroup.TabIndex = 3;
            this.buttonRemoveGroup.Text = "Remove Group";
            this.buttonRemoveGroup.UseVisualStyleBackColor = true;
            this.buttonRemoveGroup.Click += new System.EventHandler(this.buttonRemoveGroup_Click);
            // 
            // buttonModify
            // 
            this.buttonModify.Location = new System.Drawing.Point(100, 12);
            this.buttonModify.Name = "buttonModify";
            this.buttonModify.Size = new System.Drawing.Size(80, 23);
            this.buttonModify.TabIndex = 3;
            this.buttonModify.Text = "Modify Group";
            this.buttonModify.UseVisualStyleBackColor = true;
            this.buttonModify.Click += new System.EventHandler(this.buttonModify_Click);
            // 
            // buttonAddGroup
            // 
            this.buttonAddGroup.Location = new System.Drawing.Point(19, 12);
            this.buttonAddGroup.Name = "buttonAddGroup";
            this.buttonAddGroup.Size = new System.Drawing.Size(75, 23);
            this.buttonAddGroup.TabIndex = 3;
            this.buttonAddGroup.Text = "Add Group";
            this.buttonAddGroup.UseVisualStyleBackColor = true;
            this.buttonAddGroup.Click += new System.EventHandler(this.buttonAddGroup_Click);
            // 
            // userControlListing
            // 
            this.userControlListing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlListing.Location = new System.Drawing.Point(288, 0);
            this.userControlListing.Module = Biztech.Global.Module.NONE;
            this.userControlListing.Name = "userControlListing";
            this.userControlListing.ShowRefresh = false;
            this.userControlListing.Size = new System.Drawing.Size(532, 586);
            this.userControlListing.TabIndex = 3;
            this.userControlListing.OnTextSearchChanged += new Biztech.UserControls.Generic.UserControlListing.OnTextChangedEvent(this.userControlListing_OnTextSearchChanged);
            // 
            // FormActiveDirectoryUsersList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 586);
            this.Controls.Add(this.userControlListing);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormActiveDirectoryUsersList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Active Directory Users List";
            this.Load += new System.EventHandler(this.FormActiveDirectoryUsersList_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewGroup;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonRemoveGroup;
        private System.Windows.Forms.Button buttonAddGroup;
        private System.Windows.Forms.Button buttonModify;
        private Biztech.UserControls.Generic.UserControlListing userControlListing;
    }
}