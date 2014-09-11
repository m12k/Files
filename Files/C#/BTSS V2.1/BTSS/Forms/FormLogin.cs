using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Biztech.Classes;

namespace Biztech
{
    public partial class FormLogin : Form
    {

        #region "Constructor"

        public FormLogin()
        {
            InitializeComponent();
        } 

        #endregion

        #region "Declarations"

        private ClassUsers classUser;

        #endregion

        #region "EventHandlers"

        private void FormLogin_Load(object sender, EventArgs e)
        {
            try
            {
                Initialize();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!classUser.IsUserValid(textBoxUserName.Text, textBoxPassword.Text))
                {
                    Global.ShowMessage("User Name or Password is invalid.\n\n Please check.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    DialogResult = DialogResult.None;
                }
                else
                {
                    var result = classUser.GetUserDetailsByUserName(textBoxUserName.Text);
                    Global.UserId = result.Id;
                    Global.UserName = textBoxUserName.Text;
                    Global.LastName = result.LastName;
                    Global.FirstName = result.FirstName;
                    Global.MiddleName = result.MiddleName;
                    Global.FullName = result.FullName;

                    Global.SetSettings("RememberMe", checkBoxRememberMe.Checked.ToString());

                    if (checkBoxRememberMe.Checked)
                        Global.SetSettings("UserName", textBoxUserName.Text);
                    else
                        Global.SetSettings("UserName", "");

                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region "Methods"

        private void Initialize()
        {
            try
            {
                classUser = new ClassUsers(Global.ConnectionString);
                 
                string value;
                value = System.Configuration.ConfigurationManager.AppSettings["RememberMe"];


                if (value != null)
                {
                    checkBoxRememberMe.Checked = Convert.ToBoolean(value);
                    textBoxUserName.Text = System.Configuration.ConfigurationManager.AppSettings["UserName"];
                    textBoxPassword.Focus();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
        
        #endregion



    }
}
