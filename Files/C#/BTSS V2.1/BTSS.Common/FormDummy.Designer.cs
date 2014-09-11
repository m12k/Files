namespace BTSS.Common
{
    partial class FormDummy
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
            this.userControlDomain1 = new BTSS.Common.UserControls.UserControlDomain();
            this.SuspendLayout();
            // 
            // userControlDomain1
            // 
            this.userControlDomain1.Location = new System.Drawing.Point(77, 73);
            this.userControlDomain1.Name = "userControlDomain1";
            this.userControlDomain1.Size = new System.Drawing.Size(320, 24);
            this.userControlDomain1.TabIndex = 0;
            // 
            // FormDummy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 347);
            this.Controls.Add(this.userControlDomain1);
            this.Name = "FormDummy";
            this.Text = "FormDummy";
            this.ResumeLayout(false);

        }

        #endregion

        private BTSS.Common.UserControls.UserControlDomain userControlDomain1;
    }
}