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
    public partial class FormAuditTrail : Form
    {

        #region "Declarations"

        private Logic.Interfaces.IAudit audit;
        private List<Logic.GetAuditListResult> data;
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

                var result = from s in data
                             where s.FieldName.ToLower().Contains(textSearch.ToLower()) ||
                             s.NewValue.ToLower().Contains(textSearch.ToLower()) ||
                             s.OldValue.ToLower().Contains(textSearch.ToLower()) ||
                             s.TableName.ToLower().Contains(textSearch.ToLower()) ||
                             s.UserName.ToLower().Contains(textSearch.ToLower())
                             select s;


                userControlListing.DataSource = result.ToList();
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

                audit = new Logic.Audit(Common.Core.ConnectionString);
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
                dtAudit.Columns.Add("AuditId");
                dtAudit.Columns.Add("Type");
                dtAudit.Columns.Add("TableName");
                dtAudit.Columns.Add("PK");
                dtAudit.Columns.Add("FieldName");
                dtAudit.Columns.Add("OldValue");
                dtAudit.Columns.Add("NewValue");
                dtAudit.Columns.Add("UpdateDate");
                dtAudit.Columns.Add("UserName");
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
                foreach (DataGridViewColumn col in userControlListing.dataGridViewList.Columns)
                {
                    switch (col.HeaderText)
                    {
                        case "Type": col.HeaderText = "Action"; break;
                        case "TableName": col.HeaderText = "Table Name"; break;
                        case "FieldName": col.HeaderText = "Field Name"; break;
                        case "OldValue": col.HeaderText = "Old Value"; break;
                        case "NewValue": col.HeaderText = "New Value"; break;
                        case "UpdateDate": col.HeaderText = "Date"; break;
                        case "UserName": col.HeaderText = "User Name"; break;
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
                List<string> users = audit.GetUsers();

                foreach (string user in users)
                {
                    treeViewAudit.Nodes["NodeUsers"].Nodes.Add(user);
                }

                List<string> tables = audit.GetTables();

                foreach (string table in tables)
                {
                    treeViewAudit.Nodes["NodeTables"].Nodes.Add(table);
                }
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
                string userName = "";
                string tableName = "";

                if (treeViewAudit.SelectedNode.Parent != null)
                {
                    if (treeViewAudit.SelectedNode.Parent.Text == "Tables")
                        tableName = treeViewAudit.SelectedNode.Text;
                    if (treeViewAudit.SelectedNode.Parent.Text == "Users")
                        userName = treeViewAudit.SelectedNode.Text;
                }

                data = audit.GetAuditList(userName, tableName);

                userControlListing.DataSource = data;

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}
