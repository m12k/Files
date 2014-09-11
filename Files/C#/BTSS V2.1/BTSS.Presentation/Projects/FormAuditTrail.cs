using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BTSS.Presentation.Projects
{
    public partial class FormAuditTrail : Form
    {

        #region "Declarations"

        private Logic.Interfaces.Projects.IAudit audit;
        private DataTable data;
        private TreeNode prevNode; 
        #endregion

        #region "Constructor"

        public FormAuditTrail()
        {
            InitializeComponent();
        }

        #endregion

        #region "Event Handlers"

        private void FormAuditTrail_Load(object sender, EventArgs e)
        {
            try
            {
                Text = "Audit Trail: " + Common.Core.ProjectName;
                Initialize();
                InitializeGrid();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void userControlListing_OnRefresh(object sender, EventArgs e)
        {
            try
            {
                RefreshListing();
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
                var result = from s in data.AsEnumerable()
                             where s.Field<string>("Operation").ToLower().Contains(textSearch.ToLower()) || 
                             s.Field<string>("TableName").ToLower().Contains(textSearch.ToLower()) || 
                             s.Field<string>("PrimaryKey").ToLower().Contains(textSearch.ToLower()) ||
                             s.Field<string>("UserName").ToLower().Contains(textSearch.ToLower()) 
                             select s;

                if (result.Any())
                    userControlListing.DataSource = result.CopyToDataTable();
                else
                {
                    userControlListing.DataSource = data.Clone(); 
                } 
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void treeViewAudit_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (prevNode != null)
                {
                    prevNode.ForeColor = Color.Black;
                    prevNode.NodeFont = new Font(Font.FontFamily, Font.Size, FontStyle.Regular);
                }

                prevNode = treeViewAudit.SelectedNode;

                treeViewAudit.SelectedNode.ForeColor = Color.Blue;
                treeViewAudit.SelectedNode.NodeFont = new Font(Font.FontFamily.Name, Font.Size, FontStyle.Bold);

                RefreshListing();
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

                audit = new Logic.Projects.Audit(Common.Core.ProjectConnectionString, Common.Core.ProjectDataProvider);
                LoadTree(); 
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
                DataTable dtAudit = new DataTable();
                dtAudit.Columns.Add("Operation");
                dtAudit.Columns.Add("TableName");
                dtAudit.Columns.Add("PrimaryKey");
                dtAudit.Columns.Add("UserName");
                dtAudit.Columns.Add("CreatedDate");
                dtAudit.Columns.Add("Memo"); 
                userControlListing.DataSource = dtAudit;

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
                userControlListing.dataGridViewList.SelectionMode = DataGridViewSelectionMode.CellSelect;

                foreach (DataGridViewColumn col in userControlListing.dataGridViewList.Columns)
                {
                    switch (col.HeaderText)
                    {
                        case "Operation": col.HeaderText = "Action"; break;
                        case "TableName": col.HeaderText = "Table Name"; break;
                        case "PrimaryKey": col.HeaderText = "Primary Key"; break;
                        case "UserName": col.HeaderText = "User Name"; break;
                        case "CreatedDate": col.HeaderText = "Created Date"; break;
                        case "Memo": 
                            col.HeaderText = "Memo";
                            col.Width = 300;                             
                            break; 
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

        private void LoadTree()
        {
            try
            {
                List<string> users = audit.GetUsersList().AsEnumerable().Select(x=>x.Field<string>("UserName")).ToList<string>();

                foreach (string user in users) 
                    treeViewAudit.Nodes["NodeUsers"].Nodes.Add(user);
                 
                List<string> tables = audit.GetTablesList().AsEnumerable().Select(x => x.Field<string>("TableName")).ToList<string>();

                foreach (string table in tables) 
                    treeViewAudit.Nodes["NodeTables"].Nodes.Add(table); 
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void RefreshListing()
        {
            try
            {
                string tableName = "";
                string userName = "";

                if (treeViewAudit.SelectedNode.Parent != null)
                {  
                    if (treeViewAudit.SelectedNode.Parent.Text == "Tables")
                        tableName = treeViewAudit.SelectedNode.Text;
                    if (treeViewAudit.SelectedNode.Parent.Text == "Users")
                        userName = treeViewAudit.SelectedNode.Text;

                    using (var loader = new Common.Loader<string>(o =>
                    {
                        if (tableName.Length > 0)
                            data = audit.GetAuditListByTable(tableName);
                        if (userName.Length > 0)
                            data = audit.GetAuditListByUser(userName);
                    }))
                    {
                        loader.RunWorker();
                    }; 

                    userControlListing.DataSource = data; 
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
