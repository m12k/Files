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
    public partial class FormModule : Form
    {
        #region "Contructor"

        public FormModule()
        {
            InitializeComponent();
        }

        #endregion

        #region "Properties"

        public string ModuleId { get; set; }
        public Common.Core.Operation Operation { get; set; }
 
        #endregion

        #region "Declarations"

        public event EventHandler OnSave;  
        private Logic.Interfaces.Projects.IModule module; 
        private Common.Validation validation;

        #endregion 

        #region "Event Handlers"

        private void FormModule_Load(object sender, EventArgs e)
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

        private void userControlButtonsSave_OnButtonClick(EventArgs e, ref bool isOk, BTSS.Common.UserControls.UserControlButtonsSave.ButtonClick button)
        {
            try
            {
                switch (button)
                {
                    case BTSS.Common.UserControls.UserControlButtonsSave.ButtonClick.Save: 
                        if (Save())
                        {
                            if (Operation == Common.Core.Operation.UPDATE)
                                DialogResult = DialogResult.OK;
                        }
                        break;
                    case BTSS.Common.UserControls.UserControlButtonsSave.ButtonClick.Cancel: DialogResult = DialogResult.Cancel; break;
                }

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
                module = new BTSS.Logic.Projects.Module(Common.Core.ProjectConnectionString, Common.Core.ProjectDataProvider);
                validation = new BTSS.Common.Validation(); 
                validation.Validate(this);

                if (Operation == Common.Core.Operation.UPDATE || Operation == Common.Core.Operation.VIEW)
                {                     
                    DataRow dr;

                    dr = module.GetProjectModuleDetails(ModuleId);

                    textBoxID.Text = ModuleId;
                    textBoxModuleName.Text = dr["mod_name"].ToString();
                    textBoxModuleName.Tag = dr["mod_name"].ToString();
                    textBoxDescription.Text = dr["mod_desc"].ToString();
                          
                    if (Operation == BTSS.Common.Core.Operation.VIEW)
                    {
                        userControlButtonsSave.Visible = false;
                        Lock();
                    }
                }

                InitializeGrid();

                if (Common.Core.ProjectDataProvider == BTSS.Common.Core.DataProvider.SQL) LoadTables();
                textBoxModuleName.Focus();

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
                DataTable dtTables = new DataTable();
                dtTables.Columns.Add("Id"); 
                dtTables.Columns.Add("IsSelected", typeof(bool));
                dtTables.Columns.Add("table_name"); 

                dataGridViewTables.DataSource = dtTables;
                 
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

                foreach (DataGridViewColumn col in dataGridViewTables.Columns)
                {
                    switch (col.HeaderText)
                    {
                        case "IsSelected": col.HeaderText = "";
                            col.Width = 20;
                            break;
                        case "table_name": col.HeaderText = "Table Name";
                            col.ReadOnly = true;
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
        
        private bool Save()
        {
            try
            {
                if (!validation.IsTextBoxesValid())
                {
                    Common.Core.ShowMessage("Please fill-in required field(s).", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                 
                if (Logic.Common.IsValueAlreadyExist(Common.Core.ProjectConnectionString, BTSS.Common.Core.ProjectDataProvider, "set_module", "mod_name", textBoxModuleName.Tag.ToString(), textBoxModuleName.Text, Operation))
                {
                    Common.Core.ShowMessage("Module Name already exist. Please check.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                ControlToClass();
                module.Save();

                if (Operation == BTSS.Common.Core.Operation.INSERT) { Common.Core.ShowMessage("Module " + textBoxModuleName.Text + " successfully saved!", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                else { Common.Core.ShowMessage("Module " + textBoxModuleName.Text + " successfully updated!", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                
                ClearControls();

                if (Operation == BTSS.Common.Core.Operation.INSERT)
                    OnSave(new object(), new EventArgs());

                textBoxModuleName.Focus();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Lock()
        {
            try
            {
                textBoxModuleName.ReadOnly = true;
                textBoxDescription.ReadOnly = true;
                dataGridViewTables.ReadOnly = true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ClearControls()
        {
            try
            {
                textBoxID.Clear();
                textBoxModuleName.Clear();
                textBoxDescription.Clear();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ControlToClass()
        {
            try
            {
                module.Operation = Operation;
                module.Id = ModuleId;
                module.Name = textBoxModuleName.Text;
                module.Desc = textBoxDescription.Text;
                module.Table = dataGridViewTables.DataSource as DataTable;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void LoadTables()
        {
            try
            {
                DataTable dt = module.GetModuleTableMap(textBoxID.Text);
                dataGridViewTables.DataSource = dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
          
    }
}
