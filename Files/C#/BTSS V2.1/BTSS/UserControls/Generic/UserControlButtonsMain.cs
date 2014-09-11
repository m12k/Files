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
    public partial class UserControlButtonsMain : UserControl
    {

        #region "Constructor"

        public UserControlButtonsMain()
        {
            InitializeComponent();
        }

        #endregion

        #region "Declarations"

        public delegate void OnClickEventHandler(EventArgs e, ref bool isOk, ButtonClick button,Bitmap icon);
        public event OnClickEventHandler OnButtonClick;

        public enum Mode { Default, Select, New, Edit }
        public enum ButtonClick { New, Edit, View, Delete, Refresh, Close }

        private ClassUsers classUser;
        private Mode _buttonsMode = Mode.Default;
        public Mode ButtonsMode
        {
            get
            {
                return _buttonsMode;
            }
            set
            {
                _buttonsMode = value;
                SetMode(_buttonsMode);
            }
        }

        private Global.Module _Module;
        public Global.Module Module { get { return _Module; } set { _Module = value; SetAccessRights(); } }

        private bool canView = true;
        private bool canAdd = true;
        private bool canEdit = true;
        private bool canDelete = true;

        #endregion
        
        #region "EventHandlers" 

        protected void OnClicked(EventArgs e, ref bool isOk, ButtonClick button, Bitmap icon)
        {
            if (OnButtonClick != null)
                OnButtonClick(e, ref isOk, button,icon);
        }
        
        private void toolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap icon = null;
                bool isOK = true;
                ButtonClick button;

                switch ((sender as System.Windows.Forms.ToolStripButton).Text)
                {
                    case "New":
                        button = ButtonClick.New; 
                        icon = Properties.Resources.New;
                        break;
                    case "Edit":
                        button = ButtonClick.Edit; 
                        icon = Properties.Resources.Edit;
                        break;
                    case "View":
                        button = ButtonClick.View; 
                        icon = Properties.Resources.View;
                        break;
                    case "Delete":
                        button = ButtonClick.Delete; break;  
                    case "Refresh":
                        button = ButtonClick.Refresh; break;  
                    default://case "Close":
                        button = ButtonClick.Close; break;
                } 

                OnClicked(e, ref isOK, button, icon);             
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
        
        #region Methods

        private void SetMode(Mode mode)
        {
            try
            {
                switch (mode)
                {
                    case Mode.Default:
                        toolStripButtonNew.Enabled = true && canAdd;
                        toolStripButtonEdit.Enabled = false;
                        toolStripButtonView.Enabled = false;
                        toolStripButtonDelete.Enabled = false;
                        break;
                    case Mode.Select:
                        toolStripButtonNew.Enabled = true && canAdd;
                        toolStripButtonEdit.Enabled = true && canEdit;
                        toolStripButtonView.Enabled = true && canView;
                        toolStripButtonDelete.Enabled = true && canDelete;
                        break;                  
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void SetAccessRights()
        {
            try
            {
                classUser = new ClassUsers(Global.ConnectionString);

                var result = classUser.GetAccessRights(Global.UserId, _Module);

                canView = result.CanView.Value;
                canAdd = result.CanAdd.Value;
                canEdit = result.CanEdit.Value;
                canDelete = result.CanDelete.Value;

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
      
    }
}
