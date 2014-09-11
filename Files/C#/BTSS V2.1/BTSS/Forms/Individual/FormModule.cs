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
        public Global.Operation Operation { get; set; }
 
        #endregion

        #region "Declarations"

        public event EventHandler OnSave;  
        private ClassModule classModule; 
        private Validation Validation;

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
                classModule = new ClassModule(Global.ProjectConnectionString, Global.ProjectDataProvider);
                Validation = new Validation();
                Validation.Validate(this);

                if (Operation == Global.Operation.UPDATE || Operation == Global.Operation.VIEW)
                {                     
                    DataRow dr;

                    dr = classModule.GetProjectModuleDetails(ModuleId);

                    textBoxID.Text = ModuleId;
                    textBoxModuleName.Text = dr["mod_name"].ToString();
                    textBoxModuleName.Tag = dr["mod_name"].ToString();
                    textBoxDescription.Text = dr["mod_desc"].ToString();
                          
                    if (Operation == Global.Operation.VIEW)
                    {
                        userControlButtonsSave.Visible = false;
                        Lock();
                    }
                }

                textBoxModuleName.Focus();

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
                 
                if (classModule.IsValueAlreadyExist("set_module", "mod_name", textBoxModuleName.Tag.ToString(), textBoxModuleName.Text, Operation))
                {
                    Global.ShowMessage("Module Name already exist. Please check.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                ControlToClass();
                classModule.Save();
              
                if (Operation == Global.Operation.INSERT) { Global.ShowMessage("Module " + textBoxModuleName.Text + " successfully saved!", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                else { Global.ShowMessage("Module " + textBoxModuleName.Text + " successfully updated!", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                
                ClearControls();

                if (Operation == Global.Operation.INSERT)
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
                classModule.Operation = Operation;
                classModule.Id = ModuleId;
                classModule.Name = textBoxModuleName.Text;
                classModule.Desc = textBoxDescription.Text;
            }
            catch (Exception)
            {
                throw;
            }
        }
         

        #endregion
          
    }
}
