namespace BTSS.Presentation.UserControls
{
    partial class UserControlGroups
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
            this.userControlListing = new Common.UserControls.UserControlListing();
            this.userControlButtonsMain = new Common.UserControls.UserControlButtonsMain();
            this.SuspendLayout();
            // 
            // userControlListing
            // 
            this.userControlListing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlListing.Location = new System.Drawing.Point(0, 27); 
            this.userControlListing.Name = "userControlListing";
            this.userControlListing.ShowRefresh = false;
            this.userControlListing.Size = new System.Drawing.Size(868, 616);
            this.userControlListing.TabIndex = 0;
            this.userControlListing.OnRowDoubleClicked += new Common.UserControls.UserControlListing.OnMouseDoubleClickEvent(this.userControlListing_OnRowDoubleClicked);
            this.userControlListing.OnTextSearchChanged += new Common.UserControls.UserControlListing.OnTextChangedEvent(this.userControlListing_OnTextSearchChanged);
            this.userControlListing.OnSelectionChanged += new Common.UserControls.UserControlListing.OnSelectionChangedEvent(this.userControlListing_OnSelectionChanged);
            this.userControlListing.OnRefresh += new System.EventHandler(this.userControlListing_OnRefresh);
            // 
            // userControlButtonsMain
            // 
            this.userControlButtonsMain.ButtonsMode = Common.UserControls.UserControlButtonsMain.Mode.Default;
            this.userControlButtonsMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.userControlButtonsMain.Location = new System.Drawing.Point(0, 0);
            this.userControlButtonsMain.Module = Common.Core.Module.BTSSGROUP;
            this.userControlButtonsMain.Name = "userControlButtonsMain";
            this.userControlButtonsMain.Size = new System.Drawing.Size(868, 27);
            this.userControlButtonsMain.TabIndex = 1;
            this.userControlButtonsMain.OnButtonClick += new Common.UserControls.UserControlButtonsMain.OnClickEventHandler(this.userControlButtonsMain_OnButtonClick);
            // 
            // UserControlGroups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.userControlListing);
            this.Controls.Add(this.userControlButtonsMain);
            this.Name = "UserControlGroups";
            this.Size = new System.Drawing.Size(868, 643);
            this.Tag = "Groups";
            this.Load += new System.EventHandler(this.UserControlGroups_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Common.UserControls.UserControlListing userControlListing;
        private Common.UserControls.UserControlButtonsMain userControlButtonsMain;
    }
}
