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
    public partial class FormGroup : Form
    {        
        #region "Contructor"

        public FormGroup()
        {
            InitializeComponent();
        } 

        #endregion

        #region "Properties"

        public string GroupId { get; set; }
        public Common.Core.Operation Operation { get; set; }

        #endregion

        #region "Declarations"

        public event EventHandler OnSave; 
        private Logic.Interfaces.Projects.IGroup group; 
        private Common.Validation validation;
          
        #endregion
         
        #region "Event Handlers"

        private void FormGroup_Load(object sender, EventArgs e)
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
                            if (Operation == BTSS.Common.Core.Operation.UPDATE)
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
                group = new Logic.Projects.Group(Common.Core.ProjectConnectionString, Common.Core.ProjectDataProvider);
                validation = new BTSS.Common.Validation(); 
                validation.Validate(this);

                InitializeGrid();

                if (Operation == Common.Core.Operation.UPDATE || Operation == Common.Core.Operation.VIEW)
                {
                                                            
                    DataRow dr;

                    dr = group.GetGroupDetails(GroupId);

                    textBoxID.Text = GroupId;
                    textBoxGroupName.Text = dr["grp_name"].ToString();
                    textBoxGroupName.Tag = dr["grp_name"].ToString();
                    textBoxDescription.Text = dr["grp_desc"].ToString();

                    if (Operation == Common.Core.Operation.VIEW)
                    {
                        userControlButtonsSave.Visible = false;
                        Lock();
                    } 
                }

                LoadModuleAccess();
                textBoxGroupName.Focus();
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
                DataTable dtGroupModule = new DataTable();
                dtGroupModule.Columns.Add("mod_id");
                dtGroupModule.Columns.Add("mod_name");
                dtGroupModule.Columns.Add("mod_desc");
                dtGroupModule.Columns.Add("can_view", typeof(bool));
                dtGroupModule.Columns.Add("can_add", typeof(bool));
                dtGroupModule.Columns.Add("can_edit", typeof(bool));
                dtGroupModule.Columns.Add("can_delete", typeof(bool));
                dataGridViewModules.DataSource = dtGroupModule;

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
                 
                foreach (DataGridViewColumn col in dataGridViewModules.Columns)
                {
                    switch (col.HeaderText)
                    {
                        case "mod_name": col.HeaderText = "Module";
                            col.ReadOnly = true;
                            break;
                        case "can_view": col.HeaderText = "Can View";
                            col.Width = 40;
                            break;
                        case "can_add": col.HeaderText = "Can Add";
                            col.Width = 40; 
                            break;
                        case "can_edit": col.HeaderText = "Can Edit";
                            col.Width = 40; 
                            break;
                        case "can_delete": col.HeaderText = "Can Delete";
                            col.Width = 70; 
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

                if (Logic.Common.IsValueAlreadyExist(Common.Core.ProjectConnectionString, Common.Core.ProjectDataProvider, "set_group", "grp_name", textBoxGroupName.Tag.ToString(), textBoxGroupName.Text, Operation))
                {
                    Common.Core.ShowMessage("Group Name already exist. Please check.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                ControlToClass();
                group.Save();

                if (group.Operation == Common.Core.Operation.INSERT) { Common.Core.ShowMessage("Group " + textBoxGroupName.Text + " successfully saved!", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                else { Common.Core.ShowMessage("Group " + textBoxGroupName.Text + " successfully updated!", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                
                ClearControls();

                if (Operation == Common.Core.Operation.INSERT)
                    OnSave(new object(), new EventArgs());

                textBoxGroupName.Focus();
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
                textBoxGroupName.ReadOnly = true;
                textBoxDescription.ReadOnly = true;
                dataGridViewModules.ReadOnly = true;
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
                textBoxGroupName.Clear();
                textBoxDescription.Clear();
                LoadModuleAccess();
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
                group.Operation = Operation;
                group.Id = GroupId;
                group.Name = textBoxGroupName.Text;
                group.Desc = textBoxDescription.Text;
                group.Data = dataGridViewModules.DataSource as DataTable;
            }
            catch (Exception)
            {
                throw;
            }
        } 

        private void LoadModuleAccess()
        {
            try
            {
                DataTable dt = group.GetGroupModule(GroupId);

                DataRow drr;

                dataGridViewModules.DataSource = group.GetModuleAccess();

                foreach (DataRow row in dt.Rows)
                {
                    drr = ((dataGridViewModules.DataSource) as DataTable).Select("mod_id = '" + row["mod_id"] + "'", "").FirstOrDefault();

                    drr["can_view"] = row["can_view"];
                    drr["can_add"] = row["can_add"];
                    drr["can_edit"] = row["can_edit"];
                    drr["can_delete"] = row["can_delete"];
                }

                (dataGridViewModules.DataSource as DataTable).AcceptChanges();
            }
            catch (Exception)
            {
                throw;
            }
        } 

        #endregion

    }
}
