namespace BTSS.Presentation.UserControls.Projects
{
    partial class UserControlModule
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
            this.userControlListing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlListing.Location = new System.Drawing.Point(0, 27); 
            this.userControlListing.Name = "userControlListing";
            this.userControlListing.ShowRefresh = false;
            this.userControlListing.Size = new System.Drawing.Size(930, 601);
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
            this.userControlButtonsMain.Location = new System.Drawing.Point(0, 0);
            this.userControlButtonsMain.Module = BTSS.Common.Core.Module.MODULE;
            this.userControlButtonsMain.Name = "userControlButtonsMain";
            this.userControlButtonsMain.Size = new System.Drawing.Size(930, 27);
            this.userControlButtonsMain.TabIndex = 1;
            this.userControlButtonsMain.OnButtonClick += new BTSS.Common.UserControls.UserControlButtonsMain.OnClickEventHandler(this.userControlButtonsMain_OnButtonClick);
            // 
            // UserControlModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.userControlListing);
            this.Controls.Add(this.userControlButtonsMain);
            this.Name = "UserControlModule";
            this.Size = new System.Drawing.Size(930, 628);
            this.Tag = "Module";
            this.Load += new System.EventHandler(this.UserControlUsers_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private BTSS.Common.UserControls.UserControlListing userControlListing;
        private BTSS.Common.UserControls.UserControlButtonsMain userControlButtonsMain;
    }
}
