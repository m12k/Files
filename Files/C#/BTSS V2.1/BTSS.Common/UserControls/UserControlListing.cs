using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BTSS.Common.UserControls 
{
    public partial class UserControlListing : UserControl
    {

        #region Constructor

        public UserControlListing()
        {
            InitializeComponent();
        }

        #endregion

        #region Declarations

        public event EventHandler OnRefresh;

        public new event EventHandler OnPrint;

        public delegate void OnMouseDoubleClickEvent(DataGridViewRow row);
        public event OnMouseDoubleClickEvent OnRowDoubleClicked;

        public delegate void OnTextChangedEvent(string textSearch);
        public event OnTextChangedEvent OnTextSearchChanged;

        public delegate void OnSelectionChangedEvent(DataGridViewRow row);  
        public event OnSelectionChangedEvent OnSelectionChanged;
         
        public BindingSource _bs; 
        private bool _CanEdit;
        public bool CanEdit { get { return _CanEdit; } set { _CanEdit = value; } }

        private bool _CanPrint = false;
        public bool CanPrint { get { return _CanPrint; } set { _CanPrint = value; toolStripButtonPrint.Visible = _CanPrint; } }
         
        private object _dataSource = null;

        #endregion
          
        #region "Properties"

        public object DataSource { set { _dataSource = value; ResfreshData(); } }
         
        private bool _ShowRefresh = false;
        public bool ShowRefresh { get { return _ShowRefresh; } set { _ShowRefresh = value; toolStripButtonRefresh.Visible = _ShowRefresh; } }

        private bool _MultiSelect = false;
        public bool MultiSelect { get { return _MultiSelect; } set { _MultiSelect = value; dataGridViewList.MultiSelect = _MultiSelect; } }

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
                
                if ((dataGridViewList.SelectedRows.Count > 0) && (OnRowDoubleClicked != null)) OnRowDoubleClicked(dataGridViewList.SelectedRows[0]);                
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
                if  ((dataGridViewList.SelectedRows.Count > 0) && (OnSelectionChanged != null)) OnSelectionChanged(dataGridViewList.SelectedRows[0]); 
            }
            catch (Exception)
            { 
                throw;
            }
        }

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                OnPrint(sender, e);
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
         
        #endregion 
               
    }
}
