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
    public partial class UserControlGroup : UserControl
    {

#region "Constructor"
        public UserControlGroup()
        {
            InitializeComponent();
        }        
#endregion
        
#region "Declarations"
        
        public event EventHandler OnClose;
        private ClassGroup classGroup;
        private DataTable data; 
        private string id = "";
        private string groupName;
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
                id = row.Cells["grp_id"].Value.ToString();
                groupName = row.Cells["grp_name"].Value.ToString();
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
                        where s.Field<string>("grp_name").ToLower().Contains(textSearch.ToLower()) || s.Field<string>("grp_desc").ToLower().Contains(textSearch.ToLower())
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
                using (var frm = new Forms.Individual.FormGroup())
                {
                    frm.Operation = Global.Operation.INSERT;
                    frm.Icon = Icon.FromHandle(icon.GetHicon());
                    frm.Text = "Group[NEW]";
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
                using (var frm = new Forms.Individual.FormGroup())
                {
                    frm.Operation = Global.Operation.UPDATE;
                    frm.Icon = Icon.FromHandle(icon.GetHicon());
                    frm.Text = "Group[EDIT]";
                    frm.GroupId = id;
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
                using (var frm = new Forms.Individual.FormGroup())
                {
                    frm.Operation = Global.Operation.VIEW;
                    frm.Icon = Icon.FromHandle(icon.GetHicon());
                    frm.Text = "Group[VIEW]";
                    frm.GroupId = id;
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
                if (!classGroup.IsGroupAllowToDelete(id))
                {
                    Global.ShowMessage("Group has reference user(s). Delete not allowed.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                 
                if (Global.ShowQuestion(groupName + " will be deleted. Do you want to continue?") == DialogResult.Cancel) { return false; }
 
                classGroup.Id = id;
                classGroup.Operation = Global.Operation.DELETE;
                classGroup.Save();
                Global.ShowMessage("Group " + groupName + " successfully deleted!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
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
                classGroup = new ClassGroup(Global.ProjectConnectionString, Global.ProjectDataProvider);
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
                dt.Columns.Add("grp_id");
                dt.Columns.Add("grp_name");
                dt.Columns.Add("grp_desc"); 
                 
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
                        case "grp_id": col.HeaderText = "Group Id"; break;
                        case "grp_name": col.HeaderText = "Group Name"; break;
                        case "grp_desc": col.HeaderText = "Description"; break;
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

                data = classGroup.GetGroupList();
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
