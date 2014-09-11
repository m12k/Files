using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Biztech.Classes;

namespace Biztech.UserControls 
{
    public partial class UserControlButtonsSave : UserControl
    {

        #region "Constructor"

        public UserControlButtonsSave()
        {
            InitializeComponent();
        }

        #endregion

        #region "Declarations"

        public delegate void OnClickEventHandler(EventArgs e, ref bool isOk, ButtonClick button);
        public event OnClickEventHandler OnButtonClick;
         
        public enum ButtonClick {   Save, Cancel }
          
        #endregion
        
        #region "EventHandlers" 

        protected void OnClicked(EventArgs e, ref bool isOk, ButtonClick button)
        {
            if (OnButtonClick != null)
                OnButtonClick(e, ref isOk, button);
        }
        
        private void Button_Click(object sender, EventArgs e)
        {
            try
            {
                bool isOK = true;
                ButtonClick button;

                switch ((sender as System.Windows.Forms.Button).Text)
                {
                    case "Save":
                        button = ButtonClick.Save; break;
                    default://case "Cancel":
                        button = ButtonClick.Cancel; break;
                }

                OnClicked(e, ref isOK, button);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
         
      
    }
}
