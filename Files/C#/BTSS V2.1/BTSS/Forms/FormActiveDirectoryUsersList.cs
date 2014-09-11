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
using System.Globalization;

namespace Biztech 
{
    public partial class FormActiveDirectoryUsersList : Form
    {
       
        #region Constructor
        
        public FormActiveDirectoryUsersList()
        {
            InitializeComponent();
        }

        #endregion 

        #region Declarations

        ActiveDirectoryUsersList activeDirectoryUsersList;
        private TreeNode prevNode;
        private IEnumerable<UserDetails> userDetails;

        #endregion
        
        #region Event Handlers
        
        private void FormActiveDirectoryUsersList_Load(object sender, EventArgs e)
        {
            try
            {
                Initialize();
                RefreshTree();

            }
            catch (Exception)
            {
                throw;
            }
        }
         
        private void treeViewGroup_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                //Start of: Change Font of  selected node
                if (prevNode != null)
                {
                    prevNode.ForeColor = Color.Black;
                    prevNode.NodeFont = new Font(Font.FontFamily, Font.Size, FontStyle.Regular);
                }

                prevNode = treeViewGroup.SelectedNode;

                treeViewGroup.SelectedNode.ForeColor = Color.Blue;
                treeViewGroup.SelectedNode.NodeFont = new Font(Font.FontFamily.Name, Font.Size, FontStyle.Regular);
                //End of: Change Font of  selected node

                PrincipalContext ctx = new PrincipalContext(ContextType.Domain, treeViewGroup.SelectedNode.ImageKey); 
                
                GroupPrincipal grp = GroupPrincipal.FindByIdentity(ctx, IdentityType.Name, treeViewGroup.SelectedNode.Text);


                userDetails = (from u in grp.Members
                               select new UserDetails { DisplayName = u.DisplayName, UserName = u.SamAccountName,Domain = u.Context.Name }).OrderBy(x => x.DisplayName).ToList();
             
                userControlListing.DataSource = userDetails;
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void buttonAddGroup_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormActiveDirectoryGroup frm = new FormActiveDirectoryGroup())
                {
                    frm.Text = frm.Text + " [Add]";
                    frm.GroupName = "";
                    frm.Operation = Global.Operation.INSERT;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        RefreshTree();
                    } 
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeViewGroup.SelectedNode == null)
                {
                    Global.ShowMessage("Please select item to remove", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                using (FormActiveDirectoryGroup frm = new FormActiveDirectoryGroup())
                {
                    frm.Text = frm.Text + " [Modify]";
                    frm.Id = Convert.ToInt32(treeViewGroup.SelectedNode.Name);
                    frm.GroupName = treeViewGroup.SelectedNode.Text;
                    frm.Operation = Global.Operation.UPDATE;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        RefreshTree();
                    } 
                }
            }
            catch (Exception)
            { 
                throw;
            }
        }

        private void buttonRemoveGroup_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeViewGroup.SelectedNode == null)
                {
                    Global.ShowMessage("Please select item to remove", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (Global.ShowQuestion(treeViewGroup.SelectedNode.Text + " will be removed. Do you want to continue?") == DialogResult.OK)
                {
                    activeDirectoryUsersList.Id = Convert.ToInt32(treeViewGroup.SelectedNode.Name);
                    activeDirectoryUsersList.Name = "";
                    activeDirectoryUsersList.Operation = Global.Operation.DELETE;
                    activeDirectoryUsersList.Save();

                    Global.ShowMessage("Data succesfully deleted!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    

                    //Clear grid rows.
                    List<UserDetails> ar = new List<UserDetails>();
                    userControlListing.DataSource = ar;


                    RefreshTree();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void userControlListing_OnTextSearchChanged(string textSearch)
        {
            try
            {
                var result = from s in userDetails.AsEnumerable()
                             where s.DisplayName.Contains(textSearch) || s.UserName.Contains(textSearch)
                             select s;
                                 
                userControlListing.DataSource = result; 

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

                InitializeGrid();


            }
            catch (Exception)
            {
                throw;
            }
        }

        private void InitializeGrid()
        {
            try
            {
                DataTable dtUsers = new DataTable();                
                dtUsers.Columns.Add("DisplayName");
                dtUsers.Columns.Add("UserName");
                dtUsers.Columns.Add("Domain"); 

                userControlListing.DataSource = dtUsers;

                InitializeGridLayout(); 
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void InitializeGridLayout()
        {
            try
            { 

                foreach (DataGridViewColumn col in userControlListing.dataGridViewList.Columns)
                {
                    switch (col.HeaderText)
                    {
                        case "DisplayName": col.HeaderText = "Display Name"; break;
                        case "UserName": col.HeaderText = "User Name"; break;
                        case "Domain": col.HeaderText = "Domain"; break;
                        default:
                            col.Visible = false; break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void RefreshTree()
        {
            try
            {
                treeViewGroup.Nodes.Clear();

                List<GetActiveDirectoryGroupListResult> list = activeDirectoryUsersList.GetActiveDirectoryGroupList();
                foreach (var value in list)
                {
                    treeViewGroup.Nodes.Add(value.Id.ToString(), value.Name, value.Domain);                
                }
                 
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
         
    }

    public class UserDetails
    {
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Domain { get; set; }
    }
     
}
 