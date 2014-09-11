namespace BTSS.Presentation
{
    partial class FormAuditTrail
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Tables");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Users");
            this.treeViewAudit = new System.Windows.Forms.TreeView();
            this.splitContainerAudit = new System.Windows.Forms.SplitContainer();
            this.userControlListing = new BTSS.Common.UserControls.UserControlListing();
            this.splitContainerAudit.Panel1.SuspendLayout();
            this.splitContainerAudit.Panel2.SuspendLayout();
            this.splitContainerAudit.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewAudit
            // 
            this.treeViewAudit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewAudit.Location = new System.Drawing.Point(0, 0);
            this.treeViewAudit.Name = "treeViewAudit";
            treeNode1.Name = "NodeTables";
            treeNode1.Text = "Tables";
            treeNode2.Name = "NodeUsers";
            treeNode2.Text = "Users";
            this.treeViewAudit.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.treeViewAudit.Size = new System.Drawing.Size(120, 598);
            this.treeViewAudit.TabIndex = 0;
            this.treeViewAudit.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewAudit_AfterSelect);
            // 
            // splitContainerAudit
            // 
            this.splitContainerAudit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerAudit.Location = new System.Drawing.Point(0, 0);
            this.splitContainerAudit.Name = "splitContainerAudit";
            // 
            // splitContainerAudit.Panel1
            // 
            this.splitContainerAudit.Panel1.Controls.Add(this.treeViewAudit);
            // 
            // splitContainerAudit.Panel2
            // 
            this.splitContainerAudit.Panel2.Controls.Add(this.userControlListing);
            this.splitContainerAudit.Size = new System.Drawing.Size(937, 598);
            this.splitContainerAudit.SplitterDistance = 120;
            this.splitContainerAudit.TabIndex = 1;
            // 
            // userControlListing
            // 
            this.userControlListing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlListing.Location = new System.Drawing.Point(0, 0); 
            this.userControlListing.Name = "userControlListing";
            this.userControlListing.ShowRefresh = true;
            this.userControlListing.Size = new System.Drawing.Size(813, 598);
            this.userControlListing.TabIndex = 0;
            this.userControlListing.OnTextSearchChanged += new BTSS.Common.UserControls.UserControlListing.OnTextChangedEvent(this.userControlListing_OnTextSearchChanged);
            this.userControlListing.OnRefresh += new System.EventHandler(this.userControlListing_OnRefresh);
            // 
            // FormAuditTrail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 598);
            this.Controls.Add(this.splitContainerAudit);
            this.Name = "FormAuditTrail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Audit Trail";
            this.Load += new System.EventHandler(this.FormAuditTrail_Load);
            this.splitContainerAudit.Panel1.ResumeLayout(false);
            this.splitContainerAudit.Panel2.ResumeLayout(false);
            this.splitContainerAudit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewAudit;
        private System.Windows.Forms.SplitContainer splitContainerAudit;
        private BTSS.Common.UserControls.UserControlListing userControlListing;
    }
}