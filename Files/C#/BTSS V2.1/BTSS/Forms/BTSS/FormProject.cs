using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Biztech.Classes;

namespace Biztech.Forms.BTSS
{
    public partial class FormProject : Form
    {
        #region "Constructor"
                
        public FormProject()
        {
            InitializeComponent();
        }

        #endregion 

        #region "Properties"

        public string ProjectId { get; set; }
        public Global.Operation Operation { get; set; }

        #endregion

        #region "Declarations"
        public event EventHandler OnSave;

        private ClassProject classProject;
        private ClassCheckConnection classCheckConnection; 
        private Validation Validation;

        #endregion
        
        #region "Event Handlers"

        private void FormProject_Load(object sender, EventArgs e)
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

        private void buttonBrowseSharepointURL_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();

                if (fbd.ShowDialog() == DialogResult.OK)
                {

                    textBoxSharepointURL.Text = fbd.SelectedPath;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void buttonBrowseErrorLogPath_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    textBoxErrorLogPath.Text = fbd.SelectedPath;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void buttonBrowseFileMDB_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();

                ofd.Filter = "2003-2010 Access(*.mdb;*.accdb)|*.mdb;*.accdb;*.mde";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    textBoxFileMDB.Text = ofd.FileName;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void buttonBrowseFileMDW_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();

                ofd.Filter = "*.mdw|*.mdw";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    textBoxFileMDW.Text = ofd.FileName;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void buttonTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonMSAccess.Checked)
                {
                    if (textBoxFileMDB.Text == "") { Global.ShowMessage("Please select mdb file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                    classCheckConnection = new ClassCheckConnection(Global.GetConnectionString(textBoxFileMDB.Text, textBoxAccessUserID.Text, textBoxAccessPassword.Text, checkBoxOtherDetails.Checked, textBoxFileMDW.Text), Global.DataProvider.OLEDB);
                }
                else
                {
                    string msg = "";

                    if (textBoxSQLDatasource.Text == "")
                        msg += "Datasource\n";
                    if (textBoxSQLDatabaseName.Text == "")
                        msg += "Database Name\n";

                    if (msg.Length > 0)
                    {
                        Global.ShowMessage("Please fill in required fields.\n\n" + msg, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    classCheckConnection = new ClassCheckConnection(Global.GetConnectionString(textBoxSQLDatasource.Text, textBoxSQLDatabaseName.Text, textBoxSQLUserId.Text, textBoxSQLPassword.Text), Global.DataProvider.SQL);
                }
                
                if (classCheckConnection.CheckConnection())
                    Global.ShowMessage("Connection Succesful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    Global.ShowMessage("Connection Failed!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void userControlButtonsSave_OnButtonClick(EventArgs e, ref bool isOk, Biztech.UserControls.UserControlButtonsSave.ButtonClick button)
        {
            try
            {
                switch (button)
                {
                    case Biztech.UserControls.UserControlButtonsSave.ButtonClick.Save:
                        if (Save())
                        {
                            if (Operation == Global.Operation.UPDATE)
                                DialogResult = DialogResult.OK;
                        }
                        break;
                    case  Biztech.UserControls.UserControlButtonsSave.ButtonClick.Cancel: DialogResult = DialogResult.Cancel; break; 
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
         
        private void radioButtonMSAccess_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LockGroupSQL(true);
                LockGroupMSAccess(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void radioButtonSQL_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LockGroupMSAccess(true);
                LockGroupSQL(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void checkBoxOtherDetails_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                textBoxFileMDW.Enabled = checkBoxOtherDetails.Checked;
                buttonBrowseFileMDW.Enabled = checkBoxOtherDetails.Checked;
                textBoxAccessUserID.Enabled = checkBoxOtherDetails.Checked;
                textBoxAccessPassword.Enabled = checkBoxOtherDetails.Checked;

                if (!checkBoxOtherDetails.Checked)
                {
                    textBoxAccessUserID.Clear();
                    textBoxAccessPassword.Clear();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
  
        #endregion

        #region "Methods" 

        private void Initialize()
        {
            try
            {
                classProject = new ClassProject(Global.ConnectionString);
                Validation = new Validation();
                Validation.Validate(this);
 
                if (Operation == Global.Operation.INSERT)
                { 
                    radioButtonMSAccess.Checked = true; 
                }
                else if (Operation == Global.Operation.UPDATE || Operation == Global.Operation.VIEW)
                { 
                    GetProjectDetailsResult result = classProject.GetProjectDetails(ProjectId);

                    textBoxID.Text = ProjectId;
                    textBoxName.Text = result.Name;
                    textBoxDesription.Text = result.Desc;
                    checkBoxIsActive.Checked = result.IsActive.Value;
                    textBoxVersion.Text = result.Version;
                    textBoxSharepointURL.Text = result.SharepointURL;
                    textBoxErrorLogPath.Text = result.PathErrorLog;
                    textBoxBusinessOwner.Text = result.BusinessOwner;
                    textBoxTester.Text = result.Tester;
                    textBoxOtherContact.Text = result.OtherContact;
                    checkBoxOtherDetails.Checked = result.HasOtherDetails.Value;

                    textBoxSQLDatasource.Text = result.Datasource;
                    textBoxSQLDatabaseName.Text = result.DatabaseName;
                    textBoxSQLUserId.Text = result.UserID;
                    textBoxSQLPassword.Text = result.Password;
                    textBoxFileMDB.Text = result.File;
                    checkBoxAllowDevelopmentMode.Checked = result.EnableBypassKey.Value;
                    checkBoxAllowDevelopmentMode.Tag = result.EnableBypassKey.Value;
                    textBoxFileMDW.Text = result.MDW;
                    textBoxAccessUserID.Text = result.UserID;
                    textBoxAccessPassword.Text = result.Password;

                    if (result.Provider == "OLEDB")
                    {
                        radioButtonMSAccess.Checked = true;
                    }
                    else //SQL
                    {
                        radioButtonSQL.Checked = true;
                    }

                    if (Operation == Global.Operation.VIEW)
                    {
                        userControlButtonsSave.Visible = false;
                        Lock();
                    }
                }

                textBoxName.Focus();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool Save()
        {
            try
            {
                if (!Validation.IsTextBoxesValid())
                {
                    Global.ShowMessage("Please fill-in required field(s).", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
 
                if (checkBoxAllowDevelopmentMode.Checked != Convert.ToBoolean(checkBoxAllowDevelopmentMode.Tag)) SetByPassKey();

                ControlToClass();
                classProject.Save(); 

                if (Operation == Global.Operation.INSERT) { Global.ShowMessage("Project " + textBoxName.Text + " successfully saved!", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                else { Global.ShowMessage("Project " + textBoxName.Text + " successfully updated!", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                ClearControls();
                
                if (Operation == Global.Operation.INSERT)
                    OnSave(new object(), new EventArgs());

                textBoxName.Focus();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Lock()
        {
            try
            { 
                textBoxID.ReadOnly = true;
                textBoxName.ReadOnly = true;
                textBoxDesription.ReadOnly = true;
                checkBoxIsActive.Enabled = false;
                textBoxVersion.ReadOnly = true;
                textBoxSharepointURL.ReadOnly = true;
                textBoxErrorLogPath.ReadOnly = true;
                textBoxBusinessOwner.ReadOnly = true;
                textBoxTester.ReadOnly = true;
                textBoxOtherContact.ReadOnly = true;
                checkBoxOtherDetails.Enabled = false;

                textBoxSQLDatasource.ReadOnly = true;
                textBoxSQLDatabaseName.ReadOnly = true;
                textBoxSQLUserId.ReadOnly = true;
                textBoxSQLPassword.ReadOnly = true;
                textBoxFileMDB.ReadOnly = true;
                checkBoxAllowDevelopmentMode.Enabled = false;
                textBoxFileMDW.ReadOnly = true;
                textBoxAccessUserID.ReadOnly = true; 
                textBoxAccessPassword.ReadOnly = true;
                radioButtonMSAccess.Enabled = false;
                radioButtonSQL.Enabled = false;
                buttonBrowseErrorLogPath.Enabled = false;
                buttonBrowseFileMDB.Enabled = false;
                buttonBrowseFileMDW.Enabled = false;
                buttonBrowseSharepointURL.Enabled = false;
                buttonTestConnection.Enabled = false;

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void LockGroupMSAccess(bool isLock)
        {
            try
            {
                textBoxFileMDB.Enabled = (!isLock);
                buttonBrowseFileMDB.Enabled = (!isLock);
                checkBoxAllowDevelopmentMode.Enabled = (!isLock);
                
                checkBoxOtherDetails.Enabled = (!isLock);

                textBoxFileMDW.Enabled = checkBoxOtherDetails.Checked;
                buttonBrowseFileMDW.Enabled = checkBoxOtherDetails.Checked;
                textBoxAccessUserID.Enabled = checkBoxOtherDetails.Checked;
                textBoxAccessPassword.Enabled = checkBoxOtherDetails.Checked;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void LockGroupSQL(bool isLock)
        {
            try
            {
                textBoxSQLDatasource.Enabled = (!isLock);
                textBoxSQLDatabaseName.Enabled = (!isLock);
                textBoxSQLUserId.Enabled = (!isLock);
                textBoxSQLPassword.Enabled = (!isLock);

                textBoxFileMDW.Enabled = false;
                buttonBrowseFileMDW.Enabled = false;
                textBoxAccessUserID.Enabled = false;
                textBoxAccessPassword.Enabled = false;

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ControlToClass()
        {
            try
            {
                classProject.Operation = Operation;
                classProject.Id = ProjectId;
                classProject.Name = textBoxName.Text;
                classProject.Description = textBoxDesription.Text;
                classProject.IsActive = checkBoxIsActive.Checked;
                classProject.Version = textBoxVersion.Text;
                classProject.SharepointURL = textBoxSharepointURL.Text;
                classProject.ErrorLogPath = textBoxErrorLogPath.Text;
                classProject.BusinessOwner = textBoxBusinessOwner.Text;
                classProject.Tester = textBoxTester.Text;
                classProject.OtherContact = textBoxOtherContact.Text;
                classProject.DataProvider = radioButtonMSAccess.Checked == true ? Global.DataProvider.OLEDB : Global.DataProvider.SQL;
                classProject.FileMBD = textBoxFileMDB.Text;
                classProject.EnableByPassKey = checkBoxAllowDevelopmentMode.Checked;
                classProject.HasOtherDetails = checkBoxOtherDetails.Checked;
                classProject.FileMDW = textBoxFileMDW.Text;
                classProject.AccessUserId = textBoxAccessUserID.Text;
                classProject.AccessPassword = textBoxAccessPassword.Text;
                classProject.Datasource = textBoxSQLDatasource.Text;
                classProject.SQLDatabaseName = textBoxSQLDatabaseName.Text;
                classProject.SQLUserId = textBoxSQLUserId.Text;
                classProject.SQLPassword = textBoxSQLPassword.Text;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SetByPassKey()
        {
            var oAccess = new Microsoft.Office.Interop.Access.ApplicationClass();
            try
            {
                oAccess.OpenCurrentDatabase(textBoxFileMDB.Text, true, textBoxAccessPassword.Text);
                if (checkBoxAllowDevelopmentMode.Checked)
                    oAccess.DoCmd.RunMacro("MacroEnableByPassKey", 1, 1);
                else
                    oAccess.DoCmd.RunMacro("MacroDisableByPassKey", 1, 1);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                oAccess.Quit(Microsoft.Office.Interop.Access.AcQuitOption.acQuitSaveAll);
            }
        }

        private void ClearControls()
        {
            try
            {
                textBoxID.Clear();
                textBoxName.Clear();
                textBoxDesription.Clear();
                checkBoxIsActive.Checked = false;
                textBoxVersion.Clear();
                textBoxSharepointURL.Clear();
                textBoxErrorLogPath.Clear();
                textBoxBusinessOwner.Clear();
                textBoxTester.Clear();
                textBoxOtherContact.Clear();
                checkBoxOtherDetails.Checked = false;
                textBoxFileMDB.Clear();
                textBoxFileMDW.Clear();
                textBoxAccessUserID.Clear();
                textBoxAccessPassword.Clear();
                textBoxSQLDatasource.Clear();

                textBoxSQLDatabaseName.Clear();
                textBoxSQLUserId.Clear();
                textBoxSQLPassword.Clear();
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion 
          
    } 

}
