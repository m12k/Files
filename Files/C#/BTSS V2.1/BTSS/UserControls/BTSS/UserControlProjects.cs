using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Transactions;
using Biztech.Classes; 
 
namespace Biztech.UserControls.BTSS
{
    public partial class UserControlProjects : UserControl 
    {

#region Constructor
        public UserControlProjects()
        {
            InitializeComponent();
        }
        #endregion
              
#region Declarations

        public event EventHandler OnClose;
        private ClassProject classProject; 
        private string id = "";
        private string projectName;
        private List<GetProjectListResult> dataList;
   
#endregion

#region EventHandlers
          
        private void UserControlProject_Load(object sender, EventArgs e)
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
                    case UserControlButtonsMain.ButtonClick.Close: OnClose(this,e); break;
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
             
        private void userControlListing_OnTextSearchChanged(string textSearch)
        {
            try
            {
                var result = from s in dataList
                             where s.Name.ToLower().Contains(textSearch.ToLower()) || s.Desc.ToLower().Contains(textSearch.ToLower())
                             select s;

                userControlListing.DataSource = result.ToList();
                
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
                id = row.Cells["Id"].Value.ToString();
                projectName = row.Cells["Name"].Value.ToString();
                userControlButtonsMain.ButtonsMode = UserControlButtonsMain.Mode.Select;                  
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
           
#region Methods

        private bool New(Bitmap icon)
        {
            try
            {  
                using (var frm = new Forms.BTSS.FormProject())
                {
                    frm.Operation = Global.Operation.INSERT;
                    frm.Icon = Icon.FromHandle(icon.GetHicon());
                    frm.Text = "Project[NEW]";
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
                using (var frm = new Forms.BTSS.FormProject())
                {
                    frm.Operation = Global.Operation.UPDATE;
                    frm.Icon = Icon.FromHandle(icon.GetHicon());
                    frm.Text = "Project[EDIT]";
                    frm.ProjectId = id;
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
                using (var frm = new Forms.BTSS.FormProject())
                {
                    frm.Operation = Global.Operation.VIEW;
                    frm.Icon = Icon.FromHandle(icon.GetHicon());
                    frm.Text = "Project[VIEW]";
                    frm.ProjectId = id;
                    frm.ShowDialog();
                }
                return true ;
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
                if (!classProject.IsProjectAllowToDelete(id))
                {
                    Global.ShowMessage("Project has reference user(s). Delete not allowed.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                if (Global.ShowQuestion(projectName + " will be deleted. Do you want to continue?") == DialogResult.Cancel) { return false; }
                 
                classProject.Id = id; 
                classProject.Operation = Global.Operation.DELETE;
                classProject.Save(); 
                Global.ShowMessage("Project " + projectName + " successfully deleted!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                classProject = new ClassProject(Global.ConnectionString);                 
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
                dt.Columns.Add("Id");
                dt.Columns.Add("Name");
                dt.Columns.Add("Desc");
                dt.Columns.Add("Version");
                dt.Columns.Add("IsActive",typeof(bool));

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
                foreach(DataGridViewColumn col in userControlListing.dataGridViewList.Columns)
                {
                   
                    switch (col.HeaderText) 
                    {
                        case "Id": col.HeaderText = "Project Id"; break;
                        case "Name": col.HeaderText = "Name"; break;
                        case "Desc": col.HeaderText = "Description"; break;
                        case "Version": col.HeaderText = "Version"; break;
                        case "IsActive": col.HeaderText = "Is Active"; break; 
                        default :
                            col.Visible = false; break;
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void RefreshOnSave(object sender,EventArgs e)
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

                dataList = classProject.GetProjectList();
                userControlListing.DataSource = dataList; 
            }
            catch (Exception)
            {
                throw;
            }
        } 

        #endregion  
        
    }
}
 