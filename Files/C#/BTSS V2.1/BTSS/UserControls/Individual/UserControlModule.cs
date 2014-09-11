using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Biztech.Classes;

namespace Biztech.UserControls.Individual
{
    public partial class UserControlModule : UserControl
    {

#region "Constructor"
        public UserControlModule()
        {
            InitializeComponent();
        }        
#endregion
        
#region "Declarations"
        
        public event EventHandler OnClose; 
        private ClassModule classModule;
        private DataTable data;
        private string id = "";
        private string moduleName;
        private Validation Validation;
         
        #endregion

#region "EventHandlers"

        private void UserControlUsers_Load(object sender, EventArgs e)
        {
            try
            {               
                Initialize();
                InitializeGrid();  
                RefreshListing(); 
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void userControlButtonsMain_OnButtonClick(EventArgs e, ref bool isOk, UserControlButtonsMain.ButtonClick button, Bitmap icon)
        {
            try
            {
                switch (button)
                {
                    case UserControlButtonsMain.ButtonClick.New: isOk = New(icon); break;
                    case UserControlButtonsMain.ButtonClick.Edit: isOk = Edit(icon); break;
                    case UserControlButtonsMain.ButtonClick.View: isOk = View(icon); break;
                    case UserControlButtonsMain.ButtonClick.Delete: isOk = Delete(); break;
                    case UserControlButtonsMain.ButtonClick.Refresh: RefreshListing(); break;
                    case UserControlButtonsMain.ButtonClick.Close: OnClose(this, e); break;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void userControlListing_OnSelectionChanged(DataGridViewRow row)
        {
            try
            {         
                id = row.Cells["mod_id"].Value.ToString();
                moduleName = row.Cells["mod_name"].Value.ToString();
                userControlButtonsMain.ButtonsMode = UserControlButtonsMain.Mode.Select; 
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void userControlListing_OnTextSearchChanged(string textSearch)
        {
            try
            {
                var result = from s in data.AsEnumerable()                         
                        where s.Field<string>("mod_name").ToLower().Contains(textSearch.ToLower()) || s.Field<string>("mod_desc").ToLower().Contains(textSearch.ToLower())
                        select s;

                if (result.Any())
                    userControlListing.DataSource = result.CopyToDataTable();
                else
                {
                    userControlListing.DataSource = data.Clone(); 
                    userControlButtonsMain.ButtonsMode = UserControlButtonsMain.Mode.Default;
                } 
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void userControlListing_OnRefresh(object sender, EventArgs e)
        {
            try
            { 
                RefreshListing();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void userControlListing_OnRowDoubleClicked(DataGridViewRow row)
        {
            try
            {
                View(Properties.Resources.View);
            }
            catch (Exception)
            {
                throw;
            }
        }
         
        #endregion

#region "Methods"

        private bool New(Bitmap icon)
        {
            try
            {
                using (var frm = new Forms.Individual.FormModule())
                {
                    frm.Operation = Global.Operation.INSERT;
                    frm.Icon = Icon.FromHandle(icon.GetHicon());
                    frm.Text = "Module[NEW]";
                    frm.OnSave += new EventHandler(RefreshOnSave);
                    if (frm.ShowDialog() == DialogResult.OK)
                        RefreshListing();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool Edit(Bitmap icon)
        {
            try
            {
                using (var frm = new Forms.Individual.FormModule())
                {
                    frm.Operation = Global.Operation.UPDATE;
                    frm.Icon = Icon.FromHandle(icon.GetHicon());
                    frm.Text = "Module[EDIT]";
                    frm.ModuleId = id;
                    if (frm.ShowDialog() == DialogResult.OK)
                        RefreshListing();
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool View(Bitmap icon)
        {
            try
            {
                using (var frm = new Forms.Individual.FormModule())
                {
                    frm.Operation = Global.Operation.VIEW;
                    frm.Icon = Icon.FromHandle(icon.GetHicon());
                    frm.Text = "Module[VIEW]";
                    frm.ModuleId = id;
                    frm.ShowDialog();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
         
        private bool Delete()
        {
            try
            { 
                if (!classModule.IsModuleAllowToDelete(id))
                {
                    Global.ShowMessage("Module has reference group(s). Delete not allowed.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                if (Global.ShowQuestion(moduleName +" will be deleted. Do you want to continue?") == DialogResult.Cancel) { return false; }
 
                classModule.Id = id;
                classModule.Operation = Global.Operation.DELETE;
                classModule.Save();                
                Global.ShowMessage("Module " + moduleName + " successfully deleted!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                RefreshListing();

                return true;
            }
            catch (Exception)
            {
                throw;
            }    
        }
         
        private void Initialize()
        {
            try
            {              
                classModule = new ClassModule(Global.ProjectConnectionString, Global.ProjectDataProvider);
                Validation = new Validation();
                Validation.Validate(this);
            }
            catch (Exception)
            {
                throw;
            }        
        }

        private void InitializeGrid()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("mod_id");
                dt.Columns.Add("mod_name");
                dt.Columns.Add("mod_desc"); 
                 
                userControlListing.DataSource = dt;
                  
                InitializeGridLayout();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void InitializeGridLayout()
        {
            try
            {
                foreach (DataGridViewColumn col in userControlListing.dataGridViewList.Columns)
                {
                    switch (col.HeaderText)
                    {
                        case "mod_id": col.HeaderText = "Module Id"; break;
                        case "mod_name": col.HeaderText = "Module Name"; break;
                        case "mod_desc": col.HeaderText = "Description"; break; 
                        default:
                            col.Visible = false; break;
                    }
                }  

            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private void RefreshOnSave(object sender, EventArgs e)
        {
            try
            {
                RefreshListing();
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        private void RefreshListing()
        {
            try
            {
                id = "";
                userControlButtonsMain.ButtonsMode = UserControlButtonsMain.Mode.Default;

                data = classModule.GetProjectModuleList();
                userControlListing.DataSource = data;
            }
            catch (Exception)
            {
                throw;
            }
        } 

#endregion 
         
    }
}
