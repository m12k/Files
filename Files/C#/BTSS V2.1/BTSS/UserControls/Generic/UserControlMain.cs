using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Biztech.UserControls
{
    public partial class UserControlMain : UserControl
    {
        public UserControlMain()
        {
            InitializeComponent();
        }


        public delegate void OnCloseControlEvent(TabPage tabPage);
        public event OnCloseControlEvent OnCloseControl;
          
        protected void OnClosed(TabPage tabPage)
        {
            if (OnCloseControl != null)
            {
                OnCloseControl(tabPage);
            }

        }

        private UserControl _control;
        private TabPage _tabPage = null;

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
                    switch (value.Name)
                    {                       
                       case "UserControlProjects": (value as BTSS.UserControlProjects).OnClose += new EventHandler(CloseControl);
                            break;
                       case "UserControlUsers":(value as BTSS.UserControlUsers).OnClose += new EventHandler(CloseControl);
                            break;
                       case "UserControlGroups": (value as BTSS.UserControlGroups).OnClose += new EventHandler(CloseControl);
                            break;                            
                       case "UserControlUser": (value as Individual.UserControlUser).OnClose += new EventHandler(CloseControl);
                            break;
                       case "UserControlModule": (value as Individual.UserControlModule).OnClose += new EventHandler(CloseControl);
                            break;
                       case "UserControlGroup": (value as Individual.UserControlGroup).OnClose += new EventHandler(CloseControl);
                            break;
                    }

                    _control = value;
                    value.Dock = DockStyle.Fill;
                    panelMain.Controls.Add(value);                    
                }
            }

            public TabPage Tab
            {
                get { return _tabPage; }
                set { _tabPage = value; }
            }

        #endregion

        #region Event Handlers

        #endregion

        #region Methods

            private void CloseControl(object sender,EventArgs e)
            {
                OnCloseControl(_tabPage);
            }

        #endregion

    }
}
