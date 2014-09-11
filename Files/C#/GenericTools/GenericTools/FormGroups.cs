using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace WindowsFormsApplication1
{
    public partial class FormGroups : Form
    {
        public FormGroups()
        {
            InitializeComponent();
        }

        public DataTable  GetGroups(string username)
        {

            try
            {
                string result = string.Empty;
                DataTable dt = new DataTable();

                dt.Columns.Add("Group Name");
               


                // if you do repeated domain access, you might want to do this *once* outside this method, 
                // and pass it in as a second parameter!
                PrincipalContext yourDomain = new PrincipalContext(ContextType.Domain);

                // find the user
                UserPrincipal user = UserPrincipal.FindByIdentity(yourDomain, username);

                using (user)
                {
                    var groups = user.GetGroups();
                    using (groups)
                    {
                        foreach (Principal group in groups) // cycle through all the groups for this user
                        {
                            dt.Rows.Add(group.SamAccountName);
                            //Console.WriteLine(group.SamAccountName + "-" + group.DisplayName);
                        }
                    } // end using (groups)
                } // end using (p)

                dt.DefaultView.Sort = "Group Name ASC";
                return dt.DefaultView.ToTable();

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = GetGroups(textBox1.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("User Name not found.");
                //throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.DataSource = GetGroups(textBox2.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("User Name not found.");
            }
        }

    }
}
