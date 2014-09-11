using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Biztech.Classes;


namespace Biztech.UserControls.Generic
{
    public partial class UserControlListing : UserControl
    {
        public UserControlListing()
        {
            InitializeComponent();
        }

        public event EventHandler OnRefresh;

        public delegate void OnMouseDoubleClickEvent(DataGridViewRow row);
        public event OnMouseDoubleClickEvent OnRowDoubleClicked;

        public delegate void OnTextChangedEvent(string textSearch);
        public event OnTextChangedEvent OnTextSearchChanged;
         
        public delegate void OnSelectionChangedEvent(DataGridViewRow row);
        public event OnSelectionChangedEvent OnSelectionChanged;
         
        public BindingSource _bs;
        private bool CanEdit;
     
        private object _dataSource = null;

        #region "Properties"

        public object DataSource { set { _dataSource = value; ResfreshData(); } }

        private Global.Module _Module;
        public Global.Module Module { get { return _Module; } set { _Module = value; SetAccessRights(); } }


        private bool showRefresh = false;
        public bool ShowRefresh { get { return showRefresh; } set { showRefresh = value; toolStripButtonRefresh.Visible = showRefresh; } }

        #endregion

        #region "Event Handlers"

        private void toolStripTextBoxSearch_TextChanged(object sender, EventArgs e)
       {
            try
            { 
                OnTextSearchChanged(toolStripTextBoxSearch.Text);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                OnRefresh(sender, e);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void dataGridViewList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (!CanEdit) return;
                
                if ((dataGridViewList.SelectedRows.Count > 0) && (OnRowDoubleClicked != null))
                {
                    OnRowDoubleClicked(dataGridViewList.SelectedRows[0]);
                }
                
            }
            catch (Exception)
            { 
                throw;
            }
        }

        private void dataGridViewList_SelectionChanged(object sender, EventArgs e)
        {
            try
            { 
                if  ((dataGridViewList.SelectedRows.Count > 0) && (OnSelectionChanged != null))
                {
                    OnSelectionChanged(dataGridViewList.SelectedRows[0]);
                }
            }
            catch (Exception)
            { 
                throw;
            }
        }

        #endregion
        
        #region "Methods"

        private void ResfreshData()
        { 
            _bs = new BindingSource(); 
            _bs.DataSource = _dataSource; 
            dataGridViewList.DataSource = _bs;            
            bindingNavigator.BindingSource = _bs; 
        }

        private void SetAccessRights()
        {
            try
            { 
                 
                using (var classUser = new ClassUsers(Global.ConnectionString))
                {
                    var result = classUser.GetAccessRights(Global.UserId, _Module);
                 
                    CanEdit = result.CanEdit.Value; 
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
