namespace BTSS.Presentation
{
    partial class FormActiveDirectory
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxGroupUser = new System.Windows.Forms.ComboBox();
            this.textBoxMembersSpecifiedGroup = new System.Windows.Forms.TextBox();
            this.textBoxDomainGroupsOfUser = new System.Windows.Forms.TextBox();
            this.buttonBrowseGroup = new System.Windows.Forms.Button();
            this.ButtonMembersSpecifiedGroupGo = new System.Windows.Forms.Button();
            this.textBoxSearchGroupUser = new System.Windows.Forms.TextBox();
            this.buttonDomainGroupsOfUserGo = new System.Windows.Forms.Button();
            this.buttonSearchGroupUserGo = new System.Windows.Forms.Button();
            this.userControlDomainSearch = new BTSS.Common.UserControls.UserControlDomain();
            this.userControlDomainGroupsOfUser = new BTSS.Common.UserControls.UserControlDomain();
            this.userControlDomainMemberofGroup = new BTSS.Common.UserControls.UserControlDomain();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search for Members of specified Group";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Search for Groups of specified User";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Search for Group or User by pattern";
            // 
            // comboBoxGroupUser
            // 
            this.comboBoxGroupUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGroupUser.FormattingEnabled = true;
            this.comboBoxGroupUser.Items.AddRange(new object[] {
            "Group",
            "User"});
            this.comboBoxGroupUser.Location = new System.Drawing.Point(230, 155);
            this.comboBoxGroupUser.Name = "comboBoxGroupUser";
            this.comboBoxGroupUser.Size = new System.Drawing.Size(91, 21);
            this.comboBoxGroupUser.TabIndex = 11;
            // 
            // textBoxMembersSpecifiedGroup
            // 
            this.textBoxMembersSpecifiedGroup.Location = new System.Drawing.Point(232, 47);
            this.textBoxMembersSpecifiedGroup.Name = "textBoxMembersSpecifiedGroup";
            this.textBoxMembersSpecifiedGroup.ReadOnly = true;
            this.textBoxMembersSpecifiedGroup.Size = new System.Drawing.Size(240, 20);
            this.textBoxMembersSpecifiedGroup.TabIndex = 2;
            this.textBoxMembersSpecifiedGroup.TextChanged += new System.EventHandler(this.textBoxMembersSpecifiedGroup_TextChanged);
            // 
            // textBoxDomainGroupsOfUser
            // 
            this.textBoxDomainGroupsOfUser.Location = new System.Drawing.Point(232, 101);
            this.textBoxDomainGroupsOfUser.Name = "textBoxDomainGroupsOfUser";
            this.textBoxDomainGroupsOfUser.Size = new System.Drawing.Size(240, 20);
            this.textBoxDomainGroupsOfUser.TabIndex = 7;
            this.textBoxDomainGroupsOfUser.TextChanged += new System.EventHandler(this.textBoxDomainGroupsOfUser_TextChanged);
            // 
            // buttonBrowseGroup
            // 
            this.buttonBrowseGroup.Location = new System.Drawing.Point(478, 44);
            this.buttonBrowseGroup.Name = "buttonBrowseGroup";
            this.buttonBrowseGroup.Size = new System.Drawing.Size(34, 23);
            this.buttonBrowseGroup.TabIndex = 3;
            this.buttonBrowseGroup.Text = "--";
            this.buttonBrowseGroup.UseVisualStyleBackColor = true;
            this.buttonBrowseGroup.Click += new System.EventHandler(this.buttonBrowseGroup_Click);
            // 
            // ButtonMembersSpecifiedGroupGo
            // 
            this.ButtonMembersSpecifiedGroupGo.Enabled = false;
            this.ButtonMembersSpecifiedGroupGo.Location = new System.Drawing.Point(518, 44);
            this.ButtonMembersSpecifiedGroupGo.Name = "ButtonMembersSpecifiedGroupGo";
            this.ButtonMembersSpecifiedGroupGo.Size = new System.Drawing.Size(34, 23);
            this.ButtonMembersSpecifiedGroupGo.TabIndex = 4;
            this.ButtonMembersSpecifiedGroupGo.Text = "GO";
            this.ButtonMembersSpecifiedGroupGo.UseVisualStyleBackColor = true;
            this.ButtonMembersSpecifiedGroupGo.Click += new System.EventHandler(this.ButtonMembersSpecifiedGroupGo_Click);
            // 
            // textBoxSearchGroupUser
            // 
            this.textBoxSearchGroupUser.Location = new System.Drawing.Point(327, 155);
            this.textBoxSearchGroupUser.Name = "textBoxSearchGroupUser";
            this.textBoxSearchGroupUser.Size = new System.Drawing.Size(143, 20);
            this.textBoxSearchGroupUser.TabIndex = 12;
            this.textBoxSearchGroupUser.TextChanged += new System.EventHandler(this.textBoxSearchGroupUser_TextChanged);
            // 
            // buttonDomainGroupsOfUserGo
            // 
            this.buttonDomainGroupsOfUserGo.Enabled = false;
            this.buttonDomainGroupsOfUserGo.Location = new System.Drawing.Point(478, 99);
            this.buttonDomainGroupsOfUserGo.Name = "buttonDomainGroupsOfUserGo";
            this.buttonDomainGroupsOfUserGo.Size = new System.Drawing.Size(34, 23);
            this.buttonDomainGroupsOfUserGo.TabIndex = 8;
            this.buttonDomainGroupsOfUserGo.Text = "GO";
            this.buttonDomainGroupsOfUserGo.UseVisualStyleBackColor = true;
            this.buttonDomainGroupsOfUserGo.Click += new System.EventHandler(this.buttonDomainGroupsOfUserGo_Click);
            // 
            // buttonSearchGroupUserGo
            // 
            this.buttonSearchGroupUserGo.Enabled = false;
            this.buttonSearchGroupUserGo.Location = new System.Drawing.Point(476, 153);
            this.buttonSearchGroupUserGo.Name = "buttonSearchGroupUserGo";
            this.buttonSearchGroupUserGo.Size = new System.Drawing.Size(34, 23);
            this.buttonSearchGroupUserGo.TabIndex = 13;
            this.buttonSearchGroupUserGo.Text = "GO";
            this.buttonSearchGroupUserGo.UseVisualStyleBackColor = true;
            this.buttonSearchGroupUserGo.Click += new System.EventHandler(this.buttonSearchGroupUserGo_Click);
            // 
            // userControlDomainSearch
            // 
            this.userControlDomainSearch.Location = new System.Drawing.Point(21, 155);
            this.userControlDomainSearch.Name = "userControlDomainSearch";
            this.userControlDomainSearch.Size = new System.Drawing.Size(205, 24);
            this.userControlDomainSearch.TabIndex = 10;
            // 
            // userControlDomainGroupsOfUser
            // 
            this.userControlDomainGroupsOfUser.Location = new System.Drawing.Point(21, 101);
            this.userControlDomainGroupsOfUser.Name = "userControlDomainGroupsOfUser";
            this.userControlDomainGroupsOfUser.Size = new System.Drawing.Size(205, 24);
            this.userControlDomainGroupsOfUser.TabIndex = 6;
            // 
            // userControlDomainMemberofGroup
            // 
            this.userControlDomainMemberofGroup.Location = new System.Drawing.Point(21, 47);
            this.userControlDomainMemberofGroup.Name = "userControlDomainMemberofGroup";
            this.userControlDomainMemberofGroup.Size = new System.Drawing.Size(205, 24);
            this.userControlDomainMemberofGroup.TabIndex = 1;
            this.userControlDomainMemberofGroup.OnSelectedIndexChanged += new System.EventHandler(this.userControlDomainMemberofGroup_OnSelectedIndexChanged);
            // 
            // FormActiveDirectory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 215);
            this.Controls.Add(this.buttonSearchGroupUserGo);
            this.Controls.Add(this.buttonDomainGroupsOfUserGo);
            this.Controls.Add(this.ButtonMembersSpecifiedGroupGo);
            this.Controls.Add(this.buttonBrowseGroup);
            this.Controls.Add(this.textBoxSearchGroupUser);
            this.Controls.Add(this.textBoxDomainGroupsOfUser);
            this.Controls.Add(this.textBoxMembersSpecifiedGroup);
            this.Controls.Add(this.comboBoxGroupUser);
            this.Controls.Add(this.userControlDomainSearch);
            this.Controls.Add(this.userControlDomainGroupsOfUser);
            this.Controls.Add(this.userControlDomainMemberofGroup);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormActiveDirectory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Active Directory";
            this.Load += new System.EventHandler(this.FormActiveDirectory_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private BTSS.Common.UserControls.UserControlDomain userControlDomainMemberofGroup;
        private BTSS.Common.UserControls.UserControlDomain userControlDomainGroupsOfUser;
        private BTSS.Common.UserControls.UserControlDomain userControlDomainSearch;
        private System.Windows.Forms.ComboBox comboBoxGroupUser;
        private System.Windows.Forms.TextBox textBoxMembersSpecifiedGroup;
        private System.Windows.Forms.TextBox textBoxDomainGroupsOfUser;
        private System.Windows.Forms.Button buttonBrowseGroup;
        private System.Windows.Forms.Button ButtonMembersSpecifiedGroupGo;
        private System.Windows.Forms.TextBox textBoxSearchGroupUser;
        private System.Windows.Forms.Button buttonDomainGroupsOfUserGo;
        private System.Windows.Forms.Button buttonSearchGroupUserGo;
    }
}