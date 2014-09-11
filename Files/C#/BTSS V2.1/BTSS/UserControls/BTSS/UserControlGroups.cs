using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Biztech.Classes;

namespace Biztech.UserControls.BTSS
{
    public partial class UserControlGroups : UserControl
    {

#region "Constructor"
    public UserControlGroups()
    {
        InitializeComponent();
    }
#endregion

#region "Declarations"

    public event EventHandler OnClose;
    private ClassGroups classGroups;
    private List<GetGroupListResult> data; 
    private string id = "";
    private string groupName;
    private Validation Validation; 

#endregion

#region "EventHandlers"

    private void UserControlGroups_Load(object sender, EventArgs e)
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
            id = row.Cells["Id"].Value.ToString();
            groupName = row.Cells["Name"].Value.ToString();
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
            var result = from s in data
                         where s.Name.ToLower().Contains(textSearch.ToLower()) || s.Desc.ToLower().Contains(textSearch.ToLower())
                         select s;

            userControlListing.DataSource = result.ToList(); 
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
            using (var frm = new Forms.BTSS.FormGroups())
            {
                frm.Operation = Global.Operation.INSERT;
                frm.Icon = Icon.FromHandle(icon.GetHicon());
                frm.Text = "Group[NEW]";
                frm.OnSave += new EventHandler(RefreshOnSave);
                if (frm.ShowDialog() == DialogResult.OK)
                    RefreshListing();
            }

            classGroups.Operation = Global.Operation.INSERT;
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
            using (var frm = new Forms.BTSS.FormGroups())
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
            using (var frm = new Forms.BTSS.FormGroups())
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
            if (!classGroups.IsGroupAllowToDelete(id))
            {
                Global.ShowMessage("Group has reference user(s). Delete not allowed.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (Global.ShowQuestion(groupName + " will be deleted. Do you want to continue?") == DialogResult.Cancel) { return false; }
             
            classGroups.Id = id;
            classGroups.Operation = Global.Operation.DELETE;
            classGroups.Save(); 
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
            classGroups = new ClassGroups(Global.ConnectionString);
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
            dt.Columns.Add("Id");
            dt.Columns.Add("Name");
            dt.Columns.Add("Desc");

            userControlListing.DataSource = dt;

            //DataTable dtModules = new DataTable();
            //dtModules.Columns.Add("Id");
            //dtModules.Columns.Add("GroupId");
            //dtModules.Columns.Add("ModuleId");
            //dtModules.Columns.Add("Name");
            //dtModules.Columns.Add("CanAdd", typeof(bool));
            //dtModules.Columns.Add("CanEdit", typeof(bool));
            //dtModules.Columns.Add("CanDelete", typeof(bool));
 
            //dataGridViewModules.DataSource = dtModules;

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
                    case "Id": col.HeaderText = "User Id"; break;
                    case "Name": col.HeaderText = "Name"; break;
                    case "Desc": col.HeaderText = "Description"; break;                    
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

            data = classGroups.GetGroupList();
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