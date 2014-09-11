using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Biztech.Classes;

namespace Biztech 
{
    public partial class FormPreferences : Form
    {

        #region "Declarations"

        private Preference preference;

        #endregion

        #region Constructor
        
        public FormPreferences()
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
                preference.Id = (int)Global.Preferences.AUDIT_TRAIL;
                preference.Tag = Global.Preferences.AUDIT_TRAIL.ToString();
                preference.Value = checkBoxEnableAuditTrail.Checked.ToString();
                preference.Operation = Global.Operation.UPDATE;
                preference.Save();
                Global.ShowMessage("Preferences successfully updated!", MessageBoxButtons.OK, MessageBoxIcon.Information); 
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
                preference = new Preference(Global.ConnectionString); 

                BTSSContext ctx = new BTSSContext(Global.ConnectionString);
 
                var row = from s in ctx.Preferences
                          where s.Tag == Global.Preferences.AUDIT_TRAIL.ToString()
                          select new { s.Value };

                if (row.Count() > 0)
                {
                    checkBoxEnableAuditTrail.Checked = Convert.ToBoolean(row.FirstOrDefault().Value);
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
