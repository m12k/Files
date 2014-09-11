using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;

namespace BTSS.Presentation
{
    public partial class FormBrowseUser : Form
    {

        #region Constructor
        public FormBrowseUser()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        public Users User { get; set; }

        #endregion

        #region Declarations

        #endregion

        #region Event Handlers
         
        private void FormBrowseUser_Load(object sender, EventArgs e)
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

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            { 
                if (textBoxSearch.TextLength > 0)
                {
                    string textSearch = textBoxSearch.Text;
                    List<Users> users = new List<Users>();
                    
                    DataTable groups = new DataTable();
                    groups.Columns.Add("domain");
                    groups.Columns.Add("group");
                    groups.Rows.Add("PRD", "business technologies db users");
                    groups.Rows.Add("PRD", "SHR MNE1050 Dept Ops Business Tech - RWM"); 
                    groups.Rows.Add("MLIDDOMAIN1", "business technology db users");

                    using (var loader = new Common.Loader<string>(o =>
                    { 
                        //Start of: getting the data.
                        for (int i = 0; i <= groups.Rows.Count - 1; i++)
                        {
                            using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, groups.Rows[i]["domain"].ToString()))
                            {
                                GroupPrincipal gp = GroupPrincipal.FindByIdentity(ctx, IdentityType.Name, groups.Rows[i]["group"].ToString());
                                
                                var usr = (from a in gp.Members
                                           where a.DisplayName.ToLower().Contains(textSearch.ToLower())
                                           select new Users()
                                           {
                                               UserName = a.SamAccountName,
                                               AccountName = a.SamAccountName,
                                               DisplayName = a.DisplayName,
                                               Email = ((UserPrincipal)a).EmailAddress,
                                               LastName = ((UserPrincipal)a).Surname,
                                               FirstName = ((UserPrincipal)a).GivenName,
                                               MiddleName= ((UserPrincipal)a).MiddleName,
                                               Domain = groups.Rows[i]["domain"].ToString()
                                           });

                                users.AddRange(usr); 
                            }
                        }
                        //End of: getting the data.
                    }))
                    {
                        loader.RunWorker();
                    };

                    dataGridViewUsers.DataSource = users.OrderBy(a => a.DisplayName).ToList(); 
                        
                } 

            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewUsers.SelectedRows.Count > 0)
                {
                    User = new Users();
                    User.LastName = (string)dataGridViewUsers.SelectedRows[0].Cells["LastName"].Value;
                    User.FirstName = (string)dataGridViewUsers.SelectedRows[0].Cells["FirstName"].Value;
                    User.MiddleName = (string)dataGridViewUsers.SelectedRows[0].Cells["MiddleName"].Value;
                    User.AccountName = (string)dataGridViewUsers.SelectedRows[0].Cells["AccountName"].Value;
                    User.UserName = (string)dataGridViewUsers.SelectedRows[0].Cells["UserName"].Value;
                    User.Domain = (string)dataGridViewUsers.SelectedRows[0].Cells["Domain"].Value;

                    DialogResult = DialogResult.OK;
                }
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
                dtUsers.Columns.Add("E-Mail");
                dtUsers.Columns.Add("UserName");
                dtUsers.Columns.Add("AccountName");
                dtUsers.Columns.Add("LastName");
                dtUsers.Columns.Add("FirstName");
                dtUsers.Columns.Add("MiddleName");
                dtUsers.Columns.Add("Domain");  
                dataGridViewUsers.DataSource = dtUsers;

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
                foreach (DataGridViewColumn col in dataGridViewUsers.Columns)
                {
                    switch (col.HeaderText)
                    {
                        case "DisplayName": col.HeaderText = "Display Name"; col.Width = 80; break;
                        case "E-Mail": col.Width = 80; break;
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

        #endregion 
      
    }
}


public class Users
{
    public string DisplayName { get; set; }
    public string Email { get; set; }    
    public string AccountName { get; set; }
    public string UserName { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string Domain { get; set; }
}