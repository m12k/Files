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
    public partial class FormActiveDirectory : Form
    {

        #region Contructor
                
        public FormActiveDirectory()
        {
            InitializeComponent();
        }

        #endregion

        #region Declarations

        #endregion

        #region EventHandlers
        
        private void FormActiveDirectory_Load(object sender, EventArgs e)
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

        private void buttonBrowseGroup_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormActiveDirectoryGroupNew frm = new FormActiveDirectoryGroupNew())
                {
                    frm.ModuleReference = BTSS.Common.Core.ModuleReference.Tools;
                    frm.Domain = userControlDomainMemberofGroup.Text;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {                        
                        textBoxMembersSpecifiedGroup.Text = frm.Name;
                    }
                }                
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ButtonMembersSpecifiedGroupGo_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormActiveDirectoryResultsView frm = new FormActiveDirectoryResultsView())
                {
                    frm.Caption = "List of Members of group: " + textBoxMembersSpecifiedGroup.Text;
                    frm.TransactionType = FormActiveDirectoryResultsView.TransactionTypes.MemberOfGroup;
                    frm.SearchType = FormActiveDirectoryResultsView.SearchTypes.User;
                    frm.Domain = userControlDomainMemberofGroup.Text;
                    frm.Name = textBoxMembersSpecifiedGroup.Text;
                    frm.ShowDialog();
                } 
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private void buttonDomainGroupsOfUserGo_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormActiveDirectoryResultsView frm = new FormActiveDirectoryResultsView())
                { 
                    frm.Caption = "List of Groups of user: " + textBoxDomainGroupsOfUser.Text; ;
                    frm.TransactionType = FormActiveDirectoryResultsView.TransactionTypes.GroupsOfUser;
                    frm.SearchType = FormActiveDirectoryResultsView.SearchTypes.Group;
                    frm.Domain = userControlDomainGroupsOfUser.Text;
                    frm.Name = textBoxDomainGroupsOfUser.Text; 
                    frm.ShowDialog();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void buttonSearchGroupUserGo_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormActiveDirectoryResultsView frm = new FormActiveDirectoryResultsView())
                {                    
                    if (comboBoxGroupUser.Text == "Group")
                    {
                        frm.SearchType = FormActiveDirectoryResultsView.SearchTypes.Group;
                        frm.Caption = "Search Results for Groups like ''" + textBoxSearchGroupUser.Text + "''";
                    }
                    else //User
                    {
                        frm.SearchType = FormActiveDirectoryResultsView.SearchTypes.User;
                        frm.Caption = "Search Results for Users like ''" + textBoxSearchGroupUser.Text + "''";
                    }

                    frm.Name = textBoxSearchGroupUser.Text;
                    frm.Domain = userControlDomainSearch.Text;
                    frm.TransactionType = FormActiveDirectoryResultsView.TransactionTypes.SearchGroupOrUser;
                    frm.ShowDialog();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
         
        private void textBoxMembersSpecifiedGroup_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ButtonMembersSpecifiedGroupGo.Enabled = textBoxMembersSpecifiedGroup.TextLength > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void textBoxDomainGroupsOfUser_TextChanged(object sender, EventArgs e)
        {
            try
            {
                buttonDomainGroupsOfUserGo.Enabled = textBoxDomainGroupsOfUser.TextLength > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void textBoxSearchGroupUser_TextChanged(object sender, EventArgs e)
        {
            try
            {
                buttonSearchGroupUserGo.Enabled = textBoxSearchGroupUser.TextLength > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private void userControlDomainMemberofGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBoxMembersSpecifiedGroup.Clear();
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

                comboBoxGroupUser.SelectedIndex = 0;

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion 

    }
}
