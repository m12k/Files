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

namespace Biztech.Forms
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {
                throw;
            }
        }

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
                    List<string> Domain = new List<string>();
                    List<Users> users = new List<Users>();
                     
                    Domain.Add("prd.manulifeusa.com");
                    Domain.Add("MLIDDOMAIN1");


                    foreach(string domain in Domain)
                    {
                        PrincipalContext ctx = new PrincipalContext(ContextType.Domain, domain);
                        UserPrincipal up = new UserPrincipal(ctx);
                             
                        up.DisplayName = "*" + textBoxSearch.Text + "*";                          
                        //up.SamAccountName = "*" + textBoxSearch.Text + "*";
                                                 
                         
                        PrincipalSearcher search = new PrincipalSearcher(up);                      
                                                 
                        //foreach (Principal p in search.FindAll().OrderBy(a=> a.DisplayName))
                        foreach (Principal p in search.FindAll())
                        { 
                            var FoundUser = p as UserPrincipal;                         
                            Users user = new Users();
                            user.UserName = p.SamAccountName;
                            user.AccountName = p.SamAccountName;
                            user.DisplayName = FoundUser.DisplayName;
                            user.Email = FoundUser.EmailAddress;
                            user.LastName = FoundUser.Surname;
                            user.FirstName = FoundUser.GivenName;
                            user.Domain = domain;
                            users.Add(user);
                        } 
                    }
                     
                    
                    dataGridViewUsers.DataSource = users.OrderBy(a=>a.DisplayName).ToList(); 
                     
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
                    User.AccountName = (string)dataGridViewUsers.SelectedRows[0].Cells["AccountName"].Value;
                    User.UserName = (string)dataGridViewUsers.SelectedRows[0].Cells["UserName"].Value;

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
    public string Domain { get; set; }
}