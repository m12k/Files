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
    public partial class FormGroup : Form
    {

        #region "Constructor"

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
        private Logic.Group group;   
        private Common.Validation validation; 

        #endregion

        #region "EventHandlers"
        
        private void FormGroups_Load(object sender, EventArgs e)
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

        private void userControlButtonsSave_OnButtonClick(EventArgs e, ref bool isOk, Common.UserControls.UserControlButtonsSave.ButtonClick button)
        {
            try
            {
                switch (button)
                {
                    case Common.UserControls.UserControlButtonsSave.ButtonClick.Save:
                        if (Save())
                        {
                            if (Operation == Common.Core.Operation.UPDATE)
                                DialogResult = DialogResult.OK;
                        }
                        break;
                    case Common.UserControls.UserControlButtonsSave.ButtonClick.Cancel: DialogResult = DialogResult.Cancel; break;
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
                group = new BTSS.Logic.Group(Common.Core.ConnectionString);
                validation = new BTSS.Common.Validation(); 
                validation.Validate(this);

                InitializeGrid();

                if (Operation == Common.Core.Operation.UPDATE || Operation == Common.Core.Operation.VIEW)
                {

                    Logic.GetGroupDetailsResult result = group.GetGroupDetails(GroupId);

                    textBoxID.Text = GroupId;
                    textBoxName.Text = result.Name;
                    textBoxDescription.Text = result.Desc;

                    if (Operation == Common.Core.Operation.VIEW)
                    {
                        userControlButtonsSave.Visible = false;
                        Lock();
                    }
                } 
                
                LoadModules();
                textBoxName.Focus();
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
                DataTable dtModules = new DataTable();
                dtModules.Columns.Add("Id");
                dtModules.Columns.Add("GroupId");
                dtModules.Columns.Add("ModuleId");
                dtModules.Columns.Add("Name");
                dtModules.Columns.Add("CanView", typeof(bool));
                dtModules.Columns.Add("CanAdd", typeof(bool));
                dtModules.Columns.Add("CanEdit", typeof(bool));
                dtModules.Columns.Add("CanDelete", typeof(bool));

                dataGridViewModules.DataSource = dtModules;

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
                        case "Name": col.HeaderText = "Name";
                            col.ReadOnly = true;
                            break;
                        case "CanView": col.HeaderText = "Can View";
                            col.Width = 50;
                            break;
                        case "CanAdd": col.HeaderText = "Can Add";
                            col.Width = 50;
                            break;
                        case "CanEdit": col.HeaderText = "Can Edit";
                            col.Width = 50;
                            break;
                        case "CanDelete": col.HeaderText = "Can Delete";
                            col.Width = 80;
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
                 
                ControlToClass();
                group.Save();

                if (Operation == Common.Core.Operation.INSERT) { Common.Core.ShowMessage("Group " + textBoxName.Text + " successfully saved!", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                else { Common.Core.ShowMessage("Group " + textBoxName.Text + " successfully updated!", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                ClearControls();

                if (Operation == Common.Core.Operation.INSERT)
                    OnSave(new object(), new EventArgs());

                textBoxName.Focus();
                return true;
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
                textBoxName.Clear();
                textBoxDescription.Clear();
                LoadModules();
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
                group.Name = textBoxName.Text;
                group.Desc = textBoxDescription.Text;
                dataGridViewModules.Update();
                group.DataGroupModule = dataGridViewModules.DataSource as DataTable;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private void LoadModules()
        {
            try
            {
                dataGridViewModules.DataSource = group.GetGroupModule(textBoxID.Text);
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
                textBoxName.ReadOnly = true;
                textBoxName.ReadOnly = true;
                textBoxDescription.ReadOnly = true;
                dataGridViewModules.ReadOnly = true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}
