namespace BTSS.Presentation.UserControls
{
    partial class UserControlProjects 
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.userControlListing = new BTSS.Common.UserControls.UserControlListing();
            this.userControlButtonsMain = new BTSS.Common.UserControls.UserControlButtonsMain();
            this.SuspendLayout();
            // 
            // userControlListing
            // 
            this.userControlListing.BackColor = System.Drawing.SystemColors.Control;
            this.userControlListing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlListing.Location = new System.Drawing.Point(0, 27); 
            this.userControlListing.Name = "userControlListing";
            this.userControlListing.ShowRefresh = false;
            this.userControlListing.Size = new System.Drawing.Size(914, 686);
            this.userControlListing.TabIndex = 0;
            this.userControlListing.OnRowDoubleClicked += new BTSS.Common.UserControls.UserControlListing.OnMouseDoubleClickEvent(this.userControlListing_OnRowDoubleClicked);
            this.userControlListing.OnTextSearchChanged += new BTSS.Common.UserControls.UserControlListing.OnTextChangedEvent(this.userControlListing_OnTextSearchChanged);
            this.userControlListing.OnSelectionChanged += new BTSS.Common.UserControls.UserControlListing.OnSelectionChangedEvent(this.userControlListing_OnSelectionChanged);
            this.userControlListing.OnRefresh += new System.EventHandler(this.userControlListing_OnRefresh);
            // 
            // userControlButtonsMain
            // 
            this.userControlButtonsMain.ButtonsMode = BTSS.Common.UserControls.UserControlButtonsMain.Mode.Default;
            this.userControlButtonsMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.userControlButtonsMain.ForeColor = System.Drawing.Color.DarkBlue;
            this.userControlButtonsMain.Location = new System.Drawing.Point(0, 0);
            this.userControlButtonsMain.Module = Common.Core.Module.BTSSPROJECTS;
            this.userControlButtonsMain.Name = "userControlButtonsMain";
            this.userControlButtonsMain.Size = new System.Drawing.Size(914, 27);
            this.userControlButtonsMain.TabIndex = 1;
            this.userControlButtonsMain.OnButtonClick += new BTSS.Common.UserControls.UserControlButtonsMain.OnClickEventHandler(this.userControlButtonsMain_OnButtonClick);
            // 
            // UserControlProjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.userControlListing);
            this.Controls.Add(this.userControlButtonsMain);
            this.Name = "UserControlProjects";
            this.Size = new System.Drawing.Size(914, 713);
            this.Tag = "Projects";
            this.Load += new System.EventHandler(this.UserControlProject_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private BTSS.Common.UserControls.UserControlListing userControlListing;
        private BTSS.Common.UserControls.UserControlButtonsMain userControlButtonsMain;
    }
}
