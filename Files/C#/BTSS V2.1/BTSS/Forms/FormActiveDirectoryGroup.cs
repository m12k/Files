using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Biztech.Classes;


using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;


namespace Biztech 
{
    public partial class FormActiveDirectoryGroup : Form
    {
        #region Constructor

        public FormActiveDirectoryGroup()
        {
            InitializeComponent();
        }

        #endregion

        #region Declarations

        ActiveDirectoryUsersList activeDirectoryUsersList;

        #endregion


        #region Properties

        private int _Id;
        public int Id { get { return _Id; } set { _Id = value; } }

        private string _GroupName;
        public string GroupName { get { return _GroupName; } set { _GroupName = value; textBoxName.Text = _GroupName; } }

        private Global.Operation _Operation;
        public Global.Operation Operation { get { return _Operation; } set { _Operation = value; } }

        #endregion

        #region Event Handlers

        private void FormActiveDirectoryGroup_Load(object sender, EventArgs e)
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (textBoxName.TextLength == 0)
                {
                    Global.ShowMessage("Name is required!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                string domain = "";

                if (!IsGroupExist(ref domain))
                {
                    Global.ShowMessage("Group Name does not exist in the Active Directory. Please check!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                 
                activeDirectoryUsersList.Id = _Id;
                activeDirectoryUsersList.Name = textBoxName.Text;
                activeDirectoryUsersList.Domain = domain;
                activeDirectoryUsersList.Operation = _Operation;
                activeDirectoryUsersList.Save();

                Global.ShowMessage("Succesfully Saved!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
            }
            catch (Exception)
            { 
                throw;
            }
        }

        #endregion

        #region Methods
         
        private void Initialize()
        {
            try
            {
                activeDirectoryUsersList = new ActiveDirectoryUsersList(Global.ConnectionString);
                 
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsGroupExist(ref string domain)
        {
            try
            {
                PrincipalContext ctxPRD = new PrincipalContext(ContextType.Domain, "prd.manulifeusa.com");
                GroupPrincipal grpPRD = GroupPrincipal.FindByIdentity(ctxPRD, IdentityType.Name, textBoxName.Text);
                 
                if (grpPRD == null)
                {
                    domain = "MLIDDOMAIN1";

                    PrincipalContext ctxMLI = new PrincipalContext(ContextType.Domain, "MLIDDOMAIN1");
                    GroupPrincipal grpMLI = GroupPrincipal.FindByIdentity(ctxMLI, IdentityType.Name, textBoxName.Text);
                    return grpMLI == null ? false : true; 
                } 
                else
                {
                    domain = "prd.manulifeusa.com";
                    return true;
                } 
                
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

         
    }
}
