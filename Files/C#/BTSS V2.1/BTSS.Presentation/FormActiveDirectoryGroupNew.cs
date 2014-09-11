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
    public partial class FormActiveDirectoryGroupNew : Form
    {

        #region Contructor
        
        public FormActiveDirectoryGroupNew()
        {
            InitializeComponent();
        }

        #endregion

        #region Declarations

        Logic.Interfaces.IActiveDirectoryGroup activeDirectoryGroup;
        private List<Logic.GetActiveDirectoryGroupListResult> data; 

        #endregion

        #region Properties

        private string _Name;
        public new string Name { get { return _Name; } set { _Name = value; } }

        private string _Domain;
        public string Domain 
        { 
            get { return _Domain; } 
            set 
            { 
                _Domain = value;
                StringBuilder sb = new StringBuilder();
                sb.Append(Text).Append(" (").Append(_Domain).Append(")");
                Text = sb.ToString();
            } 
        }

        private Common.Core.ModuleReference _ModuleReferece;
        public Common.Core.ModuleReference ModuleReference { get { return _ModuleReferece; } set { _ModuleReferece = value; } }

        #endregion

        #region EventHandlers

        private void FormActiveDirectoryGroupNew_Load(object sender, EventArgs e)
        {
            try
            {

                Initialize();
                RefreshListing();


            }
            catch (Exception)
            {
                throw;
            }
        }

        private void userControlListingGroup_OnTextSearchChanged(string textSearch)
        {
            try
            {
                var result = from s in data
                             where s.Domain.ToLower().Contains(textSearch.ToLower()) || s.Name.ToLower().Contains(textSearch.ToLower())
                             select s;

                userControlListingGroup.DataSource = result.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private void userControlListingGroup_OnRefresh(object sender, EventArgs e)
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
        
        private void buttonAddGroup_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormActiveDirectoryAddGroup frm = new FormActiveDirectoryAddGroup())
                {
                    frm.Domain = _Domain;
                    if (frm.ShowDialog() == DialogResult.OK)
                        RefreshListing();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private void userControlListingGroup_OnSelectionChanged(DataGridViewRow row)
        {
            try
            {
                _Name = row.Cells["Name"].Value.ToString();
                 
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
                if (_Name.Length == 0 && userControlListingGroup.dataGridViewList.Rows.Count > 0)
                {
                    Common.Core.ShowMessage("Please select group from the list.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
                    return; 
                }
                                                
                DialogResult = DialogResult.OK;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (userControlListingGroup.dataGridViewList.SelectedRows.Count == 0)
                {
                    Common.Core.ShowMessage("Please select group from the list to Remove.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return; 
                }

                if (Common.Core.ShowQuestion(userControlListingGroup.dataGridViewList.SelectedRows[0].Cells["Name"].Value.ToString() + " will be removed. Do you want to continue?") == DialogResult.OK)
                {
                    activeDirectoryGroup.Id = (int)userControlListingGroup.dataGridViewList.SelectedRows[0].Cells["Id"].Value;
                    activeDirectoryGroup.Operation = BTSS.Common.Core.Operation.DELETE;
                    activeDirectoryGroup.Save();                     
                    RefreshListing();
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
                activeDirectoryGroup = new Logic.ActiveDirectoryGroup(BTSS.Common.Core.ConnectionString);

                InitializeGrid();

                switch (_ModuleReferece)
                {
                    case BTSS.Common.Core.ModuleReference.Setting:
                        buttonCancel.Text = "Close";
                        buttonOK.Visible = false;
                        break;
                    case BTSS.Common.Core.ModuleReference.Tools:
                        break;
                }                  

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
                dtGroup.Columns.Add("Id");                
                dtGroup.Columns.Add("Name");
                dtGroup.Columns.Add("Domain"); 
                userControlListingGroup.DataSource = dtGroup;

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
                foreach (DataGridViewColumn col in userControlListingGroup.dataGridViewList.Columns)
                {
                    switch (col.HeaderText)
                    {
                        case "Domain": col.HeaderText = "Domain"; break;
                        case "Name": col.HeaderText = "Name"; break; 
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

        private void RefreshListing()
        {
            try
            { 
                using (var loader = new Common.Loader<string>(o =>
                { 
                    if (_ModuleReferece == BTSS.Common.Core.ModuleReference.Tools)
                        data = activeDirectoryGroup.GetActiveDirectoryGroupList(_Domain);
                    else if (_ModuleReferece == BTSS.Common.Core.ModuleReference.Setting)
                        data = activeDirectoryGroup.GetActiveDirectoryGroupList(""); //select ALL group if module reference is Settings.
                }))
                {
                    loader.RunWorker();
                };
                 
                userControlListingGroup.DataSource = data;

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
         
         
    }
}
