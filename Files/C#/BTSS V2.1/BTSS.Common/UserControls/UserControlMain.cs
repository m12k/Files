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
    public partial class UserControlMain : UserControl
    {
        public UserControlMain()
        {
            InitializeComponent();
        }
         
        private UserControl _control; 

        #region Properties

            public string ModuleDirectory { set { labelModuleDirectory.Text = value; } }
            
            public UserControl Control
            {
                get 
                { 
                    return _control; 
                }
                set 
                {  
                    _control = value;
                    value.Dock = DockStyle.Fill;
                    panelMain.Controls.Add(value);                    
                }
            } 

        #endregion 

    }
}
