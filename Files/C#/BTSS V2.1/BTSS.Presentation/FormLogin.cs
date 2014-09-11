using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms; 

namespace BTSS.Presentation
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

        private Logic.User user;
        private string windowsUserName;

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
                if (!user.IsUserValid(textBoxUserName.Text, textBoxPassword.Text, Common.Core.UserAuthentication.ToString()))
                {
                    if (Common.Core.UserAuthentication == BTSS.Common.Core.Authentication.Application)
                    {
                        Common.Core.ShowMessage("User Name or Password is invalid.\n\nPlease check.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        DialogResult = DialogResult.None;
                    }
                    else
                    {
                        Common.Core.ShowMessage("You are not authorize to use this application.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
                        DialogResult = DialogResult.Cancel ;
                    }
                }
                else
                {
                    var result = user.GetUserDetailsByUserName(textBoxUserName.Text);
                    Common.Core.UserId = result.Id;
                    Common.Core.UserName = textBoxUserName.Text;
                    Common.Core.LastName = result.LastName;
                    Common.Core.FirstName = result.FirstName;
                    Common.Core.MiddleName = result.MiddleName;
                    Common.Core.FullName = result.FullName;
                    
                    if (Common.Core.UserAuthentication == BTSS.Common.Core.Authentication.Application)
                    {                        
                        if (checkBoxRememberMe.Checked)
                            Common.Core.SetSettings("UserName", textBoxUserName.Text);
                        else
                            Common.Core.SetSettings("UserName", "");
                    }

                    Common.Core.SetSettings("Authentication", comboBoxAuthentication.Text);
                    Common.Core.SetSettings("RememberMe", checkBoxRememberMe.Checked.ToString());

                    DialogResult = DialogResult.OK;
                } 
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private void comboBoxAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Common.Core.UserAuthentication = (comboBoxAuthentication.Text == "Windows Authentication") ? Common.Core.Authentication.Windows : Common.Core.Authentication.Application;
                
                Func<bool,bool> LockControls = (bool Lock) =>
                {
                    textBoxUserName.Enabled = !Lock;
                    textBoxPassword.Enabled = !Lock;
                    checkBoxRememberMe.Enabled = !Lock;

                    if (Lock)
                    {
                        textBoxUserName.Text = windowsUserName; //set windows user name if selected authentication is Windows.
                        checkBoxRememberMe.Checked = false;
                    }
                    else
                    {
                        string isRememberMe;
                        isRememberMe = System.Configuration.ConfigurationManager.AppSettings["RememberMe"];
                         
                        if (isRememberMe != null)
                        {
                            checkBoxRememberMe.Checked = Convert.ToBoolean(isRememberMe);
                            textBoxUserName.Text = System.Configuration.ConfigurationManager.AppSettings["UserName"]; 
                        } 
                    }

                    return true;
                }; 

                LockControls(Common.Core.UserAuthentication == BTSS.Common.Core.Authentication.Windows ? true:false);
                 
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
                user = new BTSS.Logic.User(Common.Core.ConnectionString);
 
                string authentication;
                authentication = System.Configuration.ConfigurationManager.AppSettings["Authentication"];

                windowsUserName = Environment.UserDomainName + "\\" + Environment.UserName;

                if (authentication == "Application Authentication")
                { 
                    string value;
                    value = System.Configuration.ConfigurationManager.AppSettings["RememberMe"];
                     
                    if (value != null)
                    {
                        checkBoxRememberMe.Checked = Convert.ToBoolean(value);
                        textBoxUserName.Text = System.Configuration.ConfigurationManager.AppSettings["UserName"];
                        comboBoxAuthentication.SelectedIndex = 1; 
                        textBoxPassword.Focus();                    
                    }
                }
                else
                {
                    comboBoxAuthentication.SelectedIndex = 0;
                    textBoxUserName.Text = windowsUserName;
                }


               
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Lock(bool enable)
        {
            try
            {
                textBoxUserName.Enabled = !enable; 
                textBoxPassword.Enabled = !enable;                
                checkBoxRememberMe.Enabled = !enable;
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
