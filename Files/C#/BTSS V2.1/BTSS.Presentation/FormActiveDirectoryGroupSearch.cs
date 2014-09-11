using System;
using System.Collections;
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
    public partial class FormActiveDirectoryGroupSearch : Form
    {

        #region Constructor
               
        public FormActiveDirectoryGroupSearch()
        {
            InitializeComponent();
        }

        #endregion

        #region Declarations
         
        List<Logic.DomainGroup> data;
                                 
        #endregion

        #region Properties

        private string _TextSearch;
        public string TextSearch { set { _TextSearch = value; } }

        private string _Domain;
        public string Domain
        {
            set
            {
                StringBuilder sb = new StringBuilder();
                _Domain = value;
                sb.Append(Text).Append(" (").Append(value).Append(")");
                Text = sb.ToString();
            }
        }

        private string _SelectedItems;
        public string SelectedItems { get { return _SelectedItems; } }

        #endregion

        #region EventHandlers
         
        private void FormActiveDirectoryGroupSearch_Shown(object sender, EventArgs e)
        {
            try
            {
                Initialize();
                LoadSearch();
            }
            catch (Exception)
            {
                throw;
            }
        } 

        private void userControlListingData_OnTextSearchChanged(string textSearch)
        {
            try
            {
                var result = from s in data
                             where s.Name.ToLower().Contains(textSearch.ToLower())
                             select s;

                userControlListingData.DataSource = result.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (userControlListingData.dataGridViewList.SelectedRows.Count == 0)
                {
                    DialogResult = DialogResult.Cancel;
                    return;
                }

                StringBuilder sb = new StringBuilder();
                
                if (userControlListingData.dataGridViewList.SelectedRows.Count > 0)
                { 
                    foreach(DataGridViewRow row in userControlListingData.dataGridViewList.SelectedRows)
                    {   
                        sb.Append(row.Cells["Domain"].Value.ToString()).Append("\\").Append(row.Cells["Name"].Value.ToString()).Append(";"); 
                    }
                 }

                _SelectedItems = sb.ToString();

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
                DataTable dtGroup = new DataTable(); 
                dtGroup.Columns.Add("Name");
                dtGroup.Columns.Add("Domain"); 

                userControlListingData.DataSource = dtGroup;

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
                foreach (DataGridViewColumn col in userControlListingData.dataGridViewList.Columns)
                {
                    switch (col.HeaderText)
                    { 
                        case "Name": col.HeaderText = "Name"; break;
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

        private void LoadSearch()
        {
            try
            {
                data = new List<BTSS.Logic.DomainGroup>();

                using (var loader = new Common.Loader<string>(o =>
                {
                    foreach (string domain in _Domain.Split(','))
                    {
                        PrincipalContext ctx = new PrincipalContext(ContextType.Domain, domain);
                        GroupPrincipal gp = new GroupPrincipal(ctx);

                        StringBuilder sb = new StringBuilder();
                        sb.Append("*").Append(_TextSearch).Append("*");

                        gp.Name = sb.ToString();

                        PrincipalSearcher ps = new PrincipalSearcher(gp);

                        var list = (from u in ps.FindAll()
                                select new Logic.DomainGroup { Name = u.Name, Domain = u.Context.Name }).OrderBy(a => a.Name);
                        
                        data.AddRange(list.ToList()); 
                    } 
                }))
                {
                    loader.RunWorker();
                };
                                  
                userControlListingData.DataSource = data; 
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion 

    }


}
