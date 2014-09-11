using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Collections;
  
namespace Biztech.UserControls.Generic
{
    public partial class UserControlExplorerBar : UserControl
    {
        
        private StrCollection module; 
 
        public UserControlExplorerBar()
        { 
            module = new StrCollection();
            module.OnAdd += new StrCollection.OnAddEvent(OnAdd); 

            InitializeComponent();
        }

        [Category("Modules")]
        [Description("Description")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]         
        public StrCollection Module
        {
            get
            {  
                return module; 
            } 
        }
           
        private void OnAdd(Label label)
        {
            try
            {
                tableLayoutPanelMain.Controls.Add(label);                 
            }
            catch (Exception)
            {
                throw;
            }
        } 
            
    } 
   
}
