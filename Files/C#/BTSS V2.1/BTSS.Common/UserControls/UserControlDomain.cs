using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BTSS.Common.UserControls
{
    public partial class UserControlDomain : UserControl
    {
        public UserControlDomain()
        {
            InitializeComponent();
        }

        #region Declarations

        public event EventHandler OnSelectedIndexChanged;
        
        #endregion

        #region Properties

        public new string Text { get { return comboBoxDomain.Text; } }

        #endregion

        private void UserControlDomain_Load(object sender, EventArgs e)
        {
            try
            {

                comboBoxDomain.DataSource = Core.ADGroup.ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void comboBoxDomain_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (OnSelectedIndexChanged != null) OnSelectedIndexChanged(sender, e);
            } 
            catch (Exception)
            {
                throw;
            }
        }
    }
}
