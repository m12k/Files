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
    public partial class FormActiveDirectoryAddGroup : Form
    {

        #region Constructor
                
        public FormActiveDirectoryAddGroup()
        {
            InitializeComponent();
        }

        #endregion

        #region Declarations

        private ToolTip tp = new ToolTip();
        private Logic.Interfaces.IActiveDirectoryGroup activeDirectoryGroup;
        private bool isDirty = true;
         
        #endregion

        #region Properties

        private string _Domain;
        public string Domain 
        { 
            set {
                _Domain = value;
                StringBuilder sb = new StringBuilder();
                sb.Append(Text).Append(" (").Append(value).Append(")");
                Text = sb.ToString();
            } 
        }

        #endregion

        #region EventHandlers
        
        private void FormActiveDirectoryAddGroup_Load(object sender, EventArgs e)
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

        private void buttonCheckNames_Click(object sender, EventArgs e)
        {
            try
            { 
                using (FormActiveDirectoryGroupSearch frm = new FormActiveDirectoryGroupSearch())
                {
                    frm.Domain = _Domain;
                    frm.TextSearch = textBoxName.Text;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        textBoxName.Text = frm.SelectedItems; 

                        isDirty = false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                isDirty = true;
                buttonCheckNames.Enabled = textBoxName.Text.Length > 0;
                buttonSave.Enabled = textBoxName.Text.Length > 0;
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
                StringBuilder sbValid = new StringBuilder();
                StringBuilder sbInValid = new StringBuilder();

                sbValid.Append("Group succesfully saved:").Append(Environment.NewLine);
                sbInValid.Append("Group does not exist:").Append(Environment.NewLine);
                 
                foreach (string group in textBoxName.Text.Split(';'))
                {
                    if (group.Trim().Length == 0) continue;

                    activeDirectoryGroup.Domain = group.Split('\\')[0];
                    activeDirectoryGroup.Name = group.Split('\\')[1];

                    if (activeDirectoryGroup.IsDomainGroupExist(activeDirectoryGroup.Name, activeDirectoryGroup.Domain)) continue;
                     
                    if (isDirty)
                    {
                        if (Common.Core.IsActiveDirectoryGroupExist(activeDirectoryGroup.Domain, activeDirectoryGroup.Name))
                        {
                            activeDirectoryGroup.Save();
                            sbValid.Append("\t").Append(group).Append(Environment.NewLine);                   
                        }
                        else
                        {
                            sbInValid.Append("\t").Append(group).Append(Environment.NewLine);                  
                        } 
                    }
                    else
                    {
                        activeDirectoryGroup.Save();
                        sbValid.Append("\t").Append(group).Append(Environment.NewLine);                 
                    }
                }

                StringBuilder sb = new StringBuilder();

                sb.Append(sbValid.Length > 26 ? sbValid.ToString() : "");
                sb.Append(sbInValid.Length > 23 ? sbInValid.ToString() : "");

                if (sb.Length > 0)
                    Common.Core.ShowMessage(sb.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Methods

        private void Initialize()
        {
            try
            {
                activeDirectoryGroup = new Logic.ActiveDirectoryGroup(Common.Core.ConnectionString);
                tp.SetToolTip(labelExample, @"PRD\business technologies db users;PRD\SHR MNE1050 Dept Ops Business Tech - RWM"); 
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
 
    }
}
