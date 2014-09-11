using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Biztech.Classes;

namespace Biztech.Forms.Individual
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
        public Global.Operation Operation { get; set; }

        #endregion

        #region "Declarations"

        public event EventHandler OnSave; 
        private ClassGroup classGroup; 
        private Validation Validation;
          
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

        private void userControlButtonsSave_OnButtonClick(EventArgs e, ref bool isOk, Biztech.UserControls.UserControlButtonsSave.ButtonClick button)
        {
            try
            {
                switch (button)
                {
                    case Biztech.UserControls.UserControlButtonsSave.ButtonClick.Save:
                        if (Save())
                        {
                            if (Operation == Global.Operation.UPDATE)
                                DialogResult = DialogResult.OK;
                        }
                        break;
                    case Biztech.UserControls.UserControlButtonsSave.ButtonClick.Cancel: DialogResult = DialogResult.Cancel; break;
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
                classGroup = new ClassGroup(Global.ProjectConnectionString, Global.ProjectDataProvider);
                Validation = new Validation();
                Validation.Validate(this);

                InitializeGrid(); 

                if (Operation == Global.Operation.UPDATE || Operation == Global.Operation.VIEW)
                {
                                                            
                    DataRow dr;

                    dr = classGroup.GetGroupDetails(GroupId);

                    textBoxID.Text = GroupId;
                    textBoxGroupName.Text = dr["grp_name"].ToString();
                    textBoxGroupName.Tag = dr["grp_name"].ToString();
                    textBoxDescription.Text = dr["grp_desc"].ToString();
                     
                    if (Operation == Global.Operation.VIEW)
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
                if (!Validation.IsTextBoxesValid())
                {
                    Global.ShowMessage("Please fill-in required field(s).", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                               
                if (classGroup.IsValueAlreadyExist("set_group", "grp_name", textBoxGroupName.Tag.ToString(), textBoxGroupName.Text, Operation))
                {
                    Global.ShowMessage("Group Name already exist. Please check.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                ControlToClass();
                classGroup.Save();  

                if (classGroup.Operation == Global.Operation.INSERT) { Global.ShowMessage("Group " + textBoxGroupName.Text + " successfully saved!", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                else { Global.ShowMessage("Group " + textBoxGroupName.Text + " successfully updated!", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                
                ClearControls();

                if (Operation == Global.Operation.INSERT)
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
                classGroup.Operation = Operation;
                classGroup.Id = GroupId;
                classGroup.Name = textBoxGroupName.Text;
                classGroup.Desc = textBoxDescription.Text;
                classGroup.Data = dataGridViewModules.DataSource as DataTable;
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
                DataTable dt = classGroup.GetGroupModule(GroupId);

                DataRow drr;

                dataGridViewModules.DataSource = classGroup.GetModuleAccess();

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
