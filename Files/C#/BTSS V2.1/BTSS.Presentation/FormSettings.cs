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
    public partial class FormSettingsAuditTrail : Form
    {

        #region "Declarations"

        private Logic.Interfaces.IPreferences preferences;

        #endregion

        #region Constructor
        
        public FormSettingsAuditTrail()
        {
            InitializeComponent();
        }

        #endregion       

        #region "Event Handlers"

        private void FormPreferences_Load(object sender, EventArgs e)
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                preferences.Id = (int)BTSS.Common.Core.Preferences.AUDIT_TRAIL;
                preferences.Tag = BTSS.Common.Core.Preferences.AUDIT_TRAIL.ToString();
                preferences.Value = checkBoxEnableAuditTrail.Checked.ToString();
                preferences.Operation = BTSS.Common.Core.Operation.UPDATE;
                preferences.Save();
                BTSS.Common.Core.ShowMessage("Preferences successfully updated!", MessageBoxButtons.OK, MessageBoxIcon.Information); 
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
                preferences = new Logic.Preferences(BTSS.Common.Core.ConnectionString);
                 
                if ((string)preferences.GetValue(BTSS.Common.Core.Preferences.AUDIT_TRAIL) != "")
                    checkBoxEnableAuditTrail.Checked = Convert.ToBoolean(preferences.GetValue(BTSS.Common.Core.Preferences.AUDIT_TRAIL));
                else
                    checkBoxEnableAuditTrail.Checked = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        

        
    }
}
