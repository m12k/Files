using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class FormPagination : Form
    {
        public FormPagination()
        {
            InitializeComponent();
        }

        BindingSource bindingSource1 = new BindingSource();
        DataTable dtData;

        private void FormPagination_Load(object sender, EventArgs e)
        {
            try
            {
                bindingNavigator1.BindingSource = bindingSource1;
                bindingSource1.CurrentChanged += new System.EventHandler(bindingSource1_CurrentChanged);
                toolStripComboBoxPageSize.SelectedIndex = 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SetPageCount()
        {
            try
            {
                List<int> list = new List<int>();

                for (int offset = 0; offset < dtData.Rows.Count; offset += Convert.ToInt32(toolStripComboBoxPageSize.Text))
                    list.Add(offset);
                 
                bindingSource1.DataSource = list;
            }
            catch (Exception)
            {
                throw;
            }
        }
       

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            try 
	        {

                dataGridView.DataSource = dtData.AsEnumerable()
                    .Skip((Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1) * Convert.ToInt32(toolStripComboBoxPageSize.Text))
                    .Take(Convert.ToInt32(toolStripComboBoxPageSize.Text)).CopyToDataTable();

                //dataGridView.DataSource = dtData.AsEnumerable()
                //                .Where((r, i) => i >= 2 && i <= 2)
                //                .CopyToDataTable();
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
                dtData = new DataTable();
                dtData.Columns.Add("ccc");
                dtData.Rows.Add("one");
                dtData.Rows.Add("two");
                dtData.Rows.Add("three");
                dtData.Rows.Add("four");
                dtData.Rows.Add("five");
                dtData.Rows.Add("six");
                dtData.Rows.Add("seven");
                dtData.Rows.Add("eight");
                dtData.Rows.Add("nine");
                dtData.Rows.Add("ten");
                dtData.Rows.Add("eleven");
                dtData.Rows.Add("twelve");
                dtData.Rows.Add("thirten");
                dtData.Rows.Add("fourten");
                dtData.Rows.Add("fithten");  

                SetPageCount();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
