using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Biztech.Classes;

namespace Biztech.Forms.BTSS
{
    public partial class FormGroups : Form
    {

        #region "Constructor"

        public FormGroups()
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
        private ClassGroups classGroups;   
        private Validation Validation; 

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
                classGroups = new ClassGroups(Global.ConnectionString);
                Validation = new Validation();
                Validation.Validate(this);

                InitializeGrid();

                if (Operation == Global.Operation.UPDATE || Operation == Global.Operation.VIEW)
                {

                    GetGroupDetailsResult result = classGroups.GetGroupDetails(GroupId);

                    textBoxID.Text = GroupId;
                    textBoxName.Text = result.Name;
                    textBoxDescription.Text = result.Desc;

                    if (Operation == Global.Operation.VIEW)
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
                if (!Validation.IsTextBoxesValid())
                {
                    Global.ShowMessage("Please fill-in required field(s).", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                 
                ControlToClass();
                classGroups.Save(); 
                 
                if (Operation == Global.Operation.INSERT) { Global.ShowMessage("Group " + textBoxName.Text + " successfully saved!", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                else { Global.ShowMessage("Group " + textBoxName.Text + " successfully updated!", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                ClearControls(); 
                
                if (Operation == Global.Operation.INSERT)
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
                classGroups.Operation = Operation;
                classGroups.Id = GroupId;
                classGroups.Name = textBoxName.Text;
                classGroups.Desc = textBoxDescription.Text;
                dataGridViewModules.Update();
                classGroups.DataGroupModule = dataGridViewModules.DataSource as DataTable;
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
                dataGridViewModules.DataSource = classGroups.GetGroupModule(textBoxID.Text);
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
